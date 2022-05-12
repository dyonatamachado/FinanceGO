using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using FinanceGO.Application.InputModels.UsuarioInputModels;
using FinanceGO.Core.Results;
using MediatR;

namespace FinanceGO.Application.Commands.UsuarioCommands.UpdateUsuario
{
    public class UpdateUsuarioCommand : IRequest<Result>
    {
        public UpdateUsuarioCommand(int id, UpdateUsuarioInputModel inputModel)
        {
            Id = id;
            Nome = inputModel.Nome;
            DataDeNascimento = inputModel.DataDeNascimento;
            Email = inputModel.Email;
        }

        public int Id { get; private set; }
        public string Nome { get; private set; }
        public string Email { get; private set; }
        public DateTime DataDeNascimento { get; private set; }
    }
}