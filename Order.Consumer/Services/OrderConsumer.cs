using Confluent.Kafka;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;
using Order.Consumer.Hubs;
using Order.Consumer.Hubs.Interfaces;
using Order.Domain.Configurations;
using Order.Domain.Models;
using System;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;

namespace Order.Consumer.Services
{
    public class OrderConsumer : BackgroundService
    {
        private readonly IHubContext<InvoiceHub, IInvoiceHub> _hubContext;
        private readonly KafkaConfig _config;
        public OrderConsumer(KafkaConfig config, IHubContext<InvoiceHub, IInvoiceHub> hubContext)
        {
            _config = config;
            _hubContext = hubContext;
        }
        protected async override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            await Task.Run(() =>
            {
                ConsumerConfig config = new()
                {
                    BootstrapServers = _config.BootstrapServer,
                    GroupId = "1",
                    ClientId = "1"
                };
                using IConsumer<Ignore, string> consumer = new ConsumerBuilder<Ignore, string>(config).Build();
                consumer.Subscribe(_config.Topics);
                while (!stoppingToken.IsCancellationRequested)
                {
                    try
                    {
                        var result = consumer.Consume(stoppingToken);
                        Debug.WriteLine(result.Message.Value);
                        Invoice order = JsonConvert.DeserializeObject<Invoice>(result.Message.Value);
                        _hubContext.Clients.All.NewInvoice(order, stoppingToken);
                    }
                    catch(ConsumeException e)
                    {
                        Debug.WriteLine($"Erro while consuming: {e.Error.Reason}");
                    }
                    catch (OperationCanceledException)
                    {
                        consumer.Close();
                    }
                    catch (Exception e)
                    {
                        Debug.WriteLine($"Erro while consuming: {e.Message}");
                    }
                }
            }, stoppingToken);
        }

        private void StartConsuming(CancellationToken cancellationToken = default)
        {
            ConsumerConfig config = new()
            {
                BootstrapServers = _config.BootstrapServer,
                GroupId = "1",
                ClientId = "1"
            };
            using IConsumer<Ignore, string> consumer = new ConsumerBuilder<Ignore, string>(config).Build();
            consumer.Subscribe(_config.Topics);
            while (!cancellationToken.IsCancellationRequested)
            {
                var result = consumer.Consume(cancellationToken);
                Debug.WriteLine(result.Message.Value);
            }
        }
    }
}
