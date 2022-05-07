using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FinanceGO.Application.ViewModels;
using MediatR;

namespace FinanceGO.Application.Queries.DespesaQueries.ReadDespesaById
{
    public class ReadDespesaByIdQuery : IRequest<DespesaViewModel>
    {
        public ReadDespesaByIdQuery(int id)
        {
            Id = id;
        }

        public int Id { get; private set; }
    }
}