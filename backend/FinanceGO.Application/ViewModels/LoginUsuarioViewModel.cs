using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FinanceGO.Application.ViewModels
{
    public class LoginUsuarioViewModel
    {
        public LoginUsuarioViewModel(string email, string token)
        {
            Email = email;
            Token = token;
        }

        public string Email { get; private set; }
        public string Token { get; private set; }
    }
}