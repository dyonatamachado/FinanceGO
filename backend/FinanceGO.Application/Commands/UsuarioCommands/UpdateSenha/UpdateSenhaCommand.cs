using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FinanceGO.Core.Results;
using MediatR;

namespace FinanceGO.Application.Commands.UsuarioCommands.UpdateSenha
{
    public class UpdateSenhaCommand : IRequest<Result>
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string SenhaAtual { get; set; }
        public string NovaSenha { get; set; }

    }
}