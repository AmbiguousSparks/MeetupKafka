using MongoDB.Driver;
using Order.Domain.Models;
using Order.Infra.Mongo;
using Order.Infra.Repositories.Interfaces;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Order.Infra.Repositories
{
    public class InvoiceRepository : BaseRepository<Invoice>, IInvoiceRepository
    {
        public InvoiceRepository(IMongoContext context) : base(context)
        {
        }

        public async Task Add(Invoice invoice, CancellationToken cancellationToken = default)
        {
            await base.Add(invoice);
        }

        public override async Task<IList<Invoice>> GetAll(CancellationToken cancellationToken = default)
        {
            return await base.GetAll(cancellationToken);
        }

        public async Task<IList<Invoice>> GetAllPending(CancellationToken cancellationToken = default)
        {
            var data = await _dbSet.FindAsync<Invoice>(Filter.Eq("IsApproved", true), cancellationToken: cancellationToken);
            return data.ToList(cancellationToken: cancellationToken);
        }
    }
}
