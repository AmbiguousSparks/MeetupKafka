using Microsoft.AspNetCore.SignalR;
using Order.Consumer.Hubs.Interfaces;
using System.Collections;
using System.Threading.Tasks;

namespace Order.Consumer.Hubs
{
    public class InvoiceHub : Hub<IInvoiceHub>
    {
        private readonly Hashtable connections = new();
        public void Connect(string name)
        {
            connections.Add(name, Context.ConnectionId);            
        }
    }
}
