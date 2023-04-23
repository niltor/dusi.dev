using Share.Models.EntityModelDtos;

namespace Application.IManager;
/// <summary>
/// 定义实体业务接口规范
/// </summary>
public interface IEntityModelManager : IDomainManager<EntityModel>
{
    Task<EntityModel?> GetCurrentAsync(Guid id, params string[] navigations);
    Task<EntityModel> AddAsync(EntityModel entity);
    Task<EntityModel> UpdateAsync(EntityModel entity, EntityModelUpdateDto dto);
    Task<EntityModel?> FindAsync(Guid id);
    Task<TDto?> FindAsync<TDto>(Expression<Func<EntityModel, bool>>? whereExp) where TDto : class;
    Task<List<TDto>> ListAsync<TDto>(Expression<Func<EntityModel, bool>>? whereExp) where TDto : class;
    Task<PageList<EntityModelItemDto>> FilterAsync(EntityModelFilterDto filter);
    Task<EntityModel?> DeleteAsync(EntityModel entity, bool softDelete = true);
    Task<bool> ExistAsync(Guid id);
    // TODO: 定义业务方法
}
