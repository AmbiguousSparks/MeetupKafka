using Confluent.Kafka;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Order.Infra.Messaging.Consumers.Builders
{
    public class ConfluentConsumerBuilder
    {
        private readonly Dictionary<string, string> _configs = new();

        public ConfluentConsumerBuilder AddConfig(IEnumerable<KeyValuePair<string, string>> consumerConfig)
        {
            if (consumerConfig == null)
                return this;

            foreach (var config in consumerConfig)
                AddConfig(config);

            return this;
        }

        private ConfluentConsumerBuilder AddConfig(KeyValuePair<string, string> config)
        {
            _configs[config.Key] = config.Value;

            return this;
        }

        public IConsumer<Ignore, string> Build(string topics)
        {
            if (_configs is null || !_configs.Any())
                throw new ArgumentException("Configuration can not be empty!");

            var consumer = new ConsumerBuilder<Ignore, string>(_configs)
                .Build();

            consumer.Subscribe(topics);

            return consumer;
        }
    }
}
