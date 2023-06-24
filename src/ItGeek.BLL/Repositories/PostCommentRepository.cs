using ItGeek.DAL.Data;
using ItGeek.DAL.Entities;
using ItGeek.DAL.Interfaces;

namespace ItGeek.BLL.Repositories;

public class PostCommentRepository : GenericRepositoryAsync<PostComment>, IPostCommentRepository
{
    private readonly AppDbContext _db;

    public PostCommentRepository(AppDbContext db) : base(db)
    {
        _db = db;
    }

}