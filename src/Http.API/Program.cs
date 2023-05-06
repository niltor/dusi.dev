using System.Text.Encodings.Web;
using System.Text.Unicode;

using Application;
using Application.Implement;

using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

using Share.Options;

var builder = WebApplication.CreateBuilder(args);
var services = builder.Services;
var configuration = builder.Configuration;
services.AddHttpContextAccessor();

// 配置
var azAppConfigConnection = builder.Configuration["AppConfig"];
if (!string.IsNullOrEmpty(azAppConfigConnection))
{
    builder.Configuration.AddAzureAppConfiguration(options =>
    {
        options.Connect(azAppConfigConnection)
            .ConfigureRefresh(refresh =>
            {
                refresh.Register("ConfigVersion", refreshAll: true);
            });
    });
}
builder.Services.AddAzureAppConfiguration();
// database sql
string? connectionString = configuration.GetConnectionString("Default");
services.AddDbContextPool<QueryDbContext>(option =>
{
    _ = option.UseNpgsql(connectionString, sql =>
    {
        _ = sql.MigrationsAssembly("Http.API");
        _ = sql.CommandTimeout(10);
    });
});
services.AddDbContextPool<CommandDbContext>(option =>
{
    _ = option.UseNpgsql(connectionString, sql =>
    {
        _ = sql.MigrationsAssembly("Http.API");
        _ = sql.CommandTimeout(10);
    });
});

// TODO:临时使用内存缓存
services.AddDistributedMemoryCache();

services.Configure<AzureOption>(configuration.GetSection("Azure"));

// 注入选项及自定义服务
services.AddSingleton<StorageService>();

//services.AddGrpc();
services.AddDataStore();
services.AddManager();
services.AddDaprClient();

#region OpenTelemetry:log/trace/metric
var otlpEndpoint = configuration.GetSection("OTLP")
    .GetValue<string>("Endpoint")
    ?? "http://localhost:4317";
services.AddOpenTelemetry("dusi", opt =>
{
    opt.Endpoint = new Uri(otlpEndpoint);
});

#endregion
#region 接口相关内容:jwt/授权/cors
// use jwt
services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(cfg =>
{
    cfg.SaveToken = true;
    var sign = configuration.GetSection("Jwt")["Sign"];
    if (string.IsNullOrEmpty(sign))
    {
        throw new Exception("未找到有效的jwt配置");
    }

    cfg.TokenValidationParameters = new TokenValidationParameters()
    {
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(sign)),
        ValidIssuer = configuration.GetSection("Jwt")["Issuer"],
        ValidAudience = configuration.GetSection("Jwt")["Audience"],
        ValidateIssuer = true,
        ValidateLifetime = true,
        RequireExpirationTime = true,
        ValidateIssuerSigningKey = true
    };
});

// 验证
services.AddAuthorization(options =>
{
    options.AddPolicy(Const.User, policy =>
        policy.RequireRole(Const.Admin, Const.User));
    options.AddPolicy(Const.Admin, policy =>
        policy.RequireRole(Const.Admin));
});

// cors配置 
services.AddCors(options =>
{
    options.AddPolicy("default", builder =>
    {
        builder.AllowAnyOrigin();
        builder.AllowAnyMethod();
        builder.AllowAnyHeader();
    });
});
#endregion

services.AddHealthChecks();
// api 接口文档设置
services.AddSwaggerGen(c =>
{
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Please enter a valid token",
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        BearerFormat = "JWT",
        Scheme = "Bearer"
    });
    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type=ReferenceType.SecurityScheme,
                    Id="Bearer"
                }
            },
            Array.Empty<string>()
        }
    });
    c.SwaggerDoc("client", new OpenApiInfo
    {
        Title = "Client API",
        Description = "API 文档",
        Version = "v1"
    });
    c.SwaggerDoc("admin", new OpenApiInfo
    {
        Title = "Admin API",
        Description = "API 文档",
        Version = "v1"
    });
    var xmlFiles = Directory.GetFiles(AppContext.BaseDirectory, "*.xml", SearchOption.TopDirectoryOnly);
    foreach (var item in xmlFiles)
    {
        try
        {
            c.IncludeXmlComments(item, includeControllerXmlComments: true);
        }
        catch (Exception) { }
    }
    c.SupportNonNullableReferenceTypes();
    c.DescribeAllParametersInCamelCase();
    c.CustomOperationIds((z) =>
    {
        var descriptor = (ControllerActionDescriptor)z.ActionDescriptor;
        return $"{descriptor.ControllerName}_{descriptor.ActionName}";
    });
    c.SchemaFilter<EnumSchemaFilter>();
    c.MapType<DateOnly>(() => new OpenApiSchema
    {
        Type = "string",
        Format = "date"
    });
});

services.AddControllersWithViews()
    .ConfigureApiBehaviorOptions(o =>
    {
        o.InvalidModelStateResponseFactory = context =>
        {
            return new CustomBadRequest(context, null);
        };
    })
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
        options.JsonSerializerOptions.Encoder = JavaScriptEncoder.Create(UnicodeRanges.All);
    });

// add middleware

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseCors("default");
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/client/swagger.json", name: "client");
        c.SwaggerEndpoint("/swagger/admin/swagger.json", "admin");
    });
}
else
{
    // 生产环境需要新的配置
    app.UseCors("default");
    //app.UseHsts();
    app.UseHttpsRedirection();
}

app.UseStaticFiles();
app.UseBlogViewMiddleware();

// 异常统一处理
app.UseExceptionHandler(handler =>
{
    handler.Run(async context =>
    {
        context.Response.StatusCode = 500;
        var exception = context.Features.Get<IExceptionHandlerFeature>()?.Error;
        var result = new
        {
            Title = "异常错误",
            Source = exception?.Source,
            Detail = exception?.Message + exception?.InnerException?.Message,
            StackTrace = exception?.StackTrace,
            Status = 500,
            TraceId = context.TraceIdentifier
        };
        await context.Response.WriteAsJsonAsync(result);
    });
});

app.UseHealthChecks("/health");
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

app.UseCloudEvents();
app.MapControllers();
app.MapSubscribeHandler();
app.MapFallbackToFile("index.html");

using (app)
{
    app.Start();
    // 初始化工作
    await using (AsyncServiceScope scope = app.Services.CreateAsyncScope())
    {
        IServiceProvider provider = scope.ServiceProvider;
        await InitDataTask.InitDataAsync(provider);
    }
    app.WaitForShutdown();
}

public partial class Program { }