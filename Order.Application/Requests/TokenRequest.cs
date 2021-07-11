using MediatR;
using Order.Application.Responses;
using Order.Application.Settings;
using Order.Domain.Models;

namespace Order.Application.Requests
{
    public class TokenRequest : IRequest<TokenResponse>
    {
        public User User { get; set; }
        public TokenSettings TokenSettings { get; set; }
    }
}
