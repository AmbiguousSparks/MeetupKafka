using MongoDB.Driver;
using Order.Domain.Models;
using Order.Domain.Models.Enums;
using Order.Infra.Mongo;
using Order.Infra.Repositories.Interfaces;
using System;
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

        public async Task<IList<Invoice>> GetAllDecided(CancellationToken cancellationToken = default)
        {
            var data = await _dbSet.FindAsync<Invoice>(Filter.Not(Filter.Eq("Status", InvoiceStatus.Pending)), cancellationToken: cancellationToken);
            return data.ToList(cancellationToken: cancellationToken);
        }

        public async Task<IList<Invoice>> GetAllPending(CancellationToken cancellationToken = default)
        {
            var data = await _dbSet.FindAsync<Invoice>(Filter.Eq("Status", InvoiceStatus.Pending), cancellationToken: cancellationToken);
            return data.ToList(cancellationToken: cancellationToken);
        }
    }
}
