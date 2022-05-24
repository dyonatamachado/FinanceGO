using System;
using System.Threading;
using System.Threading.Tasks;
using FinanceGO.Core.Authentication;
using FinanceGO.Core.Entities;
using FinanceGO.Core.Repositories.UsuarioRepositories;
using FinanceGO.Core.Results;
using FinanceGO.Core.UserServices;
using MediatR;

namespace FinanceGO.Application.Commands.UsuarioCommands.UpdateSenha
{
    public class UpdateSenhaCommandHandler : IRequestHandler<UpdateSenhaCommand, Result>
    {
        private readonly IUsuarioQueryRepository _queryRepository; 
        private readonly IUsuarioCommandRepository _commandRepository;
        private readonly IAuthenticationService _authenticationService;
        private readonly int _loggedUserId;

        public UpdateSenhaCommandHandler(IUsuarioQueryRepository queryRepository, IUsuarioCommandRepository commandRepository, IAuthenticationService authenticationService, ILoggedUserService usuarioService)
        {
            _queryRepository = queryRepository;
            _commandRepository = commandRepository;
            _authenticationService = authenticationService;
            _loggedUserId = usuarioService.GetUserId();
        }

        public async Task<Result> Handle(UpdateSenhaCommand request, CancellationToken cancellationToken)
        {
            if(request.Id != _loggedUserId) return new UsuarioNaoAutorizadoResult();

            var usuario = await _queryRepository.GetUsuarioByIdAsync(request.Id);

            var emailInformado = request.Email;
            var senhaAtualInformadaHash = _authenticationService.ComputeSha256Hash(request.SenhaAtual);

            if(usuario.Email != emailInformado || usuario.Senha != senhaAtualInformadaHash)
                return new DadosInformadosNaoConferemResult();

            var novaSenhaHash = _authenticationService.ComputeSha256Hash(request.NovaSenha);
            usuario.AlterarSenha(novaSenhaHash);

            await _commandRepository.UpdatePasswordAsync(usuario);
            return new RegistroAtualizadoComSucessoResult();
        }
    }
}