using System;
using System.ComponentModel.DataAnnotations;
using FinanceGO.Application.ViewModels;
using MediatR;

namespace FinanceGO.Application.Commands.UsuarioCommands.CreateUsuario
{
    public class CreateUsuarioCommand : IRequest<UsuarioViewModel>
    {
        public CreateUsuarioCommand(string nome, string email, string senha, DateTime dataDeNascimento)
        {
            Nome = nome;
            Email = email;
            Senha = senha;
            DataDeNascimento = dataDeNascimento;
        }

        [Required]
        public string Nome { get; private set; }
        [Required]
        public string Email { get; private set; }
        [Required]
        public string Senha { get; private set; }
        [Required]
        public DateTime DataDeNascimento { get; private set; }
    }
}