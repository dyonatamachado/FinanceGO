using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FinanceGO.Core.Entities
{
    public class Receita
    {
        public Receita(string descricao, double valor, DateTime data)
        {
            Descricao = descricao;
            Valor = valor;
            Data = data;
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
    }
}