using Application.Exceptions;
using Application.Interfaces;
using MediatR;
using Shared.Wrapper;
using System.Security.Cryptography;

namespace Application.Features.AuthFeatures.Commands.Update
{
    public class ForgotPasswordCommand : IRequest<Result<string>>
    {
        public string Email { get; set; }
    }

    public class ForgotPasswordCommandHandle : IRequestHandler<ForgotPasswordCommand, Result<string>>
    {
        private readonly IEmailRepository _emailRepository;
        private readonly IUserRepository _userRepository;

        public ForgotPasswordCommandHandle(IEmailRepository emailRepository, IUserRepository userRepository)
        {
            _emailRepository = emailRepository;
            _userRepository = userRepository;
        }

        public async Task<Result<string>> Handle(ForgotPasswordCommand command, CancellationToken cancellationToken)
        {
            var rngCryptoServiceProvider = new RNGCryptoServiceProvider();
            var randomBytes = new byte[40];
            rngCryptoServiceProvider.GetBytes(randomBytes);
            var resetToken = BitConverter.ToString(randomBytes).Replace("-", "");

            var user = _userRepository.Entities.FirstOrDefault(u => u.Email == command.Email);
            if (user is null)
            {
                throw new ApiException("This email not register !");
            }

            user.ResetToken = resetToken;
            await _userRepository.Save();
            _emailRepository.Send(command.Email, "Sign-up Verification API - Reset Password", $@"<h4>Reset Password Email</h4> {resetToken}");
            return await Result<string>.SuccessAsync("SendMail Success !");
        }
    }
}