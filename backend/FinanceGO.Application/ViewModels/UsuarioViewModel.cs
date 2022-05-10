using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FinanceGO.Application.ViewModels
{
    public class UsuarioViewModel
    {
        public UsuarioViewModel(int id, string nome, string email, DateTime dataDeNascimento)
        {
            Id = id;
            Nome = nome;
            Email = email;
            DataDeNascimento = dataDeNascimento;
        }

        [Required]
        public int Id { get; private set; }
        [Required]
        public string Nome { get; private set; }
        [Required]
        public string Email { get; private set; }
        [Required]
        public DateTime DataDeNascimento { get; private set; }
    }
}