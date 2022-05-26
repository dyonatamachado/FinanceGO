using AutoMapper;
using FinanceGO.Application.Commands.DespesaCommands.CreateDespesa;
using FinanceGO.Application.InputModels.DespesaInputModels;
using FinanceGO.Application.Validators.IRulesValidators;
using FinanceGO.Application.ViewModels;
using FinanceGO.Core.Entities;
using FinanceGO.Core.Enums;
using FinanceGO.Core.Repositories.DespesaRepositories;
using FinanceGO.Core.Results;
using FinanceGO.Core.RulesValidators;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace FinanceGO.Tests.FinanceGO.Application.Commands.DespesaCommands.CreateDespesa.CreateDespesaCommandHandlerTests
{
    public class CreateDespesaCommandHandlerHandleTest
    {
        [Fact]
        public async void RetornaRegistroDuplicadoResultDadoDespesaDuplicada()
        {
            //ARRANGE
            var inputModel = new CreateDespesaInputModel("Aluguel", 600, DateTime.Now, Categoria.Moradia);
            var request = new CreateDespesaCommand(inputModel, 1);

            var mockValidator = new Mock<IDespesaDuplicadaValidator>();
            mockValidator.Setup(mv => mv.DespesaIsDuplicada(request)).ReturnsAsync(true);
            var validator = mockValidator.Object;

            var mapper = new Mock<IMapper>().Object;
            var repository = new Mock<IDespesaCommandRepository>().Object;

            var handler = new CreateDespesaCommandHandler(repository, mapper, validator);

            //ACT

            var resultado = await handler.Handle(request, CancellationToken.None);

            //ASSERT
            Assert.IsType<RegistroDuplicadoResult>(resultado);
        }

        [Fact]
        public async void RetornaCriadoComSucessoResultDadoDespesaNaoDuplicada()
        {
            //ARRANGE
            var inputModel = new CreateDespesaInputModel("Aluguel", 600, DateTime.Now, Categoria.Moradia);
            var request = new CreateDespesaCommand(inputModel, 1);
            var despesa = new Despesa(request.Descricao, request.Valor, request.Data, request.Categoria, request.UsuarioId);
            var despesaViewModel = new DespesaViewModel(1, despesa.Descricao, despesa.Valor, despesa.Data, despesa.Categoria);

            var mockValidator = new Mock<IDespesaDuplicadaValidator>();
            mockValidator.Setup(mv => mv.DespesaIsDuplicada(request)).ReturnsAsync(false);
            var validator = mockValidator.Object;

            var mockMapper = new Mock<IMapper>();
            mockMapper.Setup(mm => mm.Map<Despesa>(request)).Returns(despesa);
            mockMapper.Setup(mm => mm.Map<DespesaViewModel>(despesa)).Returns(despesaViewModel);
            var mapper = mockMapper.Object;

            var mockRepository = new Mock<IDespesaCommandRepository>();
            mockRepository.Setup(mr => mr.CreateDespesaAsync(despesa));
            var repository = mockRepository.Object;

            var handler = new CreateDespesaCommandHandler(repository, mapper, validator);

            //ACT

            var resultado = await handler.Handle(request, CancellationToken.None);

            //ASSERT
            mockRepository.Verify(mr => mr.CreateDespesaAsync(despesa), Times.Once);
            var criadoComSucessoResult = Assert.IsType<CriadoComSucessoResult>(resultado);
            Assert.IsType<DespesaViewModel>(criadoComSucessoResult.Value);
            Assert.Equal(criadoComSucessoResult.Value, despesaViewModel);
        }
    }
}
