using ItGeek.DAL.Data;
using ItGeek.DAL.Entities;
using ItGeek.DAL.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ItGeek.BLL.Repositories;

public class TagRepository : GenericRepositoryAsync<Tag>, ITagRepository
{
    private readonly AppDbContext _db;

    public TagRepository(AppDbContext db) : base(db)
	{
        _db = db;
    }

	public async Task<List<Tag>> GetTagByNameAsync(string tagName)
	{
		return await _db.Tags.Where(x => x.Name.Contains(tagName)).ToListAsync();
	}
}
