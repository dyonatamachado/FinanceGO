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

        public int Id { get; set; }
        public string Descricao { get; set; }
        public double Valor { get; set; }
        public DateTime Data { get; set; }
        public int UsuarioId { get; private set; }
        public Usuario Usuario { get; private set; }
    }
}