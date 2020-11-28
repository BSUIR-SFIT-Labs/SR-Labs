using ProjectForTesting.DataAccess.Repositories;
using ProjectForTesting.Domain.Entities;
using ProjectForTesting.Persistence;
using System.Threading.Tasks;

namespace ProjectForTesting.DataAccess
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _context;

        private IRepository<User> _users;

        public UnitOfWork(AppDbContext context)
        {
            _context = context;
        }

        public IRepository<User> Users
        {
            get
            {
                return _users ??= new Repository<User>(_context);
            }
        }

        public async Task CommitAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}