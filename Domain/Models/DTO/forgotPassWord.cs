namespace Domain.Models.DTO
{
    public class forgotPassWord
    {
        public string ResetToken { get; set; }
        public DateTime ResetTokenExpires { get; set; }
    }
}
