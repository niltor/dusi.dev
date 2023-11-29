using Docfx;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;

namespace Application.Services;

public class InitDataTask
{
    public static async Task InitDataAsync(IServiceProvider provider)
    {
        CommandDbContext context = provider.GetRequiredService<CommandDbContext>();
        ILoggerFactory loggerFactory = provider.GetRequiredService<ILoggerFactory>();
        ILogger<InitDataTask> logger = loggerFactory.CreateLogger<InitDataTask>();
        IConfiguration configuration = provider.GetRequiredService<IConfiguration>();

        string? connectionString = context.Database.GetConnectionString();
        try
        {
            // 执行迁移,如果手动执行,请删除该代码
            context.Database.Migrate();

            if (!await context.Database.CanConnectAsync())
            {
                logger.LogError("数据库无法连接:{message}", connectionString);
                return;
            }
            else
            {
                // 判断是否初始化
                SystemRole? role = await context.SystemRoles.SingleOrDefaultAsync(r => r.Name.ToLower() == "admin");
                if (role == null)
                {
                    logger.LogInformation("初始化数据");
                    await InitRoleAndUserAsync(context);
                }
                await UpdateAsync(context, configuration, logger);
                var env = provider.GetRequiredService<IWebHostEnvironment>();
                await UpdateStaticFilesAsync(context, env, logger);
            }
        }
        catch (Exception ex)
        {
            logger.LogError("初始化异常,请检查数据库配置：{message}", connectionString + ex.Message);
        }
    }

    /// <summary>
    /// 初始化角色和管理用户
    /// </summary>
    public static async Task InitRoleAndUserAsync(CommandDbContext context)
    {
        SystemRole role = new()
        {
            Name = "Admin",
            NameValue = AppConst.Admin
        };
        SystemRole userRole = new()
        {
            Name = "User",
            NameValue = AppConst.User
        };
        string salt = HashCrypto.BuildSalt();
        SystemUser user = new()
        {
            UserName = "admin",
            PasswordSalt = salt,
            PasswordHash = HashCrypto.GeneratePwd("123456", salt),
            SystemRoles = new List<SystemRole>() { role },
        };
        _ = context.SystemRoles.Add(userRole);
        _ = context.SystemRoles.Add(role);
        _ = context.SystemUsers.Add(user);
        _ = await context.SaveChangesAsync();
    }

    /// <summary>
    /// 更新
    /// </summary>
    /// <param name="context"></param>
    /// <param name="configuration"></param>
    /// <param name="logger"></param>
    /// <returns></returns>
    public static async Task UpdateAsync(CommandDbContext context, IConfiguration configuration, ILogger<InitDataTask> logger)
    {
        // 查询库中版本
        WebConfig? version = await context.WebConfigs.Where(c => c.Key == AppConst.Version).FirstOrDefaultAsync();
        if (version == null)
        {
            WebConfig config = new()
            {
                IsSystem = true,
                Valid = true,
                Key = AppConst.Version,
                // 版本格式:yyMMdd.编号
                Value = DateTime.UtcNow.ToString("yyMMdd") + ".01"
            };
            _ = context.Add(config);
            _ = await context.SaveChangesAsync();
            version = config;
        }
        // 比对新版本
        string? newVersion = configuration.GetValue<string>(AppConst.Version);

        if (double.TryParse(newVersion, out double newVersionValue)
            && double.TryParse(version.Value, out double versionValue))
        {
            if (newVersionValue > versionValue)
            {
                // TODO:执行更新方法
                version.Value = newVersionValue.ToString();
                _ = await context.SaveChangesAsync();
            }
        }
        else
        {
            logger.LogError("版本格式错误");
        }
    }

    /// <summary>
    /// 更新静态文件
    /// </summary>
    /// <param name="context"></param>
    /// <param name="_env"></param>
    /// <param name="logger"></param>
    /// <returns></returns>
    public static async Task UpdateStaticFilesAsync(CommandDbContext context, IWebHostEnvironment _env, ILogger<InitDataTask> logger)
    {
        try
        {
            var markdownPath = Path.Combine(_env.WebRootPath, "markdown");
            if (!Directory.Exists(markdownPath))
            {
                Directory.CreateDirectory(markdownPath);
            }
            // get alll files in markdown folder
            var files = Directory.GetFiles(markdownPath, "*.md", SearchOption.AllDirectories);

            var fileNames = files.Select(f => new FileInfo(f).Directory?.Name ?? "" + Path.GetFileNameWithoutExtension(f))
                .ToList();

            // get all blogs from database which not exist in markdown path
            var blogs = context.Blogs.Where(b => !fileNames.Contains(b.Catalog.Name + b.Id.ToString()))
                .Include(b => b.Catalog)
                .ToList();

            if (blogs.Count > 0)
            {
                foreach (var blog in blogs)
                {
                    var path = Path.Combine(markdownPath, blog.Catalog.Name);
                    if (!Directory.Exists(path))
                    {
                        Directory.CreateDirectory(path);
                    }
                    var fileName = blog.Id.ToString() + ".md";
                    await File.WriteAllTextAsync(Path.Combine(path, fileName), blog.Content);
                }
            }
            if (_env.IsProduction())
            {
                await Docset.Build(Path.Combine(_env.WebRootPath, "docfx.json"), new BuildOptions { });
            }
        }
        catch (Exception ex)
        {
            logger.LogError("udpate static file error: {message}", ex.Message);
        }
    }
}
