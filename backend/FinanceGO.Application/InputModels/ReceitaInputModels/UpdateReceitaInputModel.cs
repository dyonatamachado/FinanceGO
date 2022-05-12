using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FinanceGO.Application.InputModels.ReceitaInputModels
{
    public class UpdateReceitaInputModel
    {
        public UpdateReceitaInputModel(string descricao, double valor, DateTime data)
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