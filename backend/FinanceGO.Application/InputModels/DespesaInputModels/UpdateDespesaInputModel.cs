using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using FinanceGO.Core.Enums;

namespace FinanceGO.Application.InputModels.DespesaInputModels
{
    public class UpdateDespesaInputModel
    {
        public UpdateDespesaInputModel(string descricao, double valor, DateTime data, Categoria categoria)
        {
            Descricao = descricao;
            Valor = valor;
            Data = data;
            Categoria = categoria;
        }

        [Required]
        public string Descricao { get; set; }
        [Required]
        public double Valor { get; set; }
        [Required]
        public DateTime Data { get; set; }
        [Range(0, 7, ErrorMessage = "O dado de Categoria não é obrigatório, porém só aceita valores inteiros entre 0 e 7.")]
        public Categoria Categoria { get; set; }
    }
}