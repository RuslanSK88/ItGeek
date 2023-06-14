using ItGeek.DAL.Data;
using ItGeek.DAL.Entities;
using ItGeek.DAL.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ItGeek.BLL.Repositories;

public class MenuItemRepository : GenericRepositoryAsync<MenuItem>, IMenuItemRepository
{
    private readonly AppDbContext _db;

    public MenuItemRepository(AppDbContext db) : base(db)
	{
        _db = db;
    }

    public async Task<List<MenuItem>> GetByMenuIdAsync(int id)
    {
        return await _db.MenuItems.Where(x=>x.MenuId == id).ToListAsync();
    }
}
