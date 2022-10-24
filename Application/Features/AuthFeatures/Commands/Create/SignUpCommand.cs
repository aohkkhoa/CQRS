using Application.Interfaces;
using MediatR;
using System.Security.Cryptography;
using System.Text;
using Domain.Models.Entities;
using Shared.Wrapper;

namespace Application.Features.AuthFeatures.Commands.Create
{
    public class SignUpCommand : IRequest<Result<User>>
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
    }

    public class SignUpCommandHandle : IRequestHandler<SignUpCommand, Result<User>>
    {
        private readonly IUserRepository _userRepository;

        public SignUpCommandHandle(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<Result<User>> Handle(SignUpCommand command, CancellationToken cancellationToken)
        {
            var user = new User();
            using (HMACSHA512? hmac = new HMACSHA512())
            {
                user.PasswordSalt = hmac.Key;
                user.PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(command.Password));
            }

            user.Username = command.Username;
            user.Address = command.Address;
            user.Phone = command.Phone;
            _userRepository.Insert(user);
            await _userRepository.Save();
            return await Result<User>.SuccessAsync(user, "Register Success !");
        }
    }
}