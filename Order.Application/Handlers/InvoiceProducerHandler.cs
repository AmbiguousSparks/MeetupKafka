using Confluent.Kafka;
using MediatR;
using Order.Application.Requests;
using Order.Domain.Models;
using Order.Infra.Messaging.Producers.Interface;
using System.Threading;
using System.Threading.Tasks;

namespace Order.Application.Handlers
{
    public class InvoiceProducerHandler : IRequestHandler<InvoiceProducerRequest, DeliveryResult<string, string>>
    {
        private readonly IProducer<Invoice> _invoiceProducer;

        public InvoiceProducerHandler(IProducer<Invoice> invoiceProducer)
        {
            _invoiceProducer = invoiceProducer;
        }

        public async Task<DeliveryResult<string, string>> Handle(InvoiceProducerRequest request, CancellationToken cancellationToken)
        {
            Invoice invoice = new()
            {
                Category = request.Category,
                Description = request.Description,
                Features = request.Features,
                Name = request.Name,
                Photo = request.Photo,
                SolicitationTime = request.SolicitationTime,
                Status = Domain.Models.Enums.InvoiceStatus.Pending,
                Value = request.Value
            };
            return await _invoiceProducer.ProduceAsync(invoice, cancellationToken);
        }
    }
}
