using ItGeek.DAL.Entities;

namespace ItGeek.Web.Models;

public class PostContentViewModel
{
    public Post post { get; set; }
    public PostContent postContent { get; set; }
    public Category category { get; set; }
    public List<Post>? posts { get; set; }
    public List<PostContent> postContents { get; set; }
}
