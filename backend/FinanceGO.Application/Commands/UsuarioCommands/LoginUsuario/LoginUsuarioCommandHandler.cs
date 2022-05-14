using System.Threading;
using System.Threading.Tasks;
using FinanceGO.Application.ViewModels;
using FinanceGO.Core.Authentication;
using FinanceGO.Core.Repositories.UsuarioRepositories;
using FinanceGO.Core.Results;
using MediatR;

namespace FinanceGO.Application.Commands.UsuarioCommands.LoginUsuario
{
    public class LoginUsuarioCommandHandler : IRequestHandler<LoginUsuarioCommand, Result>
    {        
        private readonly IUsuarioQueryRepository _queryRepository; 
        private readonly IAuthenticationService _authenticationService;

        public LoginUsuarioCommandHandler(IUsuarioQueryRepository queryRepository, IAuthenticationService authenticationService)
        {
            _queryRepository = queryRepository;
            _authenticationService = authenticationService;
        }

        public async Task<Result> Handle(LoginUsuarioCommand request, CancellationToken cancellationToken)
        {
            var senhaHash = _authenticationService.ComputeSha256Hash(request.Password);
            var email = request.Email;

            var usuario = await _queryRepository.GetUsuarioByEmailAndPasswordAsync(email, senhaHash);
            if(usuario == null) return new DadosInformadosNaoConferemResult();

            var token = _authenticationService.GenerateJwtToken(usuario.Id);
            var usuarioViewModel = new LoginUsuarioViewModel(usuario.Nome, token);
            return new LoginEfetuadoComSucessoResult(usuarioViewModel);
        }
    }
}