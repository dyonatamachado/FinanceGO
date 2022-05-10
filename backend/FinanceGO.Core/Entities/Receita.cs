using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FinanceGO.Core.Entities
{
    public class Receita
    {
        public Receita(string descricao, double valor, DateTime data, int usuarioId)
        {
            Descricao = descricao;
            Valor = valor;
            Data = data;
            UsuarioId = usuarioId;
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
        public int UsuarioId { get; private set; }
        [Required]
        public Usuario Usuario { get; private set; }
    }
}