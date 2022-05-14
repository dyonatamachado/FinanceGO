using System;
using System.Threading;
using System.Threading.Tasks;
using FinanceGO.Core.Authentication;
using FinanceGO.Core.Entities;
using FinanceGO.Core.Repositories.UsuarioRepositories;
using FinanceGO.Core.Results;
using MediatR;

namespace FinanceGO.Application.Commands.UsuarioCommands.UpdateSenha
{
    public class UpdateSenhaCommandHandler : IRequestHandler<UpdateSenhaCommand, Result>
    {
        private readonly IUsuarioQueryRepository _queryRepository; 
        private readonly IUsuarioCommandRepository _commandRepository;
        private readonly IAuthenticationService _authenticationService;

        public UpdateSenhaCommandHandler(IUsuarioQueryRepository queryRepository, IUsuarioCommandRepository commandRepository, IAuthenticationService authenticationService)
        {
            _queryRepository = queryRepository;
            _commandRepository = commandRepository;
            _authenticationService = authenticationService;
        }

        public async Task<Result> Handle(UpdateSenhaCommand request, CancellationToken cancellationToken)
        {
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