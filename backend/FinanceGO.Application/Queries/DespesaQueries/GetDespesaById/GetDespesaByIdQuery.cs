using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FinanceGO.Application.ViewModels;
using MediatR;

namespace FinanceGO.Application.Queries.DespesaQueries.GetDespesaById
{
    public class GetDespesaByIdQuery : IRequest<DespesaViewModel>
    {
        public GetDespesaByIdQuery(int id)
        {
            Id = id;
        }

        public int Id { get; private set; }
    }
}