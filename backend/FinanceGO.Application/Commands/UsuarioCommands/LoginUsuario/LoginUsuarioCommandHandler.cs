using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using FinanceGO.Application.ViewModels;
using FinanceGO.Core.AuthServices;
using FinanceGO.Core.Repositories.UsuarioRepositories;
using MediatR;

namespace FinanceGO.Application.Commands.UsuarioCommands.LoginUsuario
{
    public class LoginUsuarioCommandHandler : IRequestHandler<LoginUsuarioCommand, LoginUsuarioViewModel>
    {        
        private readonly IUsuarioQueryRepository _queryRepository; 
        private readonly IAuthenticationService _authenticationService;

        public LoginUsuarioCommandHandler(IUsuarioQueryRepository queryRepository, IAuthenticationService authenticationService)
        {
            _queryRepository = queryRepository;
            _authenticationService = authenticationService;
        }

        public async Task<LoginUsuarioViewModel> Handle(LoginUsuarioCommand request, CancellationToken cancellationToken)
        {
            var senhaHash = _authenticationService.ComputeSha256Hash(request.Password);

            var usuario = await _queryRepository.GetUsuarioByEmailAndPasswordAsync(request.Email, senhaHash);
            
            if(usuario == null) return null;

            var token = _authenticationService.GenerateJwtToken(usuario.Id);

            return new LoginUsuarioViewModel(request.Email, token);
        }
    }
}