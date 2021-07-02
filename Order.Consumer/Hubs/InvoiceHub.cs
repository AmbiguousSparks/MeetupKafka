using Microsoft.AspNetCore.SignalR;
using Order.Consumer.Hubs.Interfaces;

namespace Order.Consumer.Hubs
{
    public class InvoiceHub : Hub<IInvoiceHub>
    {
    }
}
