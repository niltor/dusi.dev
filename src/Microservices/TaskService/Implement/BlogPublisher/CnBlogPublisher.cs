using Ater.MetaWeBlog;
using Ater.MetaWeBlog.Options;
using Google.Protobuf.WellKnownTypes;
using TaskService.Implement.PostBlog;

namespace TaskService.Implement.BlogPublisher;

public class CnBlogPublisher : BlogPublisher
{

    public Client Client { get; set; }

    public CnBlogPublisher(string blogid, string username, string pat) : base("")
    {
        var option = new CnBlogsOption(blogid, username, pat);
        Client = new Client(option);
    }


    public override List<Catalog> GetCatalogs()
    {
        var res = new List<Catalog>();
        var catalogs = Client.GetCategories();

        catalogs.ForEach(c =>
        {
            var item = new Catalog
            {
                Id = c.CategoryID,
                Title = c.Title,
            };
            res.Add(item);
        });
        return res;
    }

    public override bool AddCatalog(Catalog catalog)
    {
        return base.AddCatalog(catalog);
    }

    public override bool AddBlog(Blog blog)
    {
        //Client.NewPost()
        return base.AddBlog(blog);
    }
}
