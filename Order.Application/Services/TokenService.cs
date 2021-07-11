using Microsoft.IdentityModel.Tokens;
using Order.Application.Requests;
using Order.Application.Responses;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Order.Application.Services
{
    public static class TokenService
    {
        public async static Task<TokenResponse> GenerateToken(TokenRequest tokenRequest, CancellationToken cancellationToken = default)
        {
            return await Task.Run(() =>
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                var key = Encoding.ASCII.GetBytes(tokenRequest.TokenSettings.TokenSecret);
                var tokenDecriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(new Claim[]
                    {
                    new Claim(ClaimTypes.Name, tokenRequest.User.Username.ToString())
                    }),
                    Expires = DateTime.UtcNow.AddHours(2),
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
                };

                var token = tokenHandler.CreateToken(tokenDecriptor);
                var response = new TokenResponse()
                {
                    Token = tokenHandler.WriteToken(token),
                    ValidFrom = token.ValidFrom,
                    ValidTo = token.ValidTo
                };
                return response;
            }, cancellationToken);
        }
    }
}
