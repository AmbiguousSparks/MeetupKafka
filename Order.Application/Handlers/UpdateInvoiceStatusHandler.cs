using MediatR;
using Order.Application.Requests;
using Order.Domain.Models;
using Order.Infra.Messaging.Producers.Interface;
using Order.Infra.Repositories.Interfaces;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Order.Application.Handlers
{
    public class UpdateInvoiceStatusHandler : IRequestHandler<UpdateInvoiceStatusRequest, Invoice>
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

        public async Task<Invoice> Handle(UpdateInvoiceStatusRequest request, CancellationToken cancellationToken)
        {
            var invoice = await _invoiceRepository.GetById(request.Id, cancellationToken);
            invoice.Status = request.Status;
            invoice.UpdateTime = DateTime.Now;
            request.Name = invoice.Name;
            await _invoiceRepository.Update(invoice, cancellationToken);
            await _invoiceUpdateProducer.ProduceAsync(request, cancellationToken);
            return invoice;
        }
    }
}
