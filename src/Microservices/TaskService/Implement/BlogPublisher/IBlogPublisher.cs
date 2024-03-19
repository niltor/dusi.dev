namespace TaskService.Implement.BlogPublisher;

/// <summary>
/// 发布博客
/// </summary>
public interface IBlogPublisher
{
    /// <summary>
    /// 接口地址
    /// </summary>
    public string BaseUrl { get; set; }

    /// <summary>
    /// 获取token
    /// </summary>
    /// <param name="option"></param>
    /// <returns></returns>
    public string GetToken(AuthOption option);
    /// <summary>
    /// 获取分类
    /// </summary>
    /// <returns></returns>
    public List<Catalog> GetCatalogs();
    /// <summary>
    /// 添加分类
    /// </summary>
    /// <param name="catalog"></param>
    /// <returns></returns>
    public bool AddCatalog(Catalog catalog);
    /// <summary>
    /// 添加博客
    /// </summary>
    /// <param name="blog"></param>
    /// <returns></returns>
    public bool AddBlog(Blog blog);

}

/// <summary>
/// 验证选项
/// </summary>
public class AuthOption
{
    /// <summary>
    /// username/key/appid
    /// </summary>
    public required string Key { get; set; }
    /// <summary>
    /// password/secret
    /// </summary>
    public required string Secret { get; set; }
}

public class Catalog
{
    public string? Description { get; set; }
    public required string Title { get; set; }

    public required string Id { get; set; }
}
public class Blog
{
    public required string Title { get; set; }
    public required string Description { get; set; }
    public List<string>? Categories { get; set; }
}
