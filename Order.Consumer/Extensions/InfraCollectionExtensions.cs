using Microsoft.Extensions.DependencyInjection;
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
                .AddTransient<IInvoiceRepository, InvoiceRepository>();
            return services;
        }
    }
}
