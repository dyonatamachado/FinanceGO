using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FinanceGO.Core.Entities;

namespace FinanceGO.Core.Authorization
{
    public interface IMesmoUsuarioAuthorizationRequirement
    {
        bool VerificarDespesaMesmoUsuario(Despesa despesa);
        bool VerificarReceitaMesmoUsuario(Receita receita);
    }
}