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
using MediatR;

namespace FinanceGO.Application.Commands.UsuarioCommands.CreateUsuario
{
    public class CreateUsuarioCommandHandler : IRequestHandler<CreateUsuarioCommand, Result>
    {
        private readonly IUsuarioQueryRepository _queryRepository;
        private readonly IUsuarioCommandRepository _commandRepository;
        private readonly IAuthenticationService _authenticationService;
        private readonly IMapper _mapper;


        public CreateUsuarioCommandHandler(IUsuarioCommandRepository commandRepository, IAuthenticationService authenticationService, IMapper mapper, IUsuarioQueryRepository queryRepository)
        {
            _commandRepository = commandRepository;
            _authenticationService = authenticationService;
            _mapper = mapper;
            _queryRepository = queryRepository;
        }

        public async Task<Result> Handle(CreateUsuarioCommand request, CancellationToken cancellationToken)
        {
            var existeUsuarioCadastradoComMesmoEmail = await VerificarSeExisteUsuarioComMesmoEmail(request.Email);
            if(existeUsuarioCadastradoComMesmoEmail) return new RegistroDuplicadoResult();
            
            var senhaHash = _authenticationService.ComputeSha256Hash(request.Senha);

            var usuario = new Usuario(request.Nome, request.Email, senhaHash, request.DataDeNascimento);
            await _commandRepository.CreateUsuarioAsync(usuario);

            var usuarioViewModel = _mapper.Map<UsuarioViewModel>(usuario);
            return new CriadoComSucessoResult(usuarioViewModel);                        
        }

        private async Task<bool> VerificarSeExisteUsuarioComMesmoEmail(string email)
        {
            var possivelUsuarioComMesmoEmail = await _queryRepository.GetUsuarioByEmailAsync(email);
            
            if(possivelUsuarioComMesmoEmail == null) 
                return false;

            return true;
        }
    }
}