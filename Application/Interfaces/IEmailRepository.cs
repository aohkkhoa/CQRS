using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IEmailRepository
    {
        void Send(string to, string subject, string html, string from = null);
        string randomTokenString();
    }
}
