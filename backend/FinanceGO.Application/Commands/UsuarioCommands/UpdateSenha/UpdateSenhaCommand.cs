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
        public int Id { get; private set; }
        public string Email { get; private set; }
        public string SenhaAtual { get; private set; }
        public string NovaSenha { get; private set; }

    }
}