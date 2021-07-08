using MediatR;
using Order.Application.Requests;
using Order.Domain.Models;
using Order.Infra.Repositories.Interfaces;
using System.Threading;
using System.Threading.Tasks;

namespace Order.Application.Handlers
{
    public class InvoiceHandler : IRequestHandler<InvoiceRequest, Invoice>
    {
        private readonly IInvoiceRepository _invoiceRepository;
        public InvoiceHandler(IInvoiceRepository invoiceRepository)
        {
            _invoiceRepository = invoiceRepository;
        }
        public async Task<Invoice> Handle(InvoiceRequest request, CancellationToken cancellationToken)
        {
            Invoice invoice = new()
            {
                Category = request.Category,
                Description = request.Description,
                Features = request.Features,
                Status = Domain.Models.Enums.InvoiceStatus.Pending,
                Name = request.Name,
                Photo = request.Photo,
                SolicitationTime = request.SolicitationTime,
                Value = request.Value
            };
            await _invoiceRepository.Add(invoice, cancellationToken);
            return invoice;            
        }
    }
}
