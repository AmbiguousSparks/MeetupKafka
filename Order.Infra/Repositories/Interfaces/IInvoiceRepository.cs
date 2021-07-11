using Order.Domain.Models;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Order.Infra.Repositories.Interfaces
{
    public interface IInvoiceRepository : IRepository<Invoice>
    {
        Task<IList<Invoice>> GetAllPending(CancellationToken cancellationToken = default);
        Task<IList<Invoice>> GetAllDecided(CancellationToken cancellationToken = default);
    }
}
