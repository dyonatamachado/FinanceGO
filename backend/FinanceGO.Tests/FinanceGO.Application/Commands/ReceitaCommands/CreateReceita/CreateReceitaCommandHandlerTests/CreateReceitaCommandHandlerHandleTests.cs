using AutoMapper;
using FinanceGO.Application.Commands.ReceitaCommands.CreateReceita;
using FinanceGO.Application.InputModels.ReceitaInputModels;
using FinanceGO.Application.ViewModels;
using FinanceGO.Core.Entities;
using FinanceGO.Core.Enums;
using FinanceGO.Core.Repositories.ReceitaRepositories;
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

namespace FinanceGO.Tests.FinanceGO.Application.Commands.ReceitaCommands.CreateReceita.CreateReceitaCommandHandlerTests
{
    public class CreateReceitaCommandHandlerHandleTests
    {
        [Fact]
        public async void RetornaRegistroDuplicadoResultDadoReceitaDuplicada()
        {
            //ARRANGE
            var inputModel = new CreateReceitaInputModel("Salário", 1500, DateTime.Now.AddDays(-3));
            var request = new CreateReceitaCommand(inputModel, 1);

            var mockValidator = new Mock<IReceitaDuplicadaValidator>();
            mockValidator.Setup(mv => mv.ReceitaIsDuplicada(request.Data, request.Descricao, request.UsuarioId)).ReturnsAsync(true);
            var validator = mockValidator.Object;

            var mapper = new Mock<IMapper>().Object;
            var repository = new Mock<IReceitaCommandRepository>().Object;

            var handler = new CreateReceitaCommandHandler(repository, mapper, validator);

            //ACT

            var resultado = await handler.Handle(request, CancellationToken.None);

            //ASSERT
            Assert.IsType<RegistroDuplicadoResult>(resultado);
        }

        [Fact]
        public async void RetornaCriadoComSucessoResultDadoDespesaNaoDuplicada()
        {
            //ARRANGE
            var inputModel = new CreateReceitaInputModel("Salário", 1500, DateTime.Now.AddDays(-3));
            var request = new CreateReceitaCommand(inputModel, 1);
            var receita = new Receita(request.Descricao, request.Valor, request.Data, request.UsuarioId);
            var receitaViewModel = new ReceitaViewModel(1, receita.Descricao, receita.Valor, receita.Data);

            var mockValidator = new Mock<IReceitaDuplicadaValidator>();
            mockValidator.Setup(mv => mv.ReceitaIsDuplicada(request.Data, request.Descricao, request.UsuarioId)).ReturnsAsync(false);
            var validator = mockValidator.Object;

            var mockMapper = new Mock<IMapper>();
            mockMapper.Setup(mm => mm.Map<Receita>(request)).Returns(receita);
            mockMapper.Setup(mm => mm.Map<ReceitaViewModel>(receita)).Returns(receitaViewModel);
            var mapper = mockMapper.Object;

            var mockRepository = new Mock<IReceitaCommandRepository>();
            mockRepository.Setup(mr => mr.CreateReceitaAsync(receita));
            var repository = mockRepository.Object;

            var handler = new CreateReceitaCommandHandler(repository, mapper, validator);

            //ACT

            var resultado = await handler.Handle(request, CancellationToken.None);

            //ASSERT
            mockRepository.Verify(mr => mr.CreateReceitaAsync(receita), Times.Once);
            var criadoComSucessoResult = Assert.IsType<CriadoComSucessoResult>(resultado);
            Assert.IsType<ReceitaViewModel>(criadoComSucessoResult.Value);
            Assert.Equal(criadoComSucessoResult.Value, receitaViewModel);
        }
    }
}
