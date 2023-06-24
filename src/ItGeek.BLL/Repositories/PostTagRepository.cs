using ItGeek.DAL.Data;
using ItGeek.DAL.Entities;
using ItGeek.DAL.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ItGeek.BLL.Repositories;

public class PostTagRepository : GenericRepositoryAsync<PostTag>, IPostTagRepository
{
	private readonly AppDbContext _db;

	public PostTagRepository(AppDbContext db) : base(db)
	{
		_db = db;
	}
	public async Task<int> GetTagIdByName(string tagName)
	{
		Tag? tag = await _db.Tags.Where(x => x.Name == tagName).FirstOrDefaultAsync();
		if(tag != null)
		{
            return tag.Id;
        }
        return 0;
    }
	public async Task<string> GetByPostIDAsync(int postId)
	{
        string tags = "";
        List<PostTag> postTags = await _db.PostTags.Include(x=>x.Tag).Where(x => x.PostId == postId).ToListAsync();
		if(postTags != null)
		{
			foreach(PostTag postTag in postTags) 
			{
				tags += postTag.Tag.Name + ", ";
            }
        }
		return tags;
    }

    public async Task<bool> GetByTagIdAsync(int postId, int tagId)
    {
        PostTag? postTag = await _db.PostTags.Where(x=>x.PostId == postId && x.TagId == tagId).FirstOrDefaultAsync();
		if (postTag != null)
		{
			return true;
		}
		return false;
    }
    public async Task DeleteByPostIdAsync(int postId)
    {
		List<PostTag> postTags = await _db.PostTags.Where(x => x.PostId == postId).ToListAsync();

		foreach (PostTag item in postTags)
		{
            _db.PostTags.Remove(item);
            await _db.SaveChangesAsync();
		}
    }
}
