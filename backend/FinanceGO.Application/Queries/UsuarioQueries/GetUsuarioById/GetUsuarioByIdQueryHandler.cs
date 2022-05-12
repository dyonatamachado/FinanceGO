using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using FinanceGO.Application.ViewModels;
using FinanceGO.Core.Repositories.UsuarioRepositories;
using MediatR;

namespace FinanceGO.Application.Queries.UsuarioQueries.GetUsuarioById
{
    public class GetUsuarioByIdQueryHandler : IRequestHandler<GetUsuarioByIdQuery, UsuarioViewModel>
    {
        private readonly IUsuarioQueryRepository _queryRepository; 
        private readonly IMapper _mapper;

        public GetUsuarioByIdQueryHandler(IUsuarioQueryRepository queryRepository, IMapper mapper)
        {
            _queryRepository = queryRepository;
            _mapper = mapper;
        }

        public async Task<UsuarioViewModel> Handle(GetUsuarioByIdQuery request, CancellationToken cancellationToken)
        {
            var usuario = await _queryRepository.GetUsuarioByIdAsync(request.Id);
            if(usuario == null) return null;

            return _mapper.Map<UsuarioViewModel>(usuario);
        }
    }
}