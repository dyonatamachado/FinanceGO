using System;
using System.ComponentModel.DataAnnotations;
using FinanceGO.Application.InputModels.ReceitaInputModels;
using FinanceGO.Core.Results;
using FinanceGO.Core.UserServices;
using MediatR;

namespace FinanceGO.Application.Commands.ReceitaCommands.CreateReceita
{
    public class CreateReceitaCommand : IRequest<Result>
    {
        public CreateReceitaCommand(CreateReceitaInputModel inputModel, int usuarioId)
        {
            Descricao = inputModel.Descricao;
            Valor = inputModel.Valor;
            Data = inputModel.Data;
            UsuarioId = usuarioId;
        }

        public string Descricao { get; set; }
        public double Valor { get; set; }
        public DateTime Data { get; set; }
        public int UsuarioId { get; private set; }
    }
}