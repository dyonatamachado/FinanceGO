using FinanceGO.Application.Commands.DespesaCommands.DeleteDespesa;
using FinanceGO.Core.Authorization;
using FinanceGO.Core.Entities;
using FinanceGO.Core.Enums;
using FinanceGO.Core.Repositories.DespesaRepositories;
using FinanceGO.Core.Results;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace FinanceGO.Tests.FinanceGO.Application.Commands.DespesaCommands.DeleteDespesa.DeleteDespesaCommandHandlerTests
{
    public class DeleteDespesaCommandHandlerHandleTests
    {
        [Fact]
        public async void RetornaRegistroNaoEncontradoResultDadoDespesaNaoEncontrada()
        {
            //ARRANGE
            var command = new DeleteDespesaCommand(1);

            var mockQueryRepo = new Mock<IDespesaQueryRepository>();
            mockQueryRepo.Setup(mqr => mqr.GetDespesaByIdAsync(command.Id)).ReturnsAsync((Despesa) null);
            var queryRepo = mockQueryRepo.Object;

            var commandRepo = new Mock<IDespesaCommandRepository>().Object;

            var requirement = new Mock<IMesmoUsuarioAuthorizationRequirement>().Object;

            var handler = new DeleteDespesaCommandHandler(commandRepo, queryRepo, requirement);

            //ACT

            var resultado = await handler.Handle(command, CancellationToken.None);

            //ASSERT

            Assert.IsType<RegistroNaoEncontradoResult>(resultado);
        }

        [Fact]
        public async void RetornaUsuarioNaoAutorizadoResultDadoUsuarioDaDespesaDiferenteDoUsuarioLogado()
        {
            //ARRANGE
            var command = new DeleteDespesaCommand(1);
            var despesa = new Despesa("Aluguel", 600, DateTime.Now, Categoria.Moradia, 2);

            var mockQueryRepo = new Mock<IDespesaQueryRepository>();
            mockQueryRepo.Setup(mqr => mqr.GetDespesaByIdAsync(command.Id)).ReturnsAsync(despesa);
            var queryRepo = mockQueryRepo.Object;

            var commandRepo = new Mock<IDespesaCommandRepository>().Object;

            var mockRequirement = new Mock<IMesmoUsuarioAuthorizationRequirement>();
            mockRequirement.Setup(mr => mr.VerificarDespesaMesmoUsuario(despesa)).Returns(false);
            var requirement = mockRequirement.Object;

            var handler = new DeleteDespesaCommandHandler(commandRepo, queryRepo, requirement);

            //ACT

            var resultado = await handler .Handle(command, CancellationToken.None);

            //ASSERT

            Assert.IsType<UsuarioNaoAutorizadoResult>(resultado);
        }

        [Fact]
        public async void RetornaDeletadoComSucessoResultDadoDespesaEncontradaEMesmoUsuario()
        {
            //ARRANGE

            var command = new DeleteDespesaCommand(1);
            var despesa = new Despesa("Aluguel", 600, DateTime.Now, Categoria.Moradia, 2);

            var mockQueryRepo = new Mock<IDespesaQueryRepository>();
            mockQueryRepo.Setup(mqr => mqr.GetDespesaByIdAsync(command.Id)).ReturnsAsync(despesa);
            var queryRepo = mockQueryRepo.Object;

            var mockCommandRepo = new Mock<IDespesaCommandRepository>();
            mockCommandRepo.Setup(mqr => mqr.DeleteDespesaAsync(despesa));
            var commandRepo = mockCommandRepo.Object;

            var mockRequirement = new Mock<IMesmoUsuarioAuthorizationRequirement>();
            mockRequirement.Setup(mr => mr.VerificarDespesaMesmoUsuario(despesa)).Returns(true);
            var requirement = mockRequirement.Object;

            var handler = new DeleteDespesaCommandHandler(commandRepo, queryRepo, requirement);

            //ACT

            var resultado = await handler.Handle(command, CancellationToken.None);

            //ASSERT
            mockCommandRepo.Verify(mcr => mcr.DeleteDespesaAsync(despesa), Times.Once);
            Assert.IsType<DeletadoComSucessoResult>(resultado);
        }

    }
}
