using ItGeek.DAL.Data;
using ItGeek.DAL.Entities;
using ItGeek.DAL.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ItGeek.BLL.Repositories;

public class CategoryRepository : GenericRepositoryAsync<Category>, ICategoryRepository
{
    private readonly AppDbContext _db;

    public CategoryRepository(AppDbContext db) : base(db)
    {
        _db = db;
    }

    public async Task<Category> GetBySlugAsync(string slug)
    {
        return await _db.Categories.Include(x => x.Posts).ThenInclude(i => i.PostContents).Where(x => x.Slug == slug).FirstAsync();
    }
}
