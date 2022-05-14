using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FinanceGO.Application.ViewModels;
using FinanceGO.Core.Results;
using MediatR;

namespace FinanceGO.Application.Queries.ReceitaQueries.GetReceitaById
{
    public class GetReceitaByIdQuery : IRequest<Result>
    {
        public GetReceitaByIdQuery(int id)
        {
            Id = id;
        }

        public int Id { get; private set; }
    }
}