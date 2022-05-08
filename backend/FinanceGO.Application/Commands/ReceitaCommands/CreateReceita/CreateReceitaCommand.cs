using System;
using System.ComponentModel.DataAnnotations;
using FinanceGO.Core.Results;
using MediatR;

namespace FinanceGO.Application.Commands.ReceitaCommands.CreateReceita
{
    public class CreateReceitaCommand : IRequest<Result>
    {
        public CreateReceitaCommand(string descricao, double valor, DateTime data)
        {
            Descricao = descricao;
            Valor = valor;
            Data = data;
        }

        [Required]
        public string Descricao { get; set; }
        [Required]
        public double Valor { get; set; }
        [Required]
        public DateTime Data { get; set; }
    }
}