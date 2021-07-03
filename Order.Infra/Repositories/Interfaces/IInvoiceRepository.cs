using Order.Domain.Models;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Order.Infra.Repositories.Interfaces
{
    public interface IInvoiceRepository
    {
        Task<IList<Invoice>> GetAllPending(CancellationToken cancellationToken = default);
        Task Add(Invoice invoice, CancellationToken cancellationToken = default);
        Task<IList<Invoice>> GetAll(CancellationToken cancellationToken = default);
        Task<Invoice> GetById(Guid id, CancellationToken cancellationToken = default);
        Task Update(Invoice invoice, CancellationToken cancellationToken = default);
    }
}
