using Order.Domain.Models;
using System.Threading;
using System.Threading.Tasks;

namespace Order.Consumer.Hubs.Interfaces
{
    public interface IInvoiceHub
    {
        Task NewInvoice(Invoice invoice, CancellationToken cancellationToken = default);
    }
}
