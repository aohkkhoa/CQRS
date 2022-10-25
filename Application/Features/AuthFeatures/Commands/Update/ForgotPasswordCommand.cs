using Application.Exceptions;
using Application.Interfaces;
using Domain.Models.DTO;
using MailKit.Security;
using MediatR;
using Microsoft.Extensions.Options;
using MimeKit;
using MimeKit.Text;
using Shared.Wrapper;
using System.Security.Cryptography;
using SmtpClient = MailKit.Net.Smtp.SmtpClient;

namespace Application.Features.AuthFeatures.Commands.Update
{
    public class ForgotPasswordCommand : IRequest<Result<string>>
    {
        public string Email { get; set; }
    }

    public class ForgotPasswordCommandHandle : IRequestHandler<ForgotPasswordCommand, Result<string>>
    {
        private readonly IUserRepository _userRepository;
        private readonly ApplicationSettings _appSettings;

        public ForgotPasswordCommandHandle(IOptions<ApplicationSettings> appSettings, IUserRepository userRepository)
        {
            _appSettings = appSettings.Value; ;
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
            var email = new MimeMessage();
            email.From.Add(MailboxAddress.Parse(_appSettings.EmailFrom));
            email.To.Add(MailboxAddress.Parse(command.Email));
            email.Subject = "Sign-up Verification API - Reset Password";
            email.Body = new TextPart(TextFormat.Html) { Text = $@"<h4>Reset Password Email</h4> {resetToken}" };

            // send email
            using var smtp = new SmtpClient();
            smtp.Connect(_appSettings.SmtpHost, _appSettings.SmtpPort, SecureSocketOptions.StartTls);
            smtp.Authenticate(_appSettings.SmtpUser, _appSettings.SmtpPass);
            smtp.Send(email);
            smtp.Disconnect(true);
            return await Result<string>.SuccessAsync("SendMail Success !");
        }
    }
}