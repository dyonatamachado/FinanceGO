using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FinanceGO.Application.InputModels.UsuarioInputModels
{
    public class DeleteUsuarioInputModel
    {
        public DeleteUsuarioInputModel(string email, string password)
        {
            Email = email;
            Password = password;
        }

        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }  
    }
}