using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using FinanceGO.Core.Repositories.UsuarioRepositories;
using FinanceGO.Core.Results;
using FinanceGO.Core.RulesValidators;
using FinanceGO.Core.UserServices;
using MediatR;

namespace FinanceGO.Application.Commands.UsuarioCommands.UpdateUsuario
{
    public class UpdateUsuarioCommandHandler : IRequestHandler<UpdateUsuarioCommand, Result>
    {
        private readonly IUsuarioQueryRepository _queryRepository; 
        private readonly IUsuarioCommandRepository _commandRepository;
        private readonly IMapper _mapper;
        private readonly int _loggedUserId;
        private readonly IEmailDuplicadoValidator _validator;

        public UpdateUsuarioCommandHandler(IUsuarioQueryRepository queryRepository, IUsuarioCommandRepository commandRepository, IMapper mapper, ILoggedUserService usuarioService, IEmailDuplicadoValidator validator)
        {
            _queryRepository = queryRepository;
            _commandRepository = commandRepository;
            _mapper = mapper;
            _loggedUserId = usuarioService.GetUserId();
            _validator = validator;
        }

        public async Task<Result> Handle(UpdateUsuarioCommand request, CancellationToken cancellationToken)
        {
            if (request.Id != _loggedUserId) return new UsuarioNaoAutorizadoResult();

            var existeUsuarioCadastradoComMesmoEmail = await _validator.EmailIsDuplicado(request.Email);
            if (existeUsuarioCadastradoComMesmoEmail) return new RegistroDuplicadoResult();

            var usuario = await _queryRepository.GetUsuarioByIdAsync(request.Id);
            usuario = _mapper.Map(request, usuario);
            await _commandRepository.UpdateUsuarioAsync(usuario);

            return new RegistroAtualizadoComSucessoResult();
        }
    }
}