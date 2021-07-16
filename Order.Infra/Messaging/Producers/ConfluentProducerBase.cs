using Confluent.Kafka;
using Newtonsoft.Json;
using Order.Infra.Messaging.Producers.Builders;
using Order.Infra.Messaging.Producers.Interface;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Order.Infra.Messaging.Producers
{
    public abstract class ConfluentProducerBase<T> : IProducer<T>, IDisposable
    {
        private readonly IProducer<string, string> _producer;
        public abstract string Topics { get; }
        public ConfluentProducerBase(ProducerConfig config)
        {
            _producer = new ConfluentProducerBuilder()
                .AddConfig(config)
                .Build();
        }
        public void Dispose()
        {
            _producer.Dispose();
            GC.SuppressFinalize(this);
        }

        public async Task<DeliveryResult<string, string>> ProduceAsync(T message, CancellationToken cancellationToken = default)
        {
            var kafkaMessage = new Message<string, string>()
            {
                Key = Guid.NewGuid().ToString(),
                Value = JsonConvert.SerializeObject(message)
            };

            return await _producer.ProduceAsync(Topics, kafkaMessage, cancellationToken);
        }
    }
}
