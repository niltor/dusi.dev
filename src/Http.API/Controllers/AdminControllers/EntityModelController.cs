using Entity.EntityDesign;
using Share.Models.EntityModelDtos;
namespace Http.API.Controllers.AdminControllers;

/// <summary>
/// 实体模型类
/// </summary>
public class EntityModelController : RestControllerBase<EntityModelManager>
{
    public EntityModelController(
        IUserContext user,
        ILogger<EntityModelController> logger,
        EntityModelManager manager
        ) : base(manager, user, logger)
    {
    }

    /// <summary>
    /// 筛选
    /// </summary>
    /// <param name="filter"></param>
    /// <returns></returns>
    [HttpPost("filter")]
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
        var entity = form.MapTo<EntityModelAddDto, EntityModel>();
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
    public async Task<ActionResult<EntityModel?>> GetDetailAsync([FromRoute] Guid id)
    {
        var res = await manager.FindAsync(id);
        return res == null ? NotFound() : res;
    }

    /// <summary>
    /// ⚠删除
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    // [ApiExplorerSettings(IgnoreApi = true)]
    [HttpDelete("{id}")]
    public async Task<ActionResult<EntityModel?>> DeleteAsync([FromRoute] Guid id)
    {
        var entity = await manager.GetCurrentAsync(id);
        if (entity == null) return NotFound();
        return await manager.DeleteAsync(entity);
    }
}