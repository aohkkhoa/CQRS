using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models.DTO
{
    public class forgotPassWord
    {
        public string ResetToken { get; set; }
        public DateTime ResetTokenExpires { get; set; }
    }
}
