using Application.IManager;
using Core.Utils;
using Dapr.AppCallback.Autogen.Grpc.v1;
using Dapr.Client.Autogen.Grpc.v1;
using Google.Protobuf.WellKnownTypes;
using Grpc.BlogService;
using Grpc.Core;
using Share.Models;
using Share.Models.BlogDtos;

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

    public override Task<InvokeResponse> OnInvoke(InvokeRequest request, ServerCallContext context)
    {

        var response = new InvokeResponse();
        Console.WriteLine("123");
        switch (request.Method)
        {
            case "add":
                var input = request.Data.Unpack<AddRequest>();
                //_manager.AddAsync(input);
                response.Data = Any.Pack(new BlogReply() { Title = "222" });
                break;
            default:
                break;
        }

        return Task.FromResult(response);
    }

}
