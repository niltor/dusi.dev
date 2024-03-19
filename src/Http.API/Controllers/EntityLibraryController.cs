
using Entity.EntityDesign;
using Share.Models.EntityLibraryDtos;
namespace Http.API.Controllers;

/// <summary>
/// 实体库
/// </summary>
public class EntityLibraryController : ClientControllerBase<EntityLibraryManager>
{
    public EntityLibraryController(
        IUserContext user,
        ILogger<EntityLibraryController> logger,
        EntityLibraryManager manager
        ) : base(manager, user, logger)
    {
    }

    /// <summary>
    /// 筛选
    /// </summary>
    /// <param name="filter"></param>
    /// <returns></returns>
    [HttpPost("filter")]
    [AllowAnonymous]
    public async Task<ActionResult<PageList<EntityLibraryItemDto>>> FilterAsync(EntityLibraryFilterDto filter)
    {
        return await manager.FilterAsync(filter);
    }

    /// <summary>
    /// 新增
    /// </summary>
    /// <param name="form"></param>
    /// <returns></returns>
    [HttpPost]
    public async Task<ActionResult<EntityLibrary>> AddAsync(EntityLibraryAddDto form)
    {
        var user = await _user.GetUserAsync<User>();
        if (user == null) { return NotFound(ErrorMsg.NotFoundUser); }
        var entity = form.MapTo<EntityLibraryAddDto, EntityLibrary>();
        entity.User = user;
        return await manager.AddAsync(entity);
    }

    /// <summary>
    /// 更新
    /// </summary>
    /// <param name="id"></param>
    /// <param name="form"></param>
    /// <returns></returns>
    [HttpPut("{id}")]
    public async Task<ActionResult<EntityLibrary?>> UpdateAsync([FromRoute] Guid id, EntityLibraryUpdateDto form)
    {
        var current = await manager.GetCurrentAsync(id);
        if (current == null) return NotFound();
        return await manager.UpdateAsync(current, form);
    }

    /// <summary>
    /// 详情
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpGet("{id}")]
    public async Task<ActionResult<EntityLibrary?>> GetDetailAsync([FromRoute] Guid id)
    {
        var res = await manager.FindAsync(id);
        return res == null ? NotFound() : res;
    }

    /// <summary>
    /// ⚠删除
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpDelete("{id}")]
    public async Task<ActionResult<EntityLibrary?>> DeleteAsync([FromRoute] Guid id)
    {
        var entity = await manager.GetCurrentAsync(id);
        if (entity == null) return NotFound();
        return await manager.DeleteAsync(entity);
    }
}