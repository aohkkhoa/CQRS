﻿using Application.Interfaces;
using Domain.Models.DTO;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Shared.Wrapper;
using WebApi;

namespace Application.Features.AuthFeatures.Queries.Login
{
    public class LoginQuery : IRequest<Result<TokenResponse>>
    {
        public string UserName { get; set; }
        public string Password { get; set; }
    }
    public class LoginQueryHandle : IRequestHandler<LoginQuery, Result<TokenResponse>>
    {
        private readonly ApplicationSettings _appSettings;

        private readonly IAuthRepository _authRepository;
        public LoginQueryHandle(IAuthRepository authRepository, IOptions<ApplicationSettings> appSettings)
        {
            _authRepository = authRepository;
            _appSettings = appSettings.Value;
        }
        public async Task<Result<TokenResponse>> Handle(LoginQuery command, CancellationToken cancellationToken)
        {

            //var user = _authRepository.LoginAsync(command.UserName, command.Password);
            var user = _authRepository.LoginAsync("user1", "234");

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(this._appSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[] { new Claim(ClaimTypes.NameIdentifier, user.userName),new Claim(ClaimTypes.Role, "Admin") }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha512Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            var encrypterToken = tokenHandler.WriteToken(token);
            /*HttpContext.SessionSession.SetString("JWToken", encrypterToken);
            HttpContext.Session.SetString("username", user.userName);*/
            TokenResponse tokenResponse = new TokenResponse() { Token=encrypterToken };
            return Result<TokenResponse>.Success(tokenResponse, "login sucess");
        }
    }
}