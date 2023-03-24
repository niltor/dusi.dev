using Core.Entities.EntityDesign;
using Share.Models.EntityMemberConstraintDtos;
namespace Dusi.Manage.Client.Services;
/// <summary>
/// 属性的约束
/// </summary>
public class EntityMemberConstraintService : BaseService
{
    public EntityMemberConstraintService(HttpClient httpClient) : base(httpClient)
    {

    }
    /// <summary>
    /// 筛选
    /// </summary>
    /// <param name="data">EntityMemberConstraintFilterDto</param>
    /// <returns></returns>
    public async Task<PageList<EntityMemberConstraintItemDto>?> Filter(EntityMemberConstraintFilterDto data) {
        var url = $"/api/admin/EntityMemberConstraint/filter";
        return await PostJsonAsync<PageList<EntityMemberConstraintItemDto>?>(url, data);
    }

    /// <summary>
    /// 新增
    /// </summary>
    /// <param name="data">EntityMemberConstraintAddDto</param>
    /// <returns></returns>
    public async Task<EntityMemberConstraint?> Add(EntityMemberConstraintAddDto data) {
        var url = $"/api/admin/EntityMemberConstraint";
        return await PostJsonAsync<EntityMemberConstraint?>(url, data);
    }

    /// <summary>
    /// 更新
    /// </summary>
    /// <param name="id"> </param>
    /// <param name="data">EntityMemberConstraintUpdateDto</param>
    /// <returns></returns>
    public async Task<EntityMemberConstraint?> Update(string id, EntityMemberConstraintUpdateDto data) {
        var url = $"/api/admin/EntityMemberConstraint/{id}";
        return await PutJsonAsync<EntityMemberConstraint?>(url, data);
    }

    /// <summary>
    /// 详情
    /// </summary>
    /// <param name="id"> </param>
    /// <returns></returns>
    public async Task<EntityMemberConstraint?> GetDetail(string id) {
        var url = $"/api/admin/EntityMemberConstraint/{id}";
        return await GetJsonAsync<EntityMemberConstraint?>(url);
    }

    /// <summary>
    /// ⚠删除
    /// </summary>
    /// <param name="id"> </param>
    /// <returns></returns>
    public async Task<EntityMemberConstraint?> Delete(string id) {
        var url = $"/api/admin/EntityMemberConstraint/{id}";
        return await DeleteJsonAsync<EntityMemberConstraint?>(url);
    }

}