using ItGeek.DAL.Data;
using ItGeek.DAL.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ItGeek.BLL.Repositories;

public class GenericRepositoryAsync<T> : IGenericRepositoryAsync<T> where T : class
{
	private readonly AppDbContext _db;

	public GenericRepositoryAsync(AppDbContext db)
    {
		_db = db;
	}
    public async Task<IReadOnlyList<T>> ListAllAsync()
	{
		return await _db.Set<T>().ToListAsync();
	}

	public async Task<T> GetByIDAsync(int id)
	{
		return await _db.Set<T>().FindAsync(id);
	}
	public async Task<T> InsertAsync(T entity)
	{
		await _db.Set<T>().AddAsync(entity);
		await _db.SaveChangesAsync();

		return entity;
	}
	public async Task UpdateAsync(T entity)
	{
		_db.Entry(entity).State = EntityState.Modified;
		await _db.SaveChangesAsync();
	}
	public async Task DeleteAsync(T entity)
	{
		_db.Set<T>().Remove(entity);
		await _db.SaveChangesAsync();
	}
	public async Task<IReadOnlyList<T>> GetPagedAsync(int page, int size)
	{
		return await _db.Set<T>().Skip((page - 1) * size).Take(size).ToListAsync();
	}

}
