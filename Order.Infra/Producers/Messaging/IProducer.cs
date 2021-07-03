using Confluent.Kafka;
using System.Threading;
using System.Threading.Tasks;

namespace Order.Infra.Producers.Messaging
{
    public interface IProducer<T>
    {
        Task<DeliveryResult<string, string>> ProduceAsync(T message, CancellationToken cancellationToken = default);
    }
}
