using Confluent.Kafka;
using Order.Application.Requests;
using Order.Infra.Messaging.Consumers;

namespace Order.Consumer.Integration.Consumers
{
    public class OrderConsumer : ConfluentConsumerBase<InvoiceRequest>
    {
        public OrderConsumer(ConsumerConfig consumerConfig) : base(consumerConfig)
        {
        }

        public override string Topics => "product.new.product";
    }
}
