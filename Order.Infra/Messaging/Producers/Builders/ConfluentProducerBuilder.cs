using Confluent.Kafka;
using System.Collections.Generic;
using System.Linq;

namespace Order.Infra.Messaging.Producers.Builders
{
    internal class ConfluentProducerBuilder
    {
        private readonly Dictionary<string, string> _configs = new();
        public ConfluentProducerBuilder AddConfig(IEnumerable<KeyValuePair<string, string>> configs)
        {
            if (configs is null)
                return this;
            foreach (var config in configs)
                AddConfig(config);

            return this;
        }

        private void AddConfig(KeyValuePair<string, string> config)
        {
            _configs[config.Key] = config.Value;
        }

        public IProducer<string, string> Build()
        {
            if (_configs is null || !_configs.Any())
                throw new System.Exception("Configuration is not valid!");
            return new ProducerBuilder<string, string>(_configs)
                .Build();
        }
    }
}
