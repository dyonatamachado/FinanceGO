using FinanceGO.Core.Results;
using MediatR;

namespace FinanceGO.Application.Queries.DespesaQueries.GetDespesaById
{
    public class GetDespesaByIdQuery : IRequest<Result>
    {
        public GetDespesaByIdQuery(int id)
        {
            Id = id;
        }

        public int Id { get; private set; }
    }
}