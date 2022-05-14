using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using FinanceGO.Core.Authentication;
using FinanceGO.Core.Authorization;
using FinanceGO.Core.Entities;
using FinanceGO.Core.Repositories.UsuarioRepositories;
using FinanceGO.Core.Results;
using FinanceGO.Core.UserServices;
using MediatR;

namespace FinanceGO.Application.Commands.UsuarioCommands.DeleteUsuario
{
    public class DeleteUsuarioCommandHandler : IRequestHandler<DeleteUsuarioCommand, Result> 
    {
        private readonly IUsuarioQueryRepository _queryRepository; 
        private readonly IUsuarioCommandRepository _commandRepository;
        private readonly IAuthenticationService _authenticationService;
        private readonly int _loggedUserId;

        public DeleteUsuarioCommandHandler(IUsuarioQueryRepository queryRepository, IUsuarioCommandRepository commandRepository, IAuthenticationService authenticationService, ILoggedUserService usuarioService)
        {
            _queryRepository = queryRepository;
            _commandRepository = commandRepository;
            _authenticationService = authenticationService;
            _loggedUserId = usuarioService.GetUserId();
        }

        public async Task<Result> Handle(DeleteUsuarioCommand request, CancellationToken cancellationToken)
        {
            if(request.Id != _loggedUserId)
                return new UsuarioNaoAutorizadoResult();

            var usuario = await _queryRepository.GetUsuarioByIdAsync(request.Id);

            var emailInformado = request.Email;
            var senhaInformadaHash = _authenticationService.ComputeSha256Hash(request.Password);

            if(usuario.Email != emailInformado || usuario.Senha != senhaInformadaHash) 
                return new DadosInformadosNaoConferemResult();
        
            await _commandRepository.DeleteUsuario(request.Id);
            return new DeletadoComSucessoResult();            
        }
    }
}