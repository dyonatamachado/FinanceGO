using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using FinanceGO.Application.ViewModels;
using FinanceGO.Core.AuthServices;
using FinanceGO.Core.Entities;
using FinanceGO.Core.Repositories.UsuarioRepositories;
using FinanceGO.Core.Results;
using MediatR;

namespace FinanceGO.Application.Commands.UsuarioCommands.CreateUsuario
{
    public class CreateUsuarioCommandHandler : IRequestHandler<CreateUsuarioCommand, UsuarioViewModel>
    {
        private readonly IUsuarioCommandRepository _commandRepository;
        private readonly IAuthenticationService _authenticationService;
        private readonly IMapper _mapper;


        public CreateUsuarioCommandHandler(IUsuarioCommandRepository commandRepository, IAuthenticationService authenticationService, IMapper mapper)
        {
            _commandRepository = commandRepository;
            _authenticationService = authenticationService;
            _mapper = mapper;
        }

        public async Task<UsuarioViewModel> Handle(CreateUsuarioCommand request, CancellationToken cancellationToken)
        {
            var senhaHash = _authenticationService.ComputeSha256Hash(request.Senha);

            var usuario = new Usuario(request.Nome, request.Email, senhaHash, request.DataDeNascimento);
            await _commandRepository.CreateUsuarioAsync(usuario);

            return _mapper.Map<UsuarioViewModel>(usuario);                        
        }
    }
}