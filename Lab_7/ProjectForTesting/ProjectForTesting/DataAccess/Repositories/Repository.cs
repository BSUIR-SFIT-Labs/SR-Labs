using Microsoft.EntityFrameworkCore;
using ProjectForTesting.Domain.Entities;
using ProjectForTesting.Persistence;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProjectForTesting.DataAccess.Repositories
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity: BaseEntity
    {
        private readonly AppDbContext _context;
        private readonly DbSet<TEntity> _entity;

        public Repository(AppDbContext context)
        {
            _context = context;
            _entity = context.Set<TEntity>();
        }

        public async Task<TEntity> GetByIdAsync(int id)
        {
            return await _entity.FirstOrDefaultAsync(e => e.Id == id);
        }

        public async Task<IReadOnlyList<TEntity>> GetAllAsync()
        {
            return await _entity.ToListAsync();
        }

        public async Task AddAsync(TEntity entity)
        {
            await _entity.AddAsync(entity);
        }

        public void Update(TEntity entity)
        {
            _entity.Attach(entity);
            _context.Entry(entity).State = EntityState.Modified;
        }

        public async Task DeleteByIdAsync(int id)
        {
            var entityToDelete = await _entity.FirstOrDefaultAsync(e => e.Id == id);
            Delete(entityToDelete);
        }

        public void Delete(TEntity entity)
        {
            if (_context.Entry(entity).State == EntityState.Detached)
            {
                _entity.Attach(entity);
            }

            _entity.Remove(entity);
        }
    }
}