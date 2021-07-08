using Confluent.Kafka;
using Order.Domain.Models;
using Order.Infra.Producers.Messaging;

namespace Order.Consumer.Producers
{
    public class InvoiceProducer : ConfluentProducerBase<Invoice>
    {
        public InvoiceProducer(ProducerConfig config) : base(config)
        {
        }

        public override string Topics => "product.new.product";
    }
}
