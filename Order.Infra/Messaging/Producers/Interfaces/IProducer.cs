using Confluent.Kafka;
using System.Threading;
using System.Threading.Tasks;

namespace Order.Infra.Messaging.Producers.Interface
{
    public interface IProducer<T>
    {
        Task<DeliveryResult<string, string>> ProduceAsync(T message, CancellationToken cancellationToken = default);
    }
}
