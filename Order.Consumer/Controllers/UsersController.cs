using MediatR;
using Microsoft.AspNetCore.Mvc;
using Order.Application;
using Order.Application.Requests;
using Order.Application.Settings;
using Order.Domain.Models;
using Order.Infra.Repositories.Interfaces;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Order.Consumer.Controllers
{
    public class UsersController : Controller
    {
        //TODO: Terminar código da autenticação no front end.
        private readonly IUserRepository _userRepository;
        public UsersController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<IActionResult> UserExists(string username, CancellationToken cancellationToken = default)
        {
            try
            {
                return Ok(await UserExistsAsync(username, cancellationToken));
            }catch(OrderException e)
            {
                return BadRequest(e.Message);
            }
        }

        public async Task<IActionResult> NewUser([FromBody] User user, CancellationToken cancellationToken = default)
        {
            try
            {
                if (await UserExistsAsync(user.Username, cancellationToken))
                    throw new InvalidOperationException("Username already exists!");
                await _userRepository.Add(user, cancellationToken);
                return Created("/", user);
            }
            catch (OrderException e)
            {
                return BadRequest(e.Message);
            }
        }

        public async Task<IActionResult> Login([FromBody] User user, [FromServices] IMediator mediator,
            [FromServices] TokenSettings tokenSettings, CancellationToken cancellationToken = default)
        {
            try
            {
                var token = await mediator.Send(new TokenRequest() { TokenSettings = tokenSettings, User = user }, cancellationToken);
                return Ok(token);
            }
            catch (OrderException e)
            {
                return BadRequest(e.Message);
            }
        }

        private async Task<bool> UserExistsAsync(string username, CancellationToken cancellationToken = default)
        {
            return await _userRepository.UserExists(username, cancellationToken);
        }
    }
}
