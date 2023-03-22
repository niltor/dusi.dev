using Application.IManager;
using Dapr.AppCallback.Autogen.Grpc.v1;
namespace CMSService.Services;

public class BlogService : AppCallback.AppCallbackBase
{
    private readonly ILogger<BlogService> _logger;
    private readonly IBlogManager _manager;
    public BlogService(ILogger<BlogService> logger, IBlogManager manager)
    {
        _logger = logger;
        _manager = manager;
    }


}
