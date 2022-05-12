using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FinanceGO.Application.InputModels.UsuarioInputModels;
using FinanceGO.Core.Results;
using MediatR;

namespace FinanceGO.Application.Commands.UsuarioCommands.DeleteUsuario
{
    public class DeleteUsuarioCommand : IRequest<Result>
    {
        public DeleteUsuarioCommand(int id, DeleteUsuarioInputModel inputModel)
        {
            Id = id;
            Email = inputModel.Email;
            Password = inputModel.Password;
        }

        public int Id { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }    
    }
}