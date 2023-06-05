namespace ItGeek.DAL.Entities;

public class Tag : BaseEntity
{
	public string Name { get; set; }
	public string Description { get; set; }
	public string Slug { get; set; }
	public string TagImage { get; set; }

	public List<Post> Posts { get; } = new();
}
