using ItGeek.DAL.Data;
using ItGeek.DAL.Entities;
using ItGeek.DAL.Interfaces;

namespace ItGeek.BLL.Repositories;

public class AuthorsSocialRepository : GenericRepositoryAsync<AuthorsSocial>, IAuthorsSocialRepository
{
	public AuthorsSocialRepository(AppDbContext db) : base(db)
	{
	}
}
