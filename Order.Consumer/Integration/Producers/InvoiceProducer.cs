using Confluent.Kafka;
using Order.Domain.Models;
using Order.Infra.Messaging.Producers;

namespace Order.Consumer.Integration.Producers
{
    public class InvoiceProducer : ConfluentProducerBase<Invoice>
    {
        public InvoiceProducer(ProducerConfig config) : base(config)
        {
        }

        public override string Topics => "product.new.product";
    }
}
