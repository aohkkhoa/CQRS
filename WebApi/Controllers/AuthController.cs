using Application.Features.AuthFeatures.Commands.Create;
using Application.Features.AuthFeatures.Commands.Update;
using Application.Features.AuthFeatures.Queries.Login;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : BaseController
    {
        public AuthController(IMediator mediator) : base(mediator)
        {
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(SignUpCommand command)
        {
            return Ok(await _mediator.Send(command));
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginQuery command)
        {
            return Ok(await _mediator.Send(command));
        }

        [HttpPost("forgot-password")]
        public async Task<IActionResult> ForgotPassword(ForgotPasswordCommand command)
        {
            return Ok(await _mediator.Send(command));
        }

        [HttpPost("Reset-password")]
        public async Task<IActionResult> ResetPassWord(ResetPassCommand command)
        {
            return Ok(await _mediator.Send(command));
        }
    }
}