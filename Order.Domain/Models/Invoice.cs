using Order.Domain.Data;
using Order.Domain.Models.Enums;
using System;
using System.Collections.Generic;

namespace Order.Domain.Models
{
    public class Invoice : Entity
    {
        public string Name { get; set; }
        public decimal Value { get; set; }
        public string Description { get; set; }
        public string Photo { get; set; }
        public byte[] PhotoByte => string.IsNullOrEmpty(Photo) ? null : Convert.FromBase64String(Photo);
        public Categories Category { get; set; }
        public List<string> Features { get; set; }
        public DateTime SolicitationTime { get; set; }
        public DateTime UpdateTime { get; set; }
        public InvoiceStatus Status { get; set; }
    }
}
