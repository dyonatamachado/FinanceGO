using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FinanceGO.Application.Commands.DespesaCommands.CreateDespesa;
using FinanceGO.Core.Enums;
using FinanceGO.Core.UserServices;
using Moq;
using Xunit;

namespace FinanceGO.Tests.FinanceGO.Application.Commands.DespesaCommands.CreateDespesa.CreateDespesaCommandTests
{
    public class CreateDespesaCommandConstructorTests
    {
        [Fact]
        public void RetornaCreateDespesaCommandDadoTodosOsDadosInformados()
        {
            //ARRANGE
            var mock = new Mock<ILoggedUserService>();
            mock.Setup(m => m.GetUserId()).Returns(1);
            var loggedUser = mock.Object;

            //ACT

            var command = new CreateDespesaCommand("Aluguel", 600, DateTime.Now, loggedUser, (Categoria) 3);

            //ASSERT

            Assert.IsType<CreateDespesaCommand>(command);
            Assert.Equal(command.UsuarioId, loggedUser.GetUserId());
        }
    }
}