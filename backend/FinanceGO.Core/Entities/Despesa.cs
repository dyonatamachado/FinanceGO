using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using FinanceGO.Core.Enums;

namespace FinanceGO.Core.Entities
{
    public class Despesa
    {
        public Despesa(string descricao, double valor, DateTime data, Categoria categoria = 0)
        {
            Descricao = descricao;
            Valor = valor;
            Data = data;
            Categoria = categoria;
        }

        [Key]
        [Required]
        public int Id { get; set; }
        [Required]
        public string Descricao { get; set; }
        [Required]
        public double Valor { get; set; }
        [Required]
        public DateTime Data { get; set; }
        [Required]
        public Categoria Categoria {get; set;}
    }
}