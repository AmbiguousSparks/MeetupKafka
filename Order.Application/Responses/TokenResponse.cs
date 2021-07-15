using Order.Domain.Models;
using System;

namespace Order.Application.Responses
{
    public class TokenResponse
    {
        public string Token { get; set; }
        public DateTime ValidFrom { get; set; }
        public DateTime ValidTo { get; set; }
        public User User { get; set; }
    }
}
