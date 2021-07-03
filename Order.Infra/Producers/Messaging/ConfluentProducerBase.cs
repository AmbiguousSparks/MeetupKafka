using Confluent.Kafka;
using Newtonsoft.Json;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Order.Infra.Producers.Messaging
{
    public abstract class ConfluentProducerBase<T> : IProducer<T>, IDisposable
    {
        private readonly ProducerConfig _producerConfig;
        private IProducer<string, string> _producer;
        public abstract string Topics { get; }
        public ConfluentProducerBase(ProducerConfig config)
        {
            _producerConfig = config;
            _producer = new ConfluentProducerBuilder()
                .AddConfig(config)
                .Build();
        }
        public void Dispose()
        {
            _producer.Dispose();
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
