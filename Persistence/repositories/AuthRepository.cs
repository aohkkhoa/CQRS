using Application.Interfaces;
using Persistence.Context;
using System.Security.Cryptography;
using System.Text;
using Application.Exceptions;
using Domain.Models.Entities;

namespace Persistence.Repositories
{
    public class AuthRepository : IAuthRepository
    {
        private readonly ApplicationDbContext _context;

        private readonly IEmailRepository _emailRepository;


        public AuthRepository(ApplicationDbContext context, IEmailRepository emailRepository)
        {
            _context = context;
            _emailRepository = emailRepository;
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

        private string RandomTokenString()
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
            if (a != null)
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

        public List<string> GetRoleByUser(int userId)
        {
            var listRoleId = _context.UserRoles.Where(a => a.UserId == userId).Select(a => a.RoleId).ToList();
            var listRoleString = (from lrs in _context.Roles
                where listRoleId.Contains(lrs.Id)
                select lrs.RoleName).ToList();
            if (listRoleId == null) throw new ApiException("Role Not Found!");
            return listRoleString;
        }

        public List<string> GetMenuByUser(List<string> roleName)
        {
            var a = (from m in _context.Menus
                join p in _context.Permissions on m.Id equals p.MenuId
                join ur in _context.UserRoles on p.RoleId equals ur.RoleId
                join r in _context.Roles on ur.RoleId equals r.Id
                where roleName.Contains(r.RoleName)
                select m.Name).ToList();
            if (a == null) throw new ApiException("Menu Not Found!");
            return a;
        }
    }
}