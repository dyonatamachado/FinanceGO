using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FinanceGO.Application.ViewModels
{
    public class LoginUsuarioViewModel
    {
        public LoginUsuarioViewModel(string nome, string token)
        {
            Nome = nome;
            Token = token;
        }

        public string Nome { get; private set; }
        public string Token { get; private set; }
    }
}