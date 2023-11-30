using Entity.EntityDesign;
namespace EntityFramework;
public class DataStoreContext
{
    public QueryDbContext QueryContext { get; init; }
    public CommandDbContext CommandContext { get; init; }

    public QuerySet<Blog> BlogQuery { get; init; }
    public QuerySet<Catalog> CatalogQuery { get; init; }
    public QuerySet<EntityLibrary> EntityLibraryQuery { get; init; }
    public QuerySet<EntityMemberConstraint> EntityMemberConstraintQuery { get; init; }
    public QuerySet<EntityMember> EntityMemberQuery { get; init; }
    public QuerySet<EntityModel> EntityModelQuery { get; init; }
    public QuerySet<OpenSourceProduct> OpenSourceProductQuery { get; init; }
    public QuerySet<SystemLogs> SystemLogsQuery { get; init; }
    public QuerySet<SystemRole> SystemRoleQuery { get; init; }
    public QuerySet<SystemUser> SystemUserQuery { get; init; }
    public QuerySet<Tags> TagsQuery { get; init; }
    public QuerySet<ThirdNews> ThirdNewsQuery { get; init; }
    public QuerySet<ThirdVideo> ThirdVideoQuery { get; init; }
    public QuerySet<User> UserQuery { get; init; }
    public CommandSet<Blog> BlogCommand { get; init; }
    public CommandSet<Catalog> CatalogCommand { get; init; }
    public CommandSet<EntityLibrary> EntityLibraryCommand { get; init; }
    public CommandSet<EntityMember> EntityMemberCommand { get; init; }
    public CommandSet<EntityMemberConstraint> EntityMemberConstraintCommand { get; init; }
    public CommandSet<EntityModel> EntityModelCommand { get; init; }
    public CommandSet<OpenSourceProduct> OpenSourceProductCommand { get; init; }
    public CommandSet<SystemLogs> SystemLogsCommand { get; init; }
    public CommandSet<SystemRole> SystemRoleCommand { get; init; }
    public CommandSet<SystemUser> SystemUserCommand { get; init; }
    public CommandSet<Tags> TagsCommand { get; init; }
    public CommandSet<ThirdNews> ThirdNewsCommand { get; init; }
    public CommandSet<ThirdVideo> ThirdVideoCommand { get; init; }
    public CommandSet<User> UserCommand { get; init; }


    /// <summary>
    /// 绑在对象
    /// </summary>
    private readonly Dictionary<string, object> SetCache = [];

    public DataStoreContext(
        BlogQueryStore blogQuery,
        CatalogQueryStore catalogQuery,
        EntityLibraryQueryStore entityLibraryQuery,
        EntityMemberConstraintQueryStore entityMemberConstraintQuery,
        EntityMemberQueryStore entityMemberQuery,
        EntityModelQueryStore entityModelQuery,
        OpenSourceProductQueryStore openSourceProductQuery,
        SystemLogsQueryStore systemLogsQuery,
        SystemRoleQueryStore systemRoleQuery,
        SystemUserQueryStore systemUserQuery,
        TagsQueryStore tagsQuery,
        ThirdNewsQueryStore thirdNewsQuery,
        ThirdVideoQueryStore thirdVideoQuery,
        UserQueryStore userQuery,
        BlogCommandStore blogCommand,
        CatalogCommandStore catalogCommand,
        EntityLibraryCommandStore entityLibraryCommand,
        EntityMemberCommandStore entityMemberCommand,
        EntityMemberConstraintCommandStore entityMemberConstraintCommand,
        EntityModelCommandStore entityModelCommand,
        OpenSourceProductCommandStore openSourceProductCommand,
        SystemLogsCommandStore systemLogsCommand,
        SystemRoleCommandStore systemRoleCommand,
        SystemUserCommandStore systemUserCommand,
        TagsCommandStore tagsCommand,
        ThirdNewsCommandStore thirdNewsCommand,
        ThirdVideoCommandStore thirdVideoCommand,
        UserCommandStore userCommand,

        QueryDbContext queryDbContext,
        CommandDbContext commandDbContext
    )
    {
        QueryContext = queryDbContext;
        CommandContext = commandDbContext;
        BlogQuery = blogQuery;
        AddCache(BlogQuery);
        CatalogQuery = catalogQuery;
        AddCache(CatalogQuery);
        EntityLibraryQuery = entityLibraryQuery;
        AddCache(EntityLibraryQuery);
        EntityMemberConstraintQuery = entityMemberConstraintQuery;
        AddCache(EntityMemberConstraintQuery);
        EntityMemberQuery = entityMemberQuery;
        AddCache(EntityMemberQuery);
        EntityModelQuery = entityModelQuery;
        AddCache(EntityModelQuery);
        OpenSourceProductQuery = openSourceProductQuery;
        AddCache(OpenSourceProductQuery);
        SystemLogsQuery = systemLogsQuery;
        AddCache(SystemLogsQuery);
        SystemRoleQuery = systemRoleQuery;
        AddCache(SystemRoleQuery);
        SystemUserQuery = systemUserQuery;
        AddCache(SystemUserQuery);
        TagsQuery = tagsQuery;
        AddCache(TagsQuery);
        ThirdNewsQuery = thirdNewsQuery;
        AddCache(ThirdNewsQuery);
        ThirdVideoQuery = thirdVideoQuery;
        AddCache(ThirdVideoQuery);
        UserQuery = userQuery;
        AddCache(UserQuery);
        BlogCommand = blogCommand;
        AddCache(BlogCommand);
        CatalogCommand = catalogCommand;
        AddCache(CatalogCommand);
        EntityLibraryCommand = entityLibraryCommand;
        AddCache(EntityLibraryCommand);
        EntityMemberCommand = entityMemberCommand;
        AddCache(EntityMemberCommand);
        EntityMemberConstraintCommand = entityMemberConstraintCommand;
        AddCache(EntityMemberConstraintCommand);
        EntityModelCommand = entityModelCommand;
        AddCache(EntityModelCommand);
        OpenSourceProductCommand = openSourceProductCommand;
        AddCache(OpenSourceProductCommand);
        SystemLogsCommand = systemLogsCommand;
        AddCache(SystemLogsCommand);
        SystemRoleCommand = systemRoleCommand;
        AddCache(SystemRoleCommand);
        SystemUserCommand = systemUserCommand;
        AddCache(SystemUserCommand);
        TagsCommand = tagsCommand;
        AddCache(TagsCommand);
        ThirdNewsCommand = thirdNewsCommand;
        AddCache(ThirdNewsCommand);
        ThirdVideoCommand = thirdVideoCommand;
        AddCache(ThirdVideoCommand);
        UserCommand = userCommand;
        AddCache(UserCommand);

    }

    public async Task<int> SaveChangesAsync()
    {
        return await CommandContext.SaveChangesAsync();
    }

    public QuerySet<TEntity> QuerySet<TEntity>() where TEntity : class, IEntityBase
    {
        var typename = typeof(TEntity).Name + "QueryStore";
        var set = GetSet(typename);
        return set == null
            ? throw new ArgumentNullException($"{typename} class object not found")
            : (QuerySet<TEntity>)set;
    }
    public CommandSet<TEntity> CommandSet<TEntity>() where TEntity : class, IEntityBase
    {
        var typename = typeof(TEntity).Name + "CommandStore";
        var set = GetSet(typename);
        return set == null
            ? throw new ArgumentNullException($"{typename} class object not found")
            : (CommandSet<TEntity>)set;
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