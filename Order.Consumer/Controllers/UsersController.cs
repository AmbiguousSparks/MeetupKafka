using MediatR;
using Microsoft.AspNetCore.Mvc;
using Order.Application;
using Order.Application.Requests;
using Order.Application.Responses;
using Order.Application.Settings;
using Order.Consumer.Models;
using Order.Domain.Models;
using Order.Infra.Repositories.Interfaces;
using System;
using System.Collections.Generic;
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

        [Route("api/Users/UserExists"), HttpPost]
        public async Task<IActionResult> UserExists([FromBody]User user, CancellationToken cancellationToken = default)
        {
            try
            {
                return Ok(new ResponseBuilder<bool>().Build(await UserExistsAsync(user.Username, cancellationToken)));
            }
            catch (OrderException e)
            {
                List<string> errors = new() { e.Message };
                return BadRequest(new ResponseBuilder<bool>().Build(false, System.Net.HttpStatusCode.BadRequest, true, errors));
            }
        }

        [Route("api/Users/New"), HttpPost]
        public async Task<IActionResult> NewUser([FromBody] User user, CancellationToken cancellationToken = default)
        {
            try
            {
                if (await UserExistsAsync(user.Username, cancellationToken))
                    throw new InvalidOperationException("Username already exists!");
                await _userRepository.Add(user, cancellationToken);
                return Created("/", new ResponseBuilder<User>().Build(user));
            }
            catch (OrderException e)
            {
                List<string> errors = new() { e.Message };
                return BadRequest(new ResponseBuilder<bool>().Build(false, System.Net.HttpStatusCode.BadRequest, true, errors));
            }
        }

        [Route("api/Users/Login"), HttpPost]
        public async Task<IActionResult> Login([FromBody] User user, [FromServices] IMediator mediator,
            [FromServices] TokenSettings tokenSettings, CancellationToken cancellationToken = default)
        {
            try
            {
                var token = await mediator.Send(new TokenRequest() { TokenSettings = tokenSettings, User = user }, cancellationToken);
                return Ok(new ResponseBuilder<TokenResponse>().Build(token));
            }
            catch (OrderException e)
            {
                List<string> errors = new() { e.Message };
                return BadRequest(new ResponseBuilder<bool>().Build(false, System.Net.HttpStatusCode.BadRequest, true, errors));
            }
        }

        private async Task<bool> UserExistsAsync(string username, CancellationToken cancellationToken = default)
        {
            return await _userRepository.UserExists(username, cancellationToken);
        }
    }
}
