using Confluent.Kafka;
using MediatR;
using Order.Domain.Models.Enums;
using System;
using System.Collections.Generic;

namespace Order.Application.Requests
{
    public class InvoiceProducerRequest : IRequest<DeliveryResult<string, string>>
    {
        public string Name { get; set; }
        public decimal Value { get; set; }
        public string Description { get; set; }
        public byte[] Photo { get; set; }        
        public Categories Category { get; set; }
        public Dictionary<string, string> Features { get; set; }
        public DateTime SolicitationTime { get; set; }
    }
}
