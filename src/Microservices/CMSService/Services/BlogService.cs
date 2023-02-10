using Application.IManager;
using Core.Utils;
using Google.Protobuf.WellKnownTypes;
using Grpc.BlogService;
using Grpc.Core;
using Share.Models;
using Share.Models.BlogDtos;
using static Grpc.BlogService.Blog;

namespace CMSService.Services;

public class BlogService : BlogBase
{
    private readonly ILogger<BlogService> _logger;
    private readonly IBlogManager _manager;
    public BlogService(ILogger<BlogService> logger, IBlogManager manager)
    {
        _logger = logger;
        _manager = manager;
    }

    public override async Task<BlogReply> Add(AddRequest request, ServerCallContext context)
    {
        var entity = request.MapTo<AddRequest, Core.Entities.CMS.Blog>();
        await _manager.AddAsync(entity);

        var reply = entity.MapTo<Core.Entities.CMS.Blog, BlogReply>();
        reply.CreatedTime = Timestamp.FromDateTimeOffset(entity.CreatedTime);
        reply.UpdatedTime = Timestamp.FromDateTimeOffset(entity.UpdatedTime);
        return reply;
    }

    public override Task<BlogReply> Delete(IdRequest request, ServerCallContext context)
    {
        return base.Delete(request, context);
    }

    public override async Task<PageReply> Filter(FilterRequest request, ServerCallContext context)
    {
        var filter = request.MapTo<FilterRequest, BlogFilterDto>();
        var res = await _manager.FilterAsync(filter);

        var reply = res.MapTo<PageList<BlogItemDto>, PageReply>();
        return reply;
    }

    public override Task<BlogReply> Update(UpdateRequest request, ServerCallContext context)
    {
        return base.Update(request, context);
    }

    public override Task<BlogReply> Detail(IdRequest request, ServerCallContext context)
    {
        return base.Detail(request, context);
    }
}
