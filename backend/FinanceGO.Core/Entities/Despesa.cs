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
        public Despesa(string descricao, double valor, DateTime data, Categoria categoria, int usuarioId)
        {
            Descricao = descricao;
            Valor = valor;
            Data = data;
            Categoria = categoria;
            UsuarioId = usuarioId;
        }

        public int Id { get; set; }
        public string Descricao { get; set; }
        public double Valor { get; set; }
        public DateTime Data { get; set; }
        public Categoria Categoria {get; set;}
        public int UsuarioId { get; private set; }
        public Usuario Usuario { get; private set; }
    }
}