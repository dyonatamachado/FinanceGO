using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using FinanceGO.Core.Results;
using MediatR;

namespace FinanceGO.Application.Commands.ReceitaCommands.UpdateReceita
{
    public class UpdateReceitaCommand : IRequest<Result>
    {
        public UpdateReceitaCommand(int id, string descricao, double valor, DateTime data)
        {
            Id = id;
            Descricao = descricao;
            Valor = valor;
            Data = data;
        }

        [Required]
        public int Id { get; set; }       
        [Required]
        public string Descricao { get; set; }
        [Required]
        public double Valor { get; set; }
        [Required]
        public DateTime Data { get; set; }
    }
}