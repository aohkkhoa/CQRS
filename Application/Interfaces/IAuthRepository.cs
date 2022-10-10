using BookManagement2.Models.Entities;
using Domain.Models.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IAuthRepository
    {
        void ForgotPassWord(string email);
        User LoginAsync(string userName, string password);
        Task<User> Register(User user);
        Task<Boolean> ResetPassWord(string token, string newPassword);
    }
}
