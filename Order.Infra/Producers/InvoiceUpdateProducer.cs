using Confluent.Kafka;
using Order.Infra.Producers.Messaging;
using Order.Infra.Requests;

namespace Order.Infra.Producers
{
    public class InvoiceUpdateProducer : ConfluentProducerBase<UpdateInvoiceStatusRequest>
    {
        public InvoiceUpdateProducer(ProducerConfig config) : base(config)
        {
        }

        public override string Topics => "invoice.status.update";
    }
}
