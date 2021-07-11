using Confluent.Kafka;
using MediatR;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Order.Application.Requests;
using Order.Consumer.Hubs;
using Order.Consumer.Hubs.Interfaces;
using Order.Infra.Messaging.Consumers.Intefaces;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Order.Consumer.Services
{
    public class OrderListenerService : BackgroundService
    {
        private readonly IHubContext<InvoiceHub, IInvoiceHub> _hubContext;

        private readonly IMediator _mediator;
        private readonly ILogger<OrderListenerService> _logger;
        private readonly IConsumer<InvoiceRequest> _invoiceConsumer;
        public OrderListenerService(IHubContext<InvoiceHub, IInvoiceHub> hubContext, IMediator mediatr, ILogger<OrderListenerService> logger, IConsumer<InvoiceRequest> invoiceConsumer)
        {
            _hubContext = hubContext;
            _mediator = mediatr;
            _logger = logger;
            _invoiceConsumer = invoiceConsumer;
        }
        protected async override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            await Task.Run(async () =>
            {
                while (!stoppingToken.IsCancellationRequested)
                {
                    try
                    {
                        var result = await _invoiceConsumer.ConsumeAsync(stoppingToken);
                        _logger.LogInformation($"Message received: {result.Message.Value}.");
                        InvoiceRequest order = JsonConvert.DeserializeObject<InvoiceRequest>(result.Message.Value);
                        if (order is not null)
                        {
                            var invoice = await _mediator.Send(order, stoppingToken);
                            await _hubContext.Clients.All.NewInvoice(invoice, stoppingToken);
                        }
                    }
                    catch (ConsumeException e)
                    {
                        _logger.LogError($"Erro while consuming: {e.Error.Reason}");
                    }
                    catch (OperationCanceledException)
                    {
                        _invoiceConsumer.Close();
                    }
                    catch (Exception e)
                    {
                        _logger.LogError($"Erro while consuming: {e.Message}");
                    }
                }
            }, stoppingToken);

        }
    }
}
