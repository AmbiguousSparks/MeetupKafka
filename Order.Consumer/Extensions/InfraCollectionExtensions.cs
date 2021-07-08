using Confluent.Kafka;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Order.Application.Requests;
using Order.Consumer.Producers;
using Order.Domain.Models;
using Order.Infra.Mongo;
using Order.Infra.Producers.Messaging;
using Order.Infra.Repositories;
using Order.Infra.Repositories.Interfaces;

namespace Order.Consumer.Extensions
{
    public static class InfraCollectionExtensions
    {
        public static IServiceCollection RegistryMongoService(this IServiceCollection services, string connectionString)
        {
            services
                .AddSingleton<IMongoContext>(s => new MongoContext(connectionString))
                .AddTransient<IInvoiceRepository, InvoiceRepository>();
            return services;
        }
        public static IServiceCollection AddProducers(this IServiceCollection services, IConfiguration configuration)
        {
            ProducerConfig config = new()
            {
                BootstrapServers = configuration.GetSection("KafkaHost").Value,
                MessageSendMaxRetries = 3,
                MessageTimeoutMs = 15000
            };
            services.AddSingleton(config);
            services.AddSingleton<IProducer<UpdateInvoiceStatusRequest>, InvoiceUpdateProducer>();
            services.AddSingleton<IProducer<Invoice>, InvoiceProducer>();
            return services;
        }
    }
}
