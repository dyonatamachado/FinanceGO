using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using FinanceGO.Core.Repositories.UsuarioRepositories;
using FinanceGO.Core.Results;
using MediatR;

namespace FinanceGO.Application.Commands.UsuarioCommands.UpdateUsuario
{
    public class UpdateUsuarioCommandHandler : IRequestHandler<UpdateUsuarioCommand, Result>
    {
        private readonly IUsuarioQueryRepository _queryRepository; 
        private readonly IUsuarioCommandRepository _commandRepository;
        private readonly IMapper _mapper;

        public UpdateUsuarioCommandHandler(IUsuarioQueryRepository queryRepository, IUsuarioCommandRepository commandRepository, IMapper mapper)
        {
            _queryRepository = queryRepository;
            _commandRepository = commandRepository;
            _mapper = mapper;
        }

        public async Task<Result> Handle(UpdateUsuarioCommand request, CancellationToken cancellationToken)
        {
            var usuario = await _queryRepository.GetUsuarioByIdAsync(request.Id);
            if(usuario == null) return new RegistroNaoEncontradoResult();

            usuario = _mapper.Map(request, usuario);
            await _commandRepository.UpdateUsuarioAsync(usuario);

            return new RegistroAtualizadoComSucessoResult();
        }
    }
}