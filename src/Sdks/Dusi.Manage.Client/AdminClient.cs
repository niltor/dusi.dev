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
    public AuthService AuthService { get; init; }
    public BlogService BlogService { get; init; }
    public EntityLibraryService EntityLibraryService { get; init; }
    public EntityMemberService EntityMemberService { get; init; }
    public EntityMemberConstraintService EntityMemberConstraintService { get; init; }
    public EntityModelService EntityModelService { get; init; }
    public SystemRoleService SystemRoleService { get; init; }
    public SystemUserService SystemUserService { get; init; }
    public ThirdNewsService ThirdNewsService { get; init; }
    public UserService UserService { get; init; }

    #endregion

    public AdminClient()
    {
        Http = new HttpClient()
        {
            BaseAddress = new Uri(BaseUrl),
        };
        JsonSerializerOptions = new JsonSerializerOptions()
        {
            ReferenceHandler = ReferenceHandler.IgnoreCycles,
            Encoder = JavaScriptEncoder.Create(UnicodeRanges.All)
        };

        #region api services
        AuthService = new AuthService(Http);
        BlogService = new BlogService(Http);
        EntityLibraryService = new EntityLibraryService(Http);
        EntityMemberService = new EntityMemberService(Http);
        EntityMemberConstraintService = new EntityMemberConstraintService(Http);
        EntityModelService = new EntityModelService(Http);
        SystemRoleService = new SystemRoleService(Http);
        SystemUserService = new SystemUserService(Http);
        ThirdNewsService = new ThirdNewsService(Http);
        UserService = new UserService(Http);

        #endregion
    }

    public void SetBaseUrl(string url)
    {
        this.BaseUrl = url;
    }

}

public class ErrorResult
{
    public string? Title { get; set; }
    public string? Detail { get; set; }
    public int Status { get; set; } = 500;
    public string? TraceId { get; set; }
}
