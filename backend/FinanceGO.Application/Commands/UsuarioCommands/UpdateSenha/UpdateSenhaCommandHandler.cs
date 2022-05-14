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
            if(usuario == null) return new RegistroNaoEncontradoResult();

            var dadosInformadosConferem = VerificarDadosInformados(request, usuario);

            if(!dadosInformadosConferem) return new DadosInformadosNaoConferemResult();
            // else if(!usuarioAutorizado) return new UsuarioNaoAutorizadoResult();

            var novaSenhaHash = _authenticationService.ComputeSha256Hash(request.NovaSenha);
            usuario.AlterarSenha(novaSenhaHash);
            await _commandRepository.UpdatePasswordAsync(usuario);
            return new RegistroAtualizadoComSucessoResult();
        }

        private Task VerificarSeUsuarioAutorizado(UpdateSenhaCommand request)
        {
            throw new NotImplementedException();
        }

        private bool VerificarDadosInformados(UpdateSenhaCommand request, Usuario usuario)
        {
            var senhaAtualInformadaHash = _authenticationService.ComputeSha256Hash(request.SenhaAtual);
            var emailInformado = request.Email;
            var idInformadoNaRequisicao = request.Id;

            return senhaAtualInformadaHash == usuario.Senha && 
                emailInformado == usuario.Email && 
                idInformadoNaRequisicao == usuario.Id;
        }
    }
}