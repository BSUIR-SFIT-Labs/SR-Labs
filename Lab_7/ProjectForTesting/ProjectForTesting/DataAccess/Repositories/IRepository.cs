using ProjectForTesting.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProjectForTesting.DataAccess.Repositories
{
    public interface IRepository<TEntity> where TEntity : BaseEntity
    {
        Task<TEntity> GetByIdAsync(int id);

        Task<IReadOnlyList<TEntity>> GetAllAsync();

        Task AddAsync(TEntity entity);

        void Update(TEntity entity);

        Task DeleteByIdAsync(int id);

        void Delete(TEntity entity);
    }
}