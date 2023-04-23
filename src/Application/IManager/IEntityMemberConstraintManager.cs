using Share.Models.EntityMemberConstraintDtos;

namespace Application.IManager;
/// <summary>
/// 定义实体业务接口规范
/// </summary>
public interface IEntityMemberConstraintManager : IDomainManager<EntityMemberConstraint>
{
    Task<EntityMemberConstraint?> GetCurrentAsync(Guid id, params string[] navigations);
    Task<EntityMemberConstraint> AddAsync(EntityMemberConstraint entity);
    Task<EntityMemberConstraint> UpdateAsync(EntityMemberConstraint entity, EntityMemberConstraintUpdateDto dto);
    Task<EntityMemberConstraint?> FindAsync(Guid id);
    Task<TDto?> FindAsync<TDto>(Expression<Func<EntityMemberConstraint, bool>>? whereExp) where TDto : class;
    Task<List<TDto>> ListAsync<TDto>(Expression<Func<EntityMemberConstraint, bool>>? whereExp) where TDto : class;
    Task<PageList<EntityMemberConstraintItemDto>> FilterAsync(EntityMemberConstraintFilterDto filter);
    Task<EntityMemberConstraint?> DeleteAsync(EntityMemberConstraint entity, bool softDelete = true);
    Task<bool> ExistAsync(Guid id);
    // TODO: 定义业务方法
}
