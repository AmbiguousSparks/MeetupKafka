using MediatR;
using Order.Domain.Models;
using Order.Domain.Models.Enums;
using System;
using System.Collections.Generic;

namespace Order.Infra.Requests
{
    public class InvoiceRequest : IRequest<Invoice>
    {
        public string Name { get; set; }
        public decimal Value { get; set; }
        public string Description { get; set; }
        public string Photo { get; set; }
        public byte[] PhotoByte => string.IsNullOrEmpty(Photo) ? null : Convert.FromBase64String(Photo);
        public Categories Category { get; set; }
        public List<string> Features { get; set; }
        public DateTime SolicitationTime { get; set; }
    }
}
