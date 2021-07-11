using MediatR;
using Order.Domain.Models;
using Order.Domain.Models.Enums;
using System;

namespace Order.Application.Requests
{
    public class UpdateInvoiceStatusRequest : IRequest<Invoice>
    {
        public Guid Id { get; set; }
        public InvoiceStatus Status { get; set; }
        public string Name { get; set; }
    }
}
