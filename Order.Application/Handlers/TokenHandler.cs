using MediatR;
using Order.Application.Requests;
using Order.Application.Responses;
using Order.Application.Services;
using Order.Domain.Models;
using Order.Infra.Repositories.Interfaces;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Order.Application.Handlers
{
    public class TokenHandler : IRequestHandler<TokenRequest, TokenResponse>
    {
        private readonly IUserRepository _userRepository;

        public TokenHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<TokenResponse> Handle(TokenRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var user = await _userRepository.GetByUsernameAndPassword(request.User.Username, request.User.Password, cancellationToken);
                user.Password = string.Empty;
                request.User = user;
                return await TokenService.GenerateToken(request, cancellationToken);
            }
            catch(ArgumentException e)
            {
                throw new OrderException(e.Message);
            }
        }
    }
}
