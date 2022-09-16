using Share.Models.EntityLibraryDtos;

namespace Application.IManager;
/// <summary>
/// 定义实体业务接口规范
/// </summary>
public interface IEntityLibraryManager : IDomainManager<EntityLibrary, EntityLibraryUpdateDto, EntityLibraryFilterDto, EntityLibraryItemDto>
{
	// TODO: 定义业务方法
}
