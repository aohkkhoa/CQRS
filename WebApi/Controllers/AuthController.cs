using Application.Features.AuthFeatures.Commands.Create;
using Application.Features.AuthFeatures.Commands.Update;
using Application.Features.AuthFeatures.Queries.Login;
using Application.Features.BookFeatures.Commands.Create;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private IValidator<CreateBookCommand> _validator;
        private readonly IMediator _mediator;

        public AuthController(IMediator mediator, IValidator<CreateBookCommand> validator)
        {
            _mediator = mediator;
            _validator = validator;
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
            /*_accountService.ForgotPassword(model, Request.Headers["origin"]);
            return Ok(new { message = "Please check your email for password reset instructions" });*/
            return Ok(await _mediator.Send(command));
        }

        [HttpPost("Reset-password")]
        public async Task<IActionResult> ResetPassWord(ResetPassCommand command)
        {
            /*_accountService.ForgotPassword(model, Request.Headers["origin"]);
            return Ok(new { message = "Please check your email for password reset instructions" });*/
            return Ok(await _mediator.Send(command));
        }
    }
}