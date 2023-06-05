using ItGeek.DAL.Data;
using ItGeek.DAL.Entities;
using ItGeek.DAL.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ItGeek.BLL.Repositories;

public class PostContentRepository : GenericRepositoryAsync<PostContent>, IPostContentRepository
{
    private readonly AppDbContext _db;

    public PostContentRepository(AppDbContext db) : base(db)
	{
        _db = db;
    }
	public async Task<PostContent> GetByPostIDAsync(int postId)
	{
		return await _db.PostContents.Where(x=>x.PostId == postId).FirstAsync();
	}
	public async Task<List<PostContent>> ListByCategoryIdAsync(int categoryId)
	{
		Category cat = await _db.Categories.FindAsync(categoryId);

		List<PostCategory> postCategory = await _db.PostCategories.Where(x => x.CategoryId == cat.Id).ToListAsync();

		List<PostContent> postContent = new List<PostContent>();

		foreach (var pc in postCategory)
		{
			PostContent onePC = await _db.PostContents.Where(x => x.PostId == pc.PostId).FirstAsync();
			postContent.Add(onePC);
		}

		return postContent;
	}
}
