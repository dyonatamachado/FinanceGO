using FinanceGO.Core.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinanceGO.Application.InputModels.DespesaInputModels
{
    public class CreateDespesaInputModel
    {
        public CreateDespesaInputModel(string descricao, double valor, DateTime data, Categoria categoria)
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
        [Required]
        [Range(0, 7, ErrorMessage = "O dado de Categoria não é obrigatório, porém só aceita valores inteiros entre 0 e 7.")]
        public Categoria Categoria { get; set; }
    }
}
