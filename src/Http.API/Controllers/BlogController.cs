using Entity.CMS;
using Share.Models;
using Share.Models.BlogDtos;

namespace Http.API.Controllers;

/// <summary>
/// 博客
/// </summary>
public class BlogController : ClientControllerBase<BlogManager>
{
    private readonly StorageService storageService;
    private readonly CatalogManager _catalogManager;

    public BlogController(
        IUserContext user,
        ILogger<BlogController> logger,
        BlogManager manager,
        StorageService storageService,
        CatalogManager catalogManager) : base(manager, user, logger)
    {
        this.storageService = storageService;
        _catalogManager = catalogManager;
    }

    /// <summary>
    /// 筛选
    /// </summary>
    /// <param name="filter"></param>
    /// <returns></returns>
    [HttpPost("filter")]
    public async Task<ActionResult<PageList<BlogItemDto>>> GetMyBlogsAsync(BlogFilterDto filter)
    {
        filter.UserId = _user.UserId;
        return await manager.FilterAsync(filter);
    }

    /// <summary>
    /// 公开博客
    /// </summary>
    /// <param name="filter"></param>
    /// <returns></returns>
    [HttpPost("public")]
    [AllowAnonymous]
    public async Task<ActionResult<PageList<BlogItemDto>>> PublicBlogsAsync(BlogFilterDto filter)
    {
        filter.IsPublic = true;
        filter.UserId = null;
        return await manager.FilterAsync(filter);
    }

    /// <summary>
    /// 获取分类信息
    /// </summary>
    /// <returns></returns>
    [HttpGet("types")]
    [AllowAnonymous]
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
    /// <param name="dto"></param>
    /// <returns></returns>
    [HttpPost]
    public async Task<ActionResult<Blog>> AddAsync(BlogAddDto dto)
    {
        if (!await _user.ExistAsync())
            return NotFound(ErrorMsg.NotFoundUser);

        if (!await _catalogManager.ExistAsync(dto.CatalogId))
            return NotFound("不存在的目录");
        var entity = await manager.CreateNewEntityAsync(dto);
        return await manager.AddAsync(entity);
    }

    /// <summary>
    /// 更新
    /// </summary>
    /// <param name="id"></param>
    /// <param name="dto"></param>
    /// <returns></returns>
    [HttpPut("{id}")]
    public async Task<ActionResult<Blog?>> UpdateAsync([FromRoute] Guid id, BlogUpdateDto dto)
    {
        var current = await manager.GetOwnedAsync(id);
        if (current == null) return NotFound(ErrorMsg.NotFoundResource);

        // 修改了所属目录
        if (current.Catalog.Id != dto.CatalogId)
        {
            var catalog = await _catalogManager.GetCurrentAsync(dto.CatalogId);
            if (catalog == null) return NotFound("不存在的目录");
            current.Catalog = catalog;
        }
        return await manager.UpdateAsync(current, dto);
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
            string[] permittedExtensions = [".jpeg", ".jpg", ".png", ".bmp", ".svg", ".webp"];

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