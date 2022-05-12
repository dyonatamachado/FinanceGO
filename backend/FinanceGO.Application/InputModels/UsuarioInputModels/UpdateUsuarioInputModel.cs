using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FinanceGO.Application.InputModels.UsuarioInputModels
{
    public class UpdateUsuarioInputModel
    {
        public UpdateUsuarioInputModel(string nome, string email, DateTime dataDeNascimento)
        {
            Nome = nome;
            Email = email;
            DataDeNascimento = dataDeNascimento;
        }

        [Required]
        public string Nome { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public DateTime DataDeNascimento { get; set; }
    }
}