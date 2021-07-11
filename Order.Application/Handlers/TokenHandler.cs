using MediatR;
using Order.Application.Requests;
using Order.Application.Responses;
using Order.Application.Services;
using Order.Infra.Repositories.Interfaces;
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
            var user = await _userRepository.GetByUsernameAndPassword(request.User.Username, request.User.Password, cancellationToken);
            request.User = user;
            return await TokenService.GenerateToken(request, cancellationToken);
        }
    }
}
