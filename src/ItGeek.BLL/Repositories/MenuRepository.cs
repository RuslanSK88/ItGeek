using ItGeek.DAL.Data;
using ItGeek.DAL.Entities;
using ItGeek.DAL.Interfaces;

namespace ItGeek.BLL.Repositories;

public class MenuRepository : GenericRepositoryAsync<Menu>, IMenuRepository
{
	public MenuRepository(AppDbContext db) : base(db)
	{
	}
}
