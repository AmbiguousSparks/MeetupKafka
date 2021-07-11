using Order.Domain.Models;
using System.Threading;
using System.Threading.Tasks;

namespace Order.Infra.Repositories.Interfaces
{
    public interface IUserRepository : IRepository<User>
    {
        Task<User> GetByUsernameAndPassword(string username, string password, CancellationToken cancellationToken = default);
        Task<bool> UserExists(string username, CancellationToken cancellationToken = default);
    }
}
