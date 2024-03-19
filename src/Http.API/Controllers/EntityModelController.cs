using Entity.EntityDesign;
using Share.Models.EntityModelDtos;
namespace Http.API.Controllers;

/// <summary>
/// 实体模型类
/// </summary>
public class EntityModelController(
    IUserContext user,
    ILogger<EntityModelController> logger,
    EntityModelManager manager,
    EntityLibraryManager libraryManager) : ClientControllerBase<EntityModelManager>(manager, user, logger)
{

    private readonly EntityLibraryManager _libraryManager = libraryManager;

    /// <summary>
    /// 筛选
    /// </summary>
    /// <param name="filter"></param>
    /// <returns></returns>
    [HttpPost("filter")]
    [AllowAnonymous]
    public async Task<ActionResult<PageList<EntityModelItemDto>>> FilterAsync(EntityModelFilterDto filter)
    {
        return await manager.FilterAsync(filter);
    }

    /// <summary>
    /// 新增
    /// </summary>
    /// <param name="form"></param>
    /// <returns></returns>
    [HttpPost]
    public async Task<ActionResult<EntityModel>> AddAsync(EntityModelAddDto form)
    {
        User? user = await _user.GetUserAsync<User>();
        if (user == null) { return NotFound(ErrorMsg.NotFoundUser); }
        EntityLibrary? lib = await _libraryManager.GetCurrentAsync(form.EntityLibraryId);
        if (lib == null) return NotFound(ErrorMsg.NotFoundEntityLib);
        EntityModel entity = form.MapTo<EntityModelAddDto, EntityModel>();

        entity.User = user;
        entity.EntityLibrary = lib;
        return await manager.AddAsync(entity);
    }

    /// <summary>
    /// 更新
    /// </summary>
    /// <param name="id"></param>
    /// <param name="form"></param>
    /// <returns></returns>
    [HttpPut("{id}")]
    public async Task<ActionResult<EntityModel?>> UpdateAsync([FromRoute] Guid id, EntityModelUpdateDto form)
    {
        EntityModel? current = await manager.GetCurrentAsync(id);
        if (current == null) return NotFound(ErrorMsg.NotFoundResource);
        return await manager.UpdateAsync(current, form);
    }

    /// <summary>
    /// 详情
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpGet("{id}")]
    public async Task<ActionResult<EntityModel?>> GetDetailAsync([FromRoute] Guid id)
    {
        EntityModel? res = await manager.FindAsync(id);
        return res == null ? NotFound() : res;
    }


    /// <summary>
    /// ⚠删除
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpDelete("{id}")]
    public async Task<ActionResult<EntityModel?>> DeleteAsync([FromRoute] Guid id)
    {
        EntityModel? entity = await manager.GetCurrentAsync(id);
        if (entity == null) return NotFound();
        return await manager.DeleteAsync(entity);
    }
}