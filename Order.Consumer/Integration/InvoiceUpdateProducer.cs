using Confluent.Kafka;
using Order.Application.Requests;
using Order.Infra.Producers.Messaging;

namespace Order.Consumer.Producers
{
    public class InvoiceUpdateProducer : ConfluentProducerBase<UpdateInvoiceStatusRequest>
    {
        public InvoiceUpdateProducer(ProducerConfig config) : base(config)
        {
        }

        public override string Topics => "invoice.status.update";
    }
}
