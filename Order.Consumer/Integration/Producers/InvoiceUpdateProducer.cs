using Confluent.Kafka;
using Order.Application.Requests;
using Order.Infra.Messaging.Producers;

namespace Order.Consumer.Integration.Producers
{
    public class InvoiceUpdateProducer : ConfluentProducerBase<UpdateInvoiceStatusRequest>
    {
        public InvoiceUpdateProducer(ProducerConfig config) : base(config)
        {
        }

        public override string Topics => "invoice.status.update";
    }
}
