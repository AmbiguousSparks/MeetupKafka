using System;

namespace Order.Application
{
    public class OrderException : Exception
    {
        public OrderException(string message) : base(message)
        {
        }
    }
}
