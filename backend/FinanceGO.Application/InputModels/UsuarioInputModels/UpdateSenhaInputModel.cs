using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FinanceGO.Application.InputModels.UsuarioInputModels
{
    public class UpdateSenhaInputModel
    {
        public UpdateSenhaInputModel(string email, string senhaAtual, string novaSenha)
        {
            Email = email;
            SenhaAtual = senhaAtual;
            NovaSenha = novaSenha;
        }

        [Required]
        public string Email { get; set; }
        [Required]
        public string SenhaAtual { get; set; }
        [Required]
        public string NovaSenha { get; set; }
    }
}