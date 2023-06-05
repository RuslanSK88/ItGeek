namespace ItGeek.DAL.Entities;

public class Post : BaseEntity
{
    public string Slug { get; set; }
	public DateTime CreatedAt { get; set; }
	public DateTime EditedAt { get; set; }
	public User? CreatedBy { get; set; }
	public User? EditedBy { get; set; }
	public bool IsDeleted { get; set; } = false;

	public List<Author> Authors { get; } = new();
	public List<Category> Categories { get; } = new();
	public List<Tag> Tags { get; } = new();
	public List<Comment> Comments { get; } = new();
}
