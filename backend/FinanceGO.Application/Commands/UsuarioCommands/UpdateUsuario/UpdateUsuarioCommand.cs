using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using FinanceGO.Core.Results;
using MediatR;

namespace FinanceGO.Application.Commands.UsuarioCommands.UpdateUsuario
{
    public class UpdateUsuarioCommand : IRequest<Result>
    {
        public UpdateUsuarioCommand(int id, string nome, DateTime dataDeNascimento, string email)
        {
            Id = id;
            Nome = nome;
            DataDeNascimento = dataDeNascimento;
            Email = email;
        }

        [Required]
        public int Id { get; set; }
        [Required]
        public string Nome { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public DateTime DataDeNascimento { get; set; }
    }
}