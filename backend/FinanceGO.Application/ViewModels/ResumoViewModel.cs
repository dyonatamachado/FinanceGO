using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using FinanceGO.Core.Entities;
using FinanceGO.Core.Enums;

namespace FinanceGO.Application.ViewModels
{
    public class ResumoViewModel
    {
        public ResumoViewModel(int mes, int ano, List<Despesa> despesasDoMes, List<Receita> receitasDoMes)
        {
            Mes = mes;
            Ano = ano;
            ReceitasTotal = SetReceitasTotal(receitasDoMes);
            DespesasTotal = SetDespesasTotal(despesasDoMes);
            Saldo = ReceitasTotal - DespesasTotal;
            DespesasByCategoria = SetDespesasByCategoria(despesasDoMes);
        }

        private Dictionary<Categoria, double> SetDespesasByCategoria(List<Despesa> despesasDoMes)
        {
            var despesasByCategoria = new Dictionary<Categoria, double>();

            if(despesasDoMes.Count == 0) return null;

            var query = despesasDoMes
                .GroupBy(d => d.Categoria)
                .Select(d => 
                    new 
                    { 
                        Categoria = d.Key, 
                        Valor = d.Sum(d => d.Valor)
                    });
            

            foreach (var item in query) 
                despesasByCategoria.Add(item.Categoria, item.Valor);

            return despesasByCategoria;
        }

        private double SetDespesasTotal(List<Despesa> despesasDoMes)
        {
            if(despesasDoMes.Count == 0) return 0;

            return despesasDoMes.Sum(d => d.Valor);
        }

        private double SetReceitasTotal(List<Receita> receitasDoMes)
        {
            if(receitasDoMes.Count == 0) return 0;
            
            return receitasDoMes.Sum(r => r.Valor);
        }

        [Required]
        public int Mes { get; private set; }
        [Required]
        public int Ano { get; private set; }
        [Required]
        public double ReceitasTotal { get; private set; }
        [Required]
        public double DespesasTotal { get; private set; }
        [Required]
        public double Saldo { get; private set; }
        [Required]
        public Dictionary<Categoria, double> DespesasByCategoria { get; private set;}
    }
}