using Share.Models.EntityMemberDtos;
namespace Dusi.Manage.Client.Services;
/// <summary>
/// 实体属性
/// </summary>
public class EntityMemberService : BaseService
{
    public EntityMemberService(HttpClient httpClient) : base(httpClient)
    {

    }
    /// <summary>
    /// 筛选
    /// </summary>
    /// <param name="data">EntityMemberFilterDto</param>
    /// <returns></returns>
    public async Task<PageList<EntityMemberItemDto>?> Filter(EntityMemberFilterDto data) {
        var url = $"/api/admin/EntityMember/filter";
        return await PostJsonAsync<PageList<EntityMemberItemDto>?>(url, data);
    }

    /// <summary>
    /// 新增
    /// </summary>
    /// <param name="data">EntityMemberAddDto</param>
    /// <returns></returns>
    public async Task<EntityMember?> Add(EntityMemberAddDto data) {
        var url = $"/api/admin/EntityMember";
        return await PostJsonAsync<EntityMember?>(url, data);
    }

    /// <summary>
    /// 更新
    /// </summary>
    /// <param name="id"> </param>
    /// <param name="data">EntityMemberUpdateDto</param>
    /// <returns></returns>
    public async Task<EntityMember?> Update(string id, EntityMemberUpdateDto data) {
        var url = $"/api/admin/EntityMember/{id}";
        return await PutJsonAsync<EntityMember?>(url, data);
    }

    /// <summary>
    /// 详情
    /// </summary>
    /// <param name="id"> </param>
    /// <returns></returns>
    public async Task<EntityMember?> GetDetail(string id) {
        var url = $"/api/admin/EntityMember/{id}";
        return await GetJsonAsync<EntityMember?>(url);
    }

    /// <summary>
    /// ⚠删除
    /// </summary>
    /// <param name="id"> </param>
    /// <returns></returns>
    public async Task<EntityMember?> Delete(string id) {
        var url = $"/api/admin/EntityMember/{id}";
        return await DeleteJsonAsync<EntityMember?>(url);
    }

}