
using eTicket.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.Linq.Expressions;

namespace eTicket.Data.Base
{
	public class EntityBaseRepository<T> : IEntityBaseRepository<T> where T : class, IEntityBase, new()
	{
		private readonly AppDbContext _context;
		public EntityBaseRepository(AppDbContext context)
		{
			_context = context;
		}
		public async Task AddAsync(T entity)
		{
			await _context.Set<T>().AddAsync(entity);
			await _context.SaveChangesAsync();
		}
		public async Task DeleteAsync(int id)
		{
			var entity = await _context.Set<T>().FirstOrDefaultAsync(n => n.Id == id);
			EntityEntry entityEntry = _context.Entry<T>(entity);
			entityEntry.State = EntityState.Deleted;
			await _context.SaveChangesAsync();
		}
		public async Task<IEnumerable<T>> GetAllAsync() => await _context.Set<T>().ToListAsync();

		public async Task<IEnumerable<T>> GetAllAsync(params Expression<Func<T, object>>[] inculdeProperties)
		{
			IQueryable<T> query = _context.Set<T>();
			query = inculdeProperties.Aggregate(query, (current, inculdProperty) => current.Include(inculdProperty));
			return await query.ToListAsync();
		}

		public async Task<T> GetByIdAsync(int id) => await _context.Set<T>().FirstOrDefaultAsync(n => n.Id == id);

		public async Task UpdateAsync(int id, T entity)
		{
			entity.Id = id;
			EntityEntry entityEntry = _context.Entry<T>(entity);
			entityEntry.State = EntityState.Modified;
			await _context.SaveChangesAsync();
		}
	}
}
