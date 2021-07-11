using Order.Domain.Data;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Order.Infra.Repositories.Interfaces
{
    public interface IRepository<TEntity> where TEntity : Entity
    {
        Task Add(TEntity entity, CancellationToken cancellationToken = default);
        Task<TEntity> GetById(Guid id, CancellationToken cancellationToken = default);
        Task<IList<TEntity>> GetAll(CancellationToken cancellationToken = default);
        Task Update(TEntity entity, CancellationToken cancellationToken = default);
        Task Remove(Guid id, CancellationToken cancellationToken = default);
    }
}
