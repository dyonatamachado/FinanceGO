using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using FinanceGO.Application.InputModels.DespesaInputModels;
using FinanceGO.Core.Enums;
using FinanceGO.Core.Results;
using FinanceGO.Core.UserServices;
using MediatR;

namespace FinanceGO.Application.Commands.DespesaCommands.UpdateDespesa
{
    public class UpdateDespesaCommand : IRequest<Result>
    {
        public UpdateDespesaCommand(UpdateDespesaInputModel inputModel, int id)
        {
            Id = id;
            Descricao = inputModel.Descricao;
            Valor = inputModel.Valor;
            Data = inputModel.Data;
            Categoria = inputModel.Categoria;
        }

        public int Id { get; private set; }       
        public string Descricao { get; private set; }
        public double Valor { get; private set; }
        public DateTime Data { get; private set; }
        public Categoria Categoria { get; private set; }
    }
}