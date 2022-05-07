using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FinanceGO.Application.ViewModels;
using MediatR;

namespace FinanceGO.Application.Queries.DespesaQueries.GetDespesasByMonth
{
    public class GetDespesasByMonthQuery : IRequest<List<DespesaViewModel>>
    {
        public GetDespesasByMonthQuery(int mes, int ano)
        {
            Mes = mes;
            Ano = ano;
        }

        public int Mes { get; private set; }
        public int Ano { get; private set; }
    }
}