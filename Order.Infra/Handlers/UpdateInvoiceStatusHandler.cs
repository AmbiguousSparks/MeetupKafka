using Confluent.Kafka;
using MediatR;
using Order.Domain.Models;
using Order.Infra.Producers.Messaging;
using Order.Infra.Repositories.Interfaces;
using Order.Infra.Requests;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Order.Infra.Handlers
{
    public class UpdateInvoiceStatusHandler : IRequestHandler<UpdateInvoiceStatusRequest, UpdateInvoiceStatusRequest>
    {
        private readonly IProducer<UpdateInvoiceStatusRequest> _invoiceUpdateProducer;
        private readonly IInvoiceRepository _invoiceRepository;

        public UpdateInvoiceStatusHandler(
            IProducer<UpdateInvoiceStatusRequest> invoiceUpdateProducer,
            IInvoiceRepository invoiceRepository)
        {
            _invoiceUpdateProducer = invoiceUpdateProducer;
            _invoiceRepository = invoiceRepository;
        }

        public async Task<UpdateInvoiceStatusRequest> Handle(UpdateInvoiceStatusRequest request, CancellationToken cancellationToken)
        {
            var invoice = await _invoiceRepository.GetById(request.Id, cancellationToken);
            invoice.Status = request.Status;
            invoice.UpdateTime = DateTime.Now;
            await _invoiceRepository.Update(invoice, cancellationToken);
            await _invoiceUpdateProducer.ProduceAsync(request, cancellationToken);
            return request;
        }
    }
}
