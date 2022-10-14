using Domain.Models.Entities;

namespace Application.Interfaces
{
    public interface IAuthRepository
    {
        void ForgotPassWord(string email);
        User LoginAsync(string userName, string password);
        Task<User> Register(User user);
        Task<Boolean> ResetPassWord(string token, string newPassword);
        List<string> GetRoleByUser(int userId);
        List<string> GetMenuByUser(List<string> roleName);
    }
}
