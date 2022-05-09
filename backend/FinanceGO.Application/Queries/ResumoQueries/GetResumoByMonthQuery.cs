using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FinanceGO.Application.ViewModels;
using FinanceGO.Core.Results;
using MediatR;

namespace FinanceGO.Application.Queries.ResumoQueries
{
    public class GetResumoByMonthQuery : IRequest<ResumoViewModel>
    {
        public GetResumoByMonthQuery(int mes, int ano)
        {
            Mes = mes;
            Ano = ano;
        }

        public int Mes { get; private set; }
        public int Ano { get; private set; }
    }
}