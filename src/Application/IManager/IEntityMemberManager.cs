using Share.Models.EntityMemberDtos;

namespace Application.IManager;
/// <summary>
/// 定义实体业务接口规范
/// </summary>
public interface IEntityMemberManager : IDomainManager<EntityMember>
{
    Task<EntityMember?> GetCurrentAsync(Guid id, params string[] navigations);
    Task<EntityMember> AddAsync(EntityMember entity);
    Task<EntityMember> UpdateAsync(EntityMember entity, EntityMemberUpdateDto dto);
    Task<EntityMember?> FindAsync(Guid id);
    Task<TDto?> FindAsync<TDto>(Expression<Func<EntityMember, bool>>? whereExp) where TDto : class;
    Task<List<TDto>> ListAsync<TDto>(Expression<Func<EntityMember, bool>>? whereExp) where TDto : class;
    Task<PageList<EntityMemberItemDto>> FilterAsync(EntityMemberFilterDto filter);
    Task<EntityMember?> DeleteAsync(EntityMember entity, bool softDelete = true);
    Task<bool> ExistAsync(Guid id);
    // TODO: 定义业务方法
}
