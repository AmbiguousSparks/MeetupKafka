using Confluent.Kafka;
using System.Threading;
using System.Threading.Tasks;

namespace Order.Infra.Messaging.Consumers.Intefaces
{
    public interface IConsumer<T>
    {
        Task<ConsumeResult<Ignore, string>> ConsumeAsync(CancellationToken cancellationToken = default);
        void Close();
    }
}
