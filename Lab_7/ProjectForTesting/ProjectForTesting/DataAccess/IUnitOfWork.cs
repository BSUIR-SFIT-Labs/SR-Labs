using ProjectForTesting.DataAccess.Repositories;
using ProjectForTesting.Domain.Entities;
using System.Threading.Tasks;

namespace ProjectForTesting.DataAccess
{
    public interface IUnitOfWork
    {
        public IRepository<User> Users { get; }

        Task CommitAsync();
    }
}