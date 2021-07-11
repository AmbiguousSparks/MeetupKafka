using MongoDB.Driver;
using Order.Domain.Models;
using Order.Infra.Mongo;
using Order.Infra.Repositories.Interfaces;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Order.Infra.Repositories
{
    public class UserRepository : BaseRepository<User>, IUserRepository
    {
        public UserRepository(IMongoContext context) : base(context)
        {
        }

        public async Task<User> GetByUsernameAndPassword(string username, string password, CancellationToken cancellationToken = default)
        {
            var users = await _dbSet.FindAsync<User>(Filter.Eq(u => u.Username, username), cancellationToken: cancellationToken);
            if (!await users.AnyAsync(cancellationToken))
                throw new ArgumentException("Username or password is invalid!");
            var user = await users.FirstOrDefaultAsync(cancellationToken);
            if (user.Password != password)
                throw new ArgumentException("Username or password is invalid!");
            return user;
        }

        public async Task<bool> UserExists(string username, CancellationToken cancellationToken = default)
        {
            var users = await _dbSet.FindAsync<User>(Filter.Eq(u => u.Username, username), cancellationToken: cancellationToken);
            return await users.AnyAsync(cancellationToken);
        }
    }
}
