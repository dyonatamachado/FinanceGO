using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FinanceGO.Core.Authorization;
using FinanceGO.Core.Entities;
using FinanceGO.Core.UserServices;

namespace FinanceGO.Infrastructure.Authorization
{
    public class MesmoUsuarioAuthorizationRequirement : IMesmoUsuarioAuthorizationRequirement
    {
        private readonly ILoggedUserService _user;

        public MesmoUsuarioAuthorizationRequirement(ILoggedUserService user)
        {
            _user = user;
        }

        public bool VerificarDespesaMesmoUsuario(Despesa despesa)
        {
            return _user.GetUserId() == despesa.UsuarioId;
        }

        public bool VerificarReceitaMesmoUsuario(Receita receita)
        {
            return _user.GetUserId() == receita.UsuarioId;
        }
    }
}