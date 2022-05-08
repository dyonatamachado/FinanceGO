using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FinanceGO.Application.ViewModels;
using MediatR;

namespace FinanceGO.Application.Queries.ReceitaQueries.GetReceitas
{
    public class GetReceitasQuery : IRequest<List<ReceitaViewModel>>
    {
        
    }
}