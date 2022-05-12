using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FinanceGO.Application.InputModels.UsuarioInputModels;
using FinanceGO.Core.Results;
using MediatR;

namespace FinanceGO.Application.Commands.UsuarioCommands.UpdateSenha
{
    public class UpdateSenhaCommand : IRequest<Result>
    {
        public UpdateSenhaCommand(int id, UpdateSenhaInputModel inputModel)
        {
            Id = id;
            Email = inputModel.Email;
            SenhaAtual = inputModel.SenhaAtual;
            NovaSenha = inputModel.NovaSenha;
        }

        public int Id { get; private set; }
        public string Email { get; private set; }
        public string SenhaAtual { get; private set; }
        public string NovaSenha { get; private set; }
    }
}