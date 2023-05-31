using ItGeek.DAL.Enums;

namespace ItGeek.DAL.Entities;

public class Role : BaseEntity
{
	public RoleName RoleName { get; set; }
	public List<User> Users { get; set; }
}
