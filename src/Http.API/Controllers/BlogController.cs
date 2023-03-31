using Dapr.Client;
using Share.Models.BlogDtos;

namespace Http.API.Controllers;

/// <summary>
/// 博客
/// </summary>
public class BlogController : ClientControllerBase<IBlogManager>
{
    private readonly DaprClient dapr;
    private readonly StorageService storageService;

    public BlogController(
        IUserContext user,
        ILogger<BlogController> logger,
        IBlogManager manager,
        DaprClient dapr,
        StorageService storageService) : base(manager, user, logger)
    {
        this.dapr = dapr;
        this.storageService = storageService;
    }

    /// <summary>
    /// 筛选
    /// </summary>
    /// <param name="filter"></param>
    /// <returns></returns>
    [HttpPost("filter")]
    [AllowAnonymous]
    public async Task<ActionResult<PageList<BlogItemDto>>> FilterAsync(BlogFilterDto filter)
    {
        return await manager.FilterAsync(filter);
    }

    /// <summary>
    /// 获取分类信息
    /// </summary>
    /// <returns></returns>
    [HttpGet("types")]
    public List<EnumDictionary> GetTypes()
    {
        return manager.GetTypes();
    }

    /// <summary>
    /// 详情
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpGet("{id}")]
    [AllowAnonymous]
    public async Task<ActionResult<Blog>> GetDetailAsync([FromRoute] Guid id)
    {
        var res = await manager.FindAsync(id);
        return res == null ? NotFound() : res;
    }

    /// <summary>
    /// 新增
    /// </summary>
    /// <param name="form"></param>
    /// <returns></returns>
    [HttpPost]
    public async Task<ActionResult<Blog>> AddAsync(BlogAddDto form)
    {
        var entity = await manager.CreateNewEntityAsync(form);
        return await manager.AddAsync(entity);
    }

    /// <summary>
    /// 更新
    /// </summary>
    /// <param name="id"></param>
    /// <param name="form"></param>
    /// <returns></returns>
    [HttpPut("{id}")]
    public async Task<ActionResult<Blog?>> UpdateAsync([FromRoute] Guid id, BlogUpdateDto form)
    {
        var current = await manager.GetOwnedAsync(id);
        if (current == null) return NotFound();
        return await manager.UpdateAsync(current, form);
    }

    /// <summary>
    /// 上传图片
    /// </summary>
    /// <param name="upload"></param>
    /// <returns></returns>
    [HttpPost("upload")]
    [RequestSizeLimit(1024 * 1024 * 1)]
    public async Task<ActionResult<UploadResult>> UploadImgAsync(IFormFile upload)
    {
        if (upload == null)
        {
            return Problem("没有上传的文件", title: "业务错误");
        }
        //获取静态资源文件根目录
        if (upload.Length > 0)
        {
            string fileExt = Path.GetExtension(upload.FileName).ToLowerInvariant();
            long fileSize = upload.Length; //获得文件大小，以字节为单位
            //判断后缀是否是图片
            string[] permittedExtensions = new string[] { ".jpeg", ".jpg", ".png", ".bmp", ".svg", ".webp" };

            if (fileExt == null)
            {
                return Problem("上传的文件没有后缀");
            }
            if (!permittedExtensions.Contains(fileExt))
            {
                return Problem("不支持的图片格式");
            }
            if (fileSize > 1024 * 1024 * 1) //M
            {
                //上传的文件不能大于1M
                return Problem("上传的图片应小于1M");
            }

            string fileName = HashCrypto.Md5FileHash(upload.OpenReadStream());
            var blobPath = Path.Combine("images", DateTime.UtcNow.ToString("yyyy-MM-dd"), fileName + fileExt);

            // 上传云存储
            var url = await storageService.UploadAsync(upload.OpenReadStream(), blobPath);

            return new UploadResult()
            {
                FilePath = url,
                Url = url,
            };
        }
        return Problem("文件不正确", title: "业务错误");
    }


    /// <summary>
    /// ⚠删除
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    // [ApiExplorerSettings(IgnoreApi = true)]
    [HttpDelete("{id}")]
    public async Task<ActionResult<Blog?>> DeleteAsync([FromRoute] Guid id)
    {
        // TODO:实现删除逻辑,注意删除权限
        var entity = await manager.GetOwnedAsync(id);
        if (entity == null) return NotFound();
        return await manager.DeleteAsync(entity);
    }
}