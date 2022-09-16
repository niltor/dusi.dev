using Share.Models.EntityMemberConstraintDtos;
namespace Http.API.Infrastructure;

/// <summary>
/// 属性的约束
/// </summary>
public class EntityMemberConstraintController : RestControllerBase<IEntityMemberConstraintManager>
{
    public EntityMemberConstraintController(
        IUserContext user,
        ILogger<EntityMemberConstraintController> logger,
        IEntityMemberConstraintManager manager
        ) : base(manager, user, logger)
    {
    }

    /// <summary>
    /// 筛选
    /// </summary>
    /// <param name="filter"></param>
    /// <returns></returns>
    [HttpPost("filter")]
    public async Task<ActionResult<PageList<EntityMemberConstraintItemDto>>> FilterAsync(EntityMemberConstraintFilterDto filter)
    {
        return await manager.FilterAsync(filter);
    }

    /// <summary>
    /// 新增
    /// </summary>
    /// <param name="form"></param>
    /// <returns></returns>
    [HttpPost]
    public async Task<ActionResult<EntityMemberConstraint>> AddAsync(EntityMemberConstraintAddDto form)
    {
        var entity = form.MapTo<EntityMemberConstraintAddDto, EntityMemberConstraint>();
        return await manager.AddAsync(entity);
    }

    /// <summary>
    /// 更新
    /// </summary>
    /// <param name="id"></param>
    /// <param name="form"></param>
    /// <returns></returns>
    [HttpPut("{id}")]
    public async Task<ActionResult<EntityMemberConstraint?>> UpdateAsync([FromRoute] Guid id, EntityMemberConstraintUpdateDto form)
    {
        var current = await manager.GetCurrent(id);
        if (current == null) return NotFound();
        return await manager.UpdateAsync(current, form);
    }

    /// <summary>
    /// 详情
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpGet("{id}")]
    public async Task<ActionResult<EntityMemberConstraint?>> GetDetailAsync([FromRoute] Guid id)
    {
        var res = await manager.FindAsync(id);
        return (res == null) ? NotFound() : res;
    }

    /// <summary>
    /// ⚠删除
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    // [ApiExplorerSettings(IgnoreApi = true)]
    [HttpDelete("{id}")]
    public async Task<ActionResult<EntityMemberConstraint?>> DeleteAsync([FromRoute] Guid id)
    {
        var entity = await manager.GetCurrent(id);
        if (entity == null) return NotFound();
        return await manager.DeleteAsync(entity);
    }
}