using Confluent.Kafka;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Order.Application.Requests;
using Order.Consumer.Integration.Consumers;
using Order.Consumer.Integration.Producers;
using Order.Domain.Models;
using Order.Infra.Messaging.Consumers.Intefaces;
using Order.Infra.Messaging.Producers.Interface;
using Order.Infra.Mongo;
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
                .AddTransient<IInvoiceRepository, InvoiceRepository>()
                .AddTransient<IUserRepository, UserRepository>();
            return services;
        }
        public static IServiceCollection AddProducers(this IServiceCollection services, IConfiguration configuration)
        {
            ProducerConfig config = configuration.GetSection("ProducerConfig").Get<ProducerConfig>();
            services.AddSingleton(config);
            services.AddSingleton<IProducer<UpdateInvoiceStatusRequest>, InvoiceUpdateProducer>();
            services.AddSingleton<IProducer<Invoice>, InvoiceProducer>();
            return services;
        }
        public static IServiceCollection AddConsumers(this IServiceCollection services, IConfiguration configuration)
        {
            ConsumerConfig config = configuration.GetSection("ConsumerConfig").Get<ConsumerConfig>();
            services.AddSingleton(config);
            services.AddSingleton<IConsumer<InvoiceRequest>, OrderConsumer>();
            return services;
        }
    }
}
