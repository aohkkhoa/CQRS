using Application.Interfaces;
using BookManagement2.Models.Entities;
using Domain.Models.DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.IdentityModel.Tokens;
using Persistence.Context;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using Application.Exceptions;
using WebApi;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace Persistence.Repositories
{
    public class AuthRepository : IAuthRepository
    {

        private readonly ApplicationDbContext _context;

        private readonly IEmailRepository _emailRepository;
        

        public AuthRepository(ApplicationDbContext context, IEmailRepository emailRepository)
        {
            _context=context;
            _emailRepository=emailRepository;
        }

        public User LoginAsync(string userName, string password)
        {
            var user = (from b in _context.Users
                        where b.userName == userName
                        select b).FirstOrDefault();

            if (user == null)
            {
                throw new ApiException("Username or pass was invalid");
            }

            var match = CheckPassword(password, user);

            if (!match)
            {
                throw new ApiException("Username or pass was invalid");
            }
            return user;
        }

        private Boolean CheckPassword(string passWord, User user)
        {
            bool result;
            using (HMACSHA512? hmac = new HMACSHA512(user.PasswordSalt))
            {
                var compute = hmac.ComputeHash(Encoding.UTF8.GetBytes(passWord));
                result = compute.SequenceEqual(user.PasswordHash);
            }
            return result;
        }

        public async Task<User> Register(User user)
        {
            _context.Users.Add(user);
            await _context.SaveChanges();
            return user;
        }
        private string randomTokenString()
        {
            using var rngCryptoServiceProvider = new RNGCryptoServiceProvider();
            var randomBytes = new byte[40];
            rngCryptoServiceProvider.GetBytes(randomBytes);
            // convert random bytes to hex string
            return BitConverter.ToString(randomBytes).Replace("-", "");
        }
        public void ForgotPassWord(string email)
        {
            var a = _emailRepository.randomTokenString();
            _emailRepository.Send(
               to: email,
               subject: "Sign-up Verification API - Reset Password",
               html: $@"<h4>Reset Password Email</h4>
                         {a}"
           );
        }

        public async Task<bool> ResetPassWord(string token, string newPassword)
        {
            var a = _context.Users.FirstOrDefault(a => a.ResetToken == token);
            if(a!=null)
            {
                a.ResetToken = "";
                using (HMACSHA512? hmac = new HMACSHA512())
                {
                    a.PasswordSalt = hmac.Key;
                    a.PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(newPassword));
                }
                await _context.SaveChanges();
                return true;
            }
            return false;
        }
    }
}
