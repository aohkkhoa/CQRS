using Application.Features.BookFeatures.Commands.Create;
using Application.Interfaces;
using Domain.Models.DTO;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Domain.Models.Entities;
using Shared.Wrapper;

namespace Application.Features.AuthFeatures.Commands.Create
{
    public class SignUpCommand : IRequest<Result<User>>
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
    }

    public class SignUpCommandHandle : IRequestHandler<SignUpCommand, Result<User>>
    {
        private readonly IAuthRepository _authRepository;

        public SignUpCommandHandle(IAuthRepository authRepository)
        {
            _authRepository = authRepository;
        }

        public async Task<Result<User>> Handle(SignUpCommand command, CancellationToken cancellationToken)
        {
            var user = new User();
            using (HMACSHA512? hmac = new HMACSHA512())
            {
                user.PasswordSalt = hmac.Key;
                user.PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(command.Password));
            }

            user.userName = command.UserName;
            user.address = command.Address;
            user.phone = command.Phone;
            await _authRepository.Register(user);
            return await Result<User>.SuccessAsync(user, "Register Success");
        }
    }
}