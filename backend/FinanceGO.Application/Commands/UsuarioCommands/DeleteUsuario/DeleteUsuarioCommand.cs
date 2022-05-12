using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FinanceGO.Core.Results;
using MediatR;

namespace FinanceGO.Application.Commands.UsuarioCommands.DeleteUsuario
{
    public class DeleteUsuarioCommand : IRequest<Result>
    {
        public DeleteUsuarioCommand(int id, string email, string password)
        {
            Id = id;
            Email = email;
            Password = password;
        }

        public int Id { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }    
    }
}