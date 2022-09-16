namespace Application.Implement;
public class DataStoreContext
{
    public QueryDbContext QueryContext { get; init; }
    public CommandDbContext CommandContext { get; init; }

    public QuerySet<EntityLibrary> EntityLibraryQuery { get; init; }
    public QuerySet<EntityMemberConstraint> EntityMemberConstraintQuery { get; init; }
    public QuerySet<EntityMember> EntityMemberQuery { get; init; }
    public QuerySet<EntityModel> EntityModelQuery { get; init; }
    public QuerySet<Role> RoleQuery { get; init; }
    public QuerySet<User> UserQuery { get; init; }
    public CommandSet<EntityLibrary> EntityLibraryCommand { get; init; }
    public CommandSet<EntityMember> EntityMemberCommand { get; init; }
    public CommandSet<EntityMemberConstraint> EntityMemberConstraintCommand { get; init; }
    public CommandSet<EntityModel> EntityModelCommand { get; init; }
    public CommandSet<Role> RoleCommand { get; init; }
    public CommandSet<User> UserCommand { get; init; }


    /// <summary>
    /// 绑在对象
    /// </summary>
    private readonly Dictionary<string, object> SetCache = new();

    public DataStoreContext(
        EntityLibraryQueryStore entityLibraryQuery,
        EntityMemberConstraintQueryStore entityMemberConstraintQuery,
        EntityMemberQueryStore entityMemberQuery,
        EntityModelQueryStore entityModelQuery,
        RoleQueryStore roleQuery,
        UserQueryStore userQuery,
        EntityLibraryCommandStore entityLibraryCommand,
        EntityMemberCommandStore entityMemberCommand,
        EntityMemberConstraintCommandStore entityMemberConstraintCommand,
        EntityModelCommandStore entityModelCommand,
        RoleCommandStore roleCommand,
        UserCommandStore userCommand,

        QueryDbContext queryDbContext,
        CommandDbContext commandDbContext
    )
    {
        QueryContext = queryDbContext;
        CommandContext = commandDbContext;
        EntityLibraryQuery = entityLibraryQuery;
        AddCache(EntityLibraryQuery);
        EntityMemberConstraintQuery = entityMemberConstraintQuery;
        AddCache(EntityMemberConstraintQuery);
        EntityMemberQuery = entityMemberQuery;
        AddCache(EntityMemberQuery);
        EntityModelQuery = entityModelQuery;
        AddCache(EntityModelQuery);
        RoleQuery = roleQuery;
        AddCache(RoleQuery);
        UserQuery = userQuery;
        AddCache(UserQuery);
        EntityLibraryCommand = entityLibraryCommand;
        AddCache(EntityLibraryCommand);
        EntityMemberCommand = entityMemberCommand;
        AddCache(EntityMemberCommand);
        EntityMemberConstraintCommand = entityMemberConstraintCommand;
        AddCache(EntityMemberConstraintCommand);
        EntityModelCommand = entityModelCommand;
        AddCache(EntityModelCommand);
        RoleCommand = roleCommand;
        AddCache(RoleCommand);
        UserCommand = userCommand;
        AddCache(UserCommand);

    }

    public async Task<int> SaveChangesAsync()
    {
        return await CommandContext.SaveChangesAsync();
    }

    public QuerySet<TEntity> QuerySet<TEntity>() where TEntity : EntityBase
    {
        var typename = typeof(TEntity).Name + "QueryStore";
        var set = GetSet(typename);
        if (set == null) throw new ArgumentNullException($"{typename} class object not found");
        return (QuerySet<TEntity>)set;
    }
    public CommandSet<TEntity> CommandSet<TEntity>() where TEntity : EntityBase
    {
        var typename = typeof(TEntity).Name + "CommandStore";
        var set = GetSet(typename);
        if (set == null) throw new ArgumentNullException($"{typename} class object not found");
        return (CommandSet<TEntity>)set;
    }

    private void AddCache(object set)
    {
        var typeName = set.GetType().Name;
        if (!SetCache.ContainsKey(typeName))
        {
            SetCache.Add(typeName, set);
        }
    }

    private object GetSet(string type)
    {
        return SetCache[type];
    }
}
