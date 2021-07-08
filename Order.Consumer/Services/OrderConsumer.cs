using Confluent.Kafka;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Order.Application.Requests;
using Order.Consumer.Hubs;
using Order.Consumer.Hubs.Interfaces;
using Order.Domain.Configurations;
using Serilog;
using System;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;

namespace Order.Consumer.Services
{
    public class OrderConsumer : BackgroundService
    {
        //TODO: refatorar codigo do consumer, adicionar interface generica pra consumers
        private readonly IHubContext<InvoiceHub, IInvoiceHub> _hubContext;

        private readonly IMediator _mediator;
        private readonly KafkaConfig _config;
        public OrderConsumer(KafkaConfig config, IHubContext<InvoiceHub, IInvoiceHub> hubContext, IMediator mediatr)
        {
            _config = config;
            _hubContext = hubContext;
            _mediator = mediatr;
        }
        protected async override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            if (_config.Topics.Count > 0)
            {
                await Task.Run(async () =>
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
                            InvoiceRequest order = JsonConvert.DeserializeObject<InvoiceRequest>(result.Message.Value);
                            if (order is not null)
                            {
                                var invoice = await _mediator.Send(order, stoppingToken);
                                await _hubContext.Clients.All.NewInvoice(invoice, stoppingToken);
                            }
                        }
                        catch (ConsumeException e)
                        {
                            Log.Error($"Erro while consuming: {e.Error.Reason}");
                        }
                        catch (OperationCanceledException)
                        {
                            consumer.Close();
                        }
                        catch (Exception e)
                        {
                            Log.Error($"Erro while consuming: {e.Message}");
                        }
                    }
                }, stoppingToken);
            }
        }
    }
}
