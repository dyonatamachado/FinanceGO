using AutoMapper;
using FinanceGO.Application.Commands.DespesaCommands.UpdateDespesa;
using FinanceGO.Application.InputModels.DespesaInputModels;
using FinanceGO.Application.Validators.IRulesValidators;
using FinanceGO.Core.Authorization;
using FinanceGO.Core.Entities;
using FinanceGO.Core.Enums;
using FinanceGO.Core.Repositories.DespesaRepositories;
using FinanceGO.Core.Results;
using Moq;
using System;
using System.Threading;
using Xunit;

namespace FinanceGO.Tests.FinanceGO.Application.Commands.DespesaCommands.UpdateDespesa.UpdateDespesaCommandHandlerTests
{
    public class UpdateDespesaCommandHandlerHandleTests
    {
        [Fact]
        public async void RetornaRegistroNaoEncontradoResultDadoDespesaNaoEncontrada()
        {
            //ARRANGE
            var inputModel = new UpdateDespesaInputModel("Aluguel", 700, DateTime.Now, Categoria.Moradia);
            var command = new global::FinanceGO.Application.Commands.DespesaCommands.UpdateDespesa.UpdateDespesaCommand(inputModel, 1);

            var mockQueryRepo = new Mock<IDespesaQueryRepository>();
            mockQueryRepo.Setup(mqr => mqr.GetDespesaByIdAsync(command.Id)).ReturnsAsync((Despesa) null);
            var queryRepo = mockQueryRepo.Object;

            var mockCommandRepo = new Mock<IDespesaCommandRepository>();
            var commandRepo = mockCommandRepo.Object;

            var mockRequirement = new Mock<IMesmoUsuarioAuthorizationRequirement>();
            var requirement = mockRequirement.Object;

            var mockMapper = new Mock<IMapper>();
            var mapper = mockMapper.Object;

            var mockValidator = new Mock<IDespesaDuplicadaValidator>();
            var validator = mockValidator.Object;

            var handler = new UpdateDespesaCommandHandler(queryRepo, commandRepo, mapper, requirement, validator);

            //ACT

            var resultado = await handler.Handle(command, CancellationToken.None);

            //ASSERT

            Assert.IsType<RegistroNaoEncontradoResult>(resultado);
        }

        [Fact]
        public async void RetornaUsuarioNaoAutorizadoDadoUsuarioDaDespesaDiferenteDoUsuarioLogado()
        {
            //ARRANGE

            var inputModel = new UpdateDespesaInputModel("Aluguel", 700, DateTime.Now, Categoria.Moradia);
            var command = new global::FinanceGO.Application.Commands.DespesaCommands.UpdateDespesa.UpdateDespesaCommand(inputModel, 1);
            var despesa = new Despesa("Aluguel", 650, DateTime.Now, Categoria.Moradia, 2);

            var mockQueryRepo = new Mock<IDespesaQueryRepository>();
            mockQueryRepo.Setup(mqr => mqr.GetDespesaByIdAsync(command.Id)).ReturnsAsync(despesa);
            var queryRepo = mockQueryRepo.Object;

            var mockCommandRepo = new Mock<IDespesaCommandRepository>();
            var commandRepo = mockCommandRepo.Object;

            var mockRequirement = new Mock<IMesmoUsuarioAuthorizationRequirement>();
            mockRequirement.Setup(mr => mr.VerificarDespesaMesmoUsuario(despesa)).Returns(false);
            var requirement = mockRequirement.Object;

            var mockMapper = new Mock<IMapper>();
            var mapper = mockMapper.Object;

            var mockValidator = new Mock<IDespesaDuplicadaValidator>();
            var validator = mockValidator.Object;

            var handler = new UpdateDespesaCommandHandler(queryRepo, commandRepo, mapper, requirement, validator);

            //ACT

            var resultado = await handler.Handle(command, CancellationToken.None);

            //ASSERT

            Assert.IsType<UsuarioNaoAutorizadoResult>(resultado);

        }

        [Fact]
        public async void RetornaRegistroDuplicadoResultDadoExisteOutraDespesaCadastradaComMesmaDescricaoParaOMesmoMesMesmoUsuario()
        {
            //ARRANGE

            var inputModel = new UpdateDespesaInputModel("Aluguel", 700, DateTime.Now, Categoria.Moradia);
            var command = new global::FinanceGO.Application.Commands.DespesaCommands.UpdateDespesa.UpdateDespesaCommand(inputModel, 1);
            var despesa = new Despesa("Aluguel", 650, DateTime.Now, Categoria.Moradia, 1);

            var mockQueryRepo = new Mock<IDespesaQueryRepository>();
            mockQueryRepo.Setup(mqr => mqr.GetDespesaByIdAsync(command.Id)).ReturnsAsync(despesa);
            var queryRepo = mockQueryRepo.Object;

            var mockCommandRepo = new Mock<IDespesaCommandRepository>();
            var commandRepo = mockCommandRepo.Object;

            var mockRequirement = new Mock<IMesmoUsuarioAuthorizationRequirement>();
            mockRequirement.Setup(mr => mr.VerificarDespesaMesmoUsuario(despesa)).Returns(true);
            var requirement = mockRequirement.Object;

            var mockMapper = new Mock<IMapper>();
            var mapper = mockMapper.Object;

            var mockValidator = new Mock<IDespesaDuplicadaValidator>();
            mockValidator.Setup(mv => mv.DespesaIsDuplicada(command, 1)).ReturnsAsync(true);
            var validator = mockValidator.Object;

            var handler = new UpdateDespesaCommandHandler(queryRepo, commandRepo, mapper, requirement, validator);

            //ACT

            var resultado = await handler.Handle(command, CancellationToken.None);

            //ASSERT

            Assert.IsType<RegistroDuplicadoResult>(resultado);

        }

        [Fact]
        public async void RetornaRegistroAtualizadoComSucessoResultDadoDespesaEncontradaUsuarioAutorizadoEDespesaNaoDuplicada() 
        {
            //ARRANGE

            var inputModel = new UpdateDespesaInputModel("Aluguel", 700, DateTime.Now, Categoria.Moradia);
            var command = new global::FinanceGO.Application.Commands.DespesaCommands.UpdateDespesa.UpdateDespesaCommand(inputModel, 1);
            var despesa = new Despesa("Aluguel", 650, DateTime.Now, Categoria.Moradia, 1);
            var despesaComDadosAlterados = new Despesa("Aluguel", 700, DateTime.Now, Categoria.Moradia, 1);

            var mockQueryRepo = new Mock<IDespesaQueryRepository>();
            mockQueryRepo.Setup(mqr => mqr.GetDespesaByIdAsync(command.Id)).ReturnsAsync(despesa);
            var queryRepo = mockQueryRepo.Object;

            var mockMapper = new Mock<IMapper>();
            mockMapper.Setup(mm => mm.Map(command, despesa)).Returns(despesaComDadosAlterados);
            var mapper = mockMapper.Object;

            var mockCommandRepo = new Mock<IDespesaCommandRepository>();
            mockCommandRepo.Setup(mcr => mcr.UpdateDespesaAsync(despesaComDadosAlterados));
            var commandRepo = mockCommandRepo.Object;

            var mockRequirement = new Mock<IMesmoUsuarioAuthorizationRequirement>();
            mockRequirement.Setup(mr => mr.VerificarDespesaMesmoUsuario(despesa)).Returns(true);
            var requirement = mockRequirement.Object;

            var mockValidator = new Mock<IDespesaDuplicadaValidator>();
            mockValidator.Setup(mv => mv.DespesaIsDuplicada(command, 1)).ReturnsAsync(false);
            var validator = mockValidator.Object;

            var handler = new UpdateDespesaCommandHandler(queryRepo, commandRepo, mapper, requirement, validator);

            //ACT

            var resultado = await handler.Handle(command, CancellationToken.None);

            //ASSERT
            mockCommandRepo.Verify(mcr => mcr.UpdateDespesaAsync((despesaComDadosAlterados)), Times.Once);
            Assert.IsType<RegistroAtualizadoComSucessoResult>(resultado);
        }
    }
}
