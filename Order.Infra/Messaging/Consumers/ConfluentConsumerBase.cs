using Confluent.Kafka;
using Microsoft.Extensions.Logging;
using Order.Infra.Messaging.Consumers.Builders;
using Order.Infra.Messaging.Consumers.Intefaces;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Order.Infra.Messaging.Consumers
{
    public abstract class ConfluentConsumerBase<T> : IConsumer<T>, IDisposable
    {
        private readonly IConsumer<Ignore, string> _consumer;
        public abstract string Topics { get; }
        protected ConfluentConsumerBase(ConsumerConfig consumerConfig)
        {
            _consumer = new ConfluentConsumerBuilder()
                            .AddConfig(consumerConfig)
                            .Build(Topics);
        }
        public async Task<ConsumeResult<Ignore, string>> ConsumeAsync(CancellationToken cancellationToken = default)
        {
            return await Task.Run(() =>
            {
                return _consumer.Consume(cancellationToken);
            }, cancellationToken);
        }

        public void Dispose()
        {
            _consumer.Dispose();
            GC.SuppressFinalize(this);
        }

        public void Close()
        {
            _consumer.Close();
        }
    }
}
