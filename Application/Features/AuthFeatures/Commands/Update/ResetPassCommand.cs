using System.Security.Cryptography;
using System.Text;
using Application.Interfaces;
using MediatR;
using Shared.Wrapper;

namespace Application.Features.AuthFeatures.Commands.Update
{
    public class ResetPassCommand : IRequest<Result<string>>
    {
        public string token { get; set; }
        public string newPassword { get; set; }
    }

    public class ResetPassCommandHandle : IRequestHandler<ResetPassCommand, Result<string>>
    {
        private readonly IUserRepository _userRepository;

        public ResetPassCommandHandle(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<Result<string>> Handle(ResetPassCommand command, CancellationToken cancellationToken)
        {
            var user = _userRepository.Entities.FirstOrDefault(u => u.ResetToken == command.token);
            if (user is not null)
            {
                user.ResetToken = "";
                using (HMACSHA512? hmac = new HMACSHA512())
                {
                    user.PasswordSalt = hmac.Key;
                    user.PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(command.newPassword));
                }

                await _userRepository.Save();
                return await Result<string>.SuccessAsync("Change password complete !");
            }

            return await Result<string>.FailAsync("Change password faild !");
        }
    }
}