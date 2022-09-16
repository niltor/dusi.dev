using Share.Models.EntityModelDtos;

namespace Application.IManager;
/// <summary>
/// 定义实体业务接口规范
/// </summary>
public interface IEntityModelManager : IDomainManager<EntityModel, EntityModelUpdateDto, EntityModelFilterDto, EntityModelItemDto>
{
	// TODO: 定义业务方法
}
