using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using FinanceGO.Application.InputModels.DespesaInputModels;
using FinanceGO.Application.ViewModels;
using FinanceGO.Core.Enums;
using FinanceGO.Core.Results;
using FinanceGO.Core.UserServices;
using MediatR;

namespace FinanceGO.Application.Commands.DespesaCommands.CreateDespesa
{
    public class CreateDespesaCommand : IRequest<Result>
    {
        public CreateDespesaCommand(CreateDespesaInputModel inputModel, int usuarioId)
        {
            Descricao = inputModel.Descricao;
            Valor = inputModel.Valor;
            Data = inputModel.Data;
            Categoria = inputModel.Categoria;
            UsuarioId = usuarioId;
        }

        public string Descricao { get; set; }
        public double Valor { get; set; }
        public DateTime Data { get; set; }
        public Categoria Categoria { get; set; }
        public int UsuarioId { get; private set; }
    }
}