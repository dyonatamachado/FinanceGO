using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FinanceGO.Application.ViewModels;
using MediatR;

namespace FinanceGO.Application.Queries.ReceitaQueries.GetReceitaById
{
    public class GetReceitaByIdQuery : IRequest<ReceitaViewModel>
    {
        public GetReceitaByIdQuery(int id)
        {
            Id = id;
        }

        public int Id { get; private set; }
    }
}