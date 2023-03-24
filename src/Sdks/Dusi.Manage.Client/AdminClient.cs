using Dusi.Manage.Client.Services;
namespace Dusi.Manage.Client;

public class AdminClient
{
    private string BaseUrl { get; set; } = "";
    public string? AccessToken { get; set; }
    public HttpClient Http { get; set; }
    public JsonSerializerOptions JsonSerializerOptions { get; set; }
    public ErrorResult? ErrorMsg { get; set; } = null;

    #region api services
    public AuthService Auth { get; init; }
    public BlogService Blog { get; init; }
    public EntityLibraryService EntityLibrary { get; init; }
    public EntityMemberService EntityMember { get; init; }
    public EntityMemberConstraintService EntityMemberConstraint { get; init; }
    public EntityModelService EntityModel { get; init; }
    public SystemRoleService SystemRole { get; init; }
    public SystemUserService SystemUser { get; init; }
    public ThirdNewsService ThirdNews { get; init; }
    public UserService User { get; init; }

    #endregion

    public AdminClient(string? baseUrl = null)
    {
        BaseUrl = baseUrl ?? "";
        Http = new HttpClient()
        {
            BaseAddress = new Uri(BaseUrl),
            Timeout = TimeSpan.FromSeconds(10)
        };
        JsonSerializerOptions = new JsonSerializerOptions()
        {
            ReferenceHandler = ReferenceHandler.IgnoreCycles,
            Encoder = JavaScriptEncoder.Create(UnicodeRanges.All)
        };

        #region api services
        Auth = new AuthService(Http);
        Blog = new BlogService(Http);
        EntityLibrary = new EntityLibraryService(Http);
        EntityMember = new EntityMemberService(Http);
        EntityMemberConstraint = new EntityMemberConstraintService(Http);
        EntityModel = new EntityModelService(Http);
        SystemRole = new SystemRoleService(Http);
        SystemUser = new SystemUserService(Http);
        ThirdNews = new ThirdNewsService(Http);
        User = new UserService(Http);

        #endregion
    }

    public void SetToken(string token)
    {

        _ = Http.DefaultRequestHeaders.TryAddWithoutValidation("Authorization", "Bearer " + token);
    }

    public void SetBaseUrl(string url)
    {
        BaseUrl = url;
    }

}

public class ErrorResult
{
    public string? Title { get; set; }
    public string? Detail { get; set; }
    public int Status { get; set; } = 500;
    public string? TraceId { get; set; }
}
