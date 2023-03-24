using Share.Models.BlogDtos;
namespace Dusi.Manage.Client.Services;
/// <summary>
/// 博客
/// </summary>
public class BlogService : BaseService
{
    public BlogService(HttpClient httpClient) : base(httpClient)
    {

    }
    /// <summary>
    /// 筛选
    /// </summary>
    /// <param name="data">BlogFilterDto</param>
    /// <returns></returns>
    public async Task<PageList<BlogItemDto>?> FilterAsync(BlogFilterDto data)
    {
        string url = $"/api/admin/Blog/filter";
        return await PostJsonAsync<PageList<BlogItemDto>?>(url, data);
    }

    /// <summary>
    /// 新增
    /// </summary>
    /// <param name="data">BlogAddDto</param>
    /// <returns></returns>
    public async Task<string?> AddAsync(BlogAddDto data)
    {
        string url = $"/api/admin/Blog";
        return await PostJsonAsync<string?>(url, data);
    }

}