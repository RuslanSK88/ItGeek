namespace ItGeek.DAL.Entities;

public class UserProfile : BaseEntity
{
    public int UserId { get; set; }
    public virtual User? User { get; set; }
	public string FirstName { get; set; }
    public string LastName { get; set; }
    public DateTime Birthday { get; set; }
}
