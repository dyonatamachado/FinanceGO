using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using FinanceGO.Application.ViewModels;
using FinanceGO.Core.Authentication;
using FinanceGO.Core.Entities;
using FinanceGO.Core.Repositories.UsuarioRepositories;
using FinanceGO.Core.Results;
using FinanceGO.Core.RulesValidators;
using MediatR;

namespace FinanceGO.Application.Commands.UsuarioCommands.CreateUsuario
{
    public class CreateUsuarioCommandHandler : IRequestHandler<CreateUsuarioCommand, Result>
    {
        private readonly IUsuarioCommandRepository _commandRepository;
        private readonly IAuthenticationService _authenticationService;
        private readonly IMapper _mapper;
        private readonly IEmailDuplicadoValidator _validator;

        public CreateUsuarioCommandHandler(IUsuarioCommandRepository commandRepository, IAuthenticationService authenticationService, IMapper mapper, IEmailDuplicadoValidator validator)
        {
            _commandRepository = commandRepository;
            _authenticationService = authenticationService;
            _mapper = mapper;
            _validator = validator;
        }

        public async Task<Result> Handle(CreateUsuarioCommand request, CancellationToken cancellationToken)
        {
            var existeUsuarioCadastradoComMesmoEmail = await _validator.EmailIsDuplicado(request.Email);
            if(existeUsuarioCadastradoComMesmoEmail) return new RegistroDuplicadoResult();
            
            var senhaHash = _authenticationService.ComputeSha256Hash(request.Senha);

            var usuario = new Usuario(request.Nome, request.Email, senhaHash, request.DataDeNascimento);
            await _commandRepository.CreateUsuarioAsync(usuario);

            var usuarioViewModel = _mapper.Map<UsuarioViewModel>(usuario);
            return new CriadoComSucessoResult(usuarioViewModel);                        
        }
    }
}