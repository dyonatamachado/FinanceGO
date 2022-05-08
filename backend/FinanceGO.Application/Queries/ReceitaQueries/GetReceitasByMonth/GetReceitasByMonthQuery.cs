using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FinanceGO.Application.ViewModels;
using MediatR;

namespace FinanceGO.Application.Queries.ReceitaQueries.GetReceitasByMonth
{
    public class GetReceitasByMonthQuery : IRequest<List<ReceitaViewModel>>
    {
        public GetReceitasByMonthQuery(int mes, int ano)
        {
            Mes = mes;
            Ano = ano;
        }

        public int Mes { get; private set; }
        public int Ano { get; private set; }
    }
}