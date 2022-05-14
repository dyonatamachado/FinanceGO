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
        private readonly int _userId;

        public MesmoUsuarioAuthorizationRequirement(ILoggedUserService user)
        {
            _userId = user.GetUserId();
        }

        public int GetUserId()
        {
            return _userId;
        }

        public bool VerificarDespesaMesmoUsuario(Despesa despesa)
        {
            return _userId == despesa.UsuarioId;
        }

        public bool VerificarReceitaMesmoUsuario(Receita receita)
        {
            return _userId == receita.UsuarioId;
        }
    }
}