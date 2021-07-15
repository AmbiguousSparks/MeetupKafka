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
        public byte[] Photo { get; set; }
        public Categories Category { get; set; }
        public Dictionary<string, string> Features { get; set; }
        public DateTime SolicitationTime { get; set; }
        public DateTime UpdateTime { get; set; }
        public InvoiceStatus Status { get; set; }
    }
}
