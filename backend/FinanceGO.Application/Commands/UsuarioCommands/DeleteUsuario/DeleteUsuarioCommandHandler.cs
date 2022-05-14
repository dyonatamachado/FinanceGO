using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using FinanceGO.Core.Authentication;
using FinanceGO.Core.Entities;
using FinanceGO.Core.Repositories.UsuarioRepositories;
using FinanceGO.Core.Results;
using MediatR;

namespace FinanceGO.Application.Commands.UsuarioCommands.DeleteUsuario
{
    public class DeleteUsuarioCommandHandler : IRequestHandler<DeleteUsuarioCommand, Result> 
    {
        private readonly IUsuarioQueryRepository _queryRepository; 
        private readonly IUsuarioCommandRepository _commandRepository;
        private readonly IAuthenticationService _authenticationService;

        public DeleteUsuarioCommandHandler(IUsuarioQueryRepository queryRepository, IUsuarioCommandRepository commandRepository, IAuthenticationService authenticationService)
        {
            _queryRepository = queryRepository;
            _commandRepository = commandRepository;
            _authenticationService = authenticationService;
        }

        public async Task<Result> Handle(DeleteUsuarioCommand request, CancellationToken cancellationToken)
        {
            var usuario = await _queryRepository.GetUsuarioByIdAsync(request.Id);
            if(usuario == null) return new RegistroNaoEncontradoResult();

            var dadosInformadosConferem = VerificarDadosInformados(request, usuario);
    
            if(!dadosInformadosConferem) return new DadosInformadosNaoConferemResult();
        
            await _commandRepository.DeleteUsuario(request.Id);
            return new DeletadoComSucessoResult();            
        }

        private Task VerificarSeUsuarioAutorizado(DeleteUsuarioCommand request)
        {
            throw new NotImplementedException();
        }

        private bool VerificarDadosInformados(DeleteUsuarioCommand request, Usuario usuario)
        {
            var senhaInformadaHash = _authenticationService.ComputeSha256Hash(request.Password);
            var emailInformado = request.Email;
            var idInformadoNaRequisicao = request.Id;
            
            return senhaInformadaHash == usuario.Senha && 
                emailInformado == usuario.Email && 
                idInformadoNaRequisicao == usuario.Id;
        }
    }
}