using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FinanceGO.Core.Entities;

namespace FinanceGO.Core.Repositories.UsuarioRepositories
{
    public interface IUsuarioCommandRepository
    {
        Task UpdatePasswordAsync(Usuario usuarioComSenhaAlterada);
        Task UpdateUsuarioAsync(Usuario usuarioComDadosAlterados);
        Task CreateUsuarioAsync(Usuario usuario);
        Task SaveChangesAsync();
        Task DeleteUsuario(int id);
    }
}