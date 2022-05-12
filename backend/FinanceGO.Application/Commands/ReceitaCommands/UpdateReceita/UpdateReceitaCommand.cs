using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using FinanceGO.Application.InputModels.ReceitaInputModels;
using FinanceGO.Core.Results;
using MediatR;

namespace FinanceGO.Application.Commands.ReceitaCommands.UpdateReceita
{
    public class UpdateReceitaCommand : IRequest<Result>
    {
        public UpdateReceitaCommand(int id, UpdateReceitaInputModel inputModel)
        {
            Id = id;
            Descricao = inputModel.Descricao;
            Valor = inputModel.Valor;
            Data = inputModel.Data;
        }

        public int Id { get; private set; }       
        public string Descricao { get; private set; }
        public double Valor { get; private set; }
        public DateTime Data { get; private set; }
    }
}