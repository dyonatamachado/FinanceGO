using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FinanceGO.Core.Entities;

namespace FinanceGO.Core.Repositories.UsuarioRepositories
{
    public interface IUsuarioQueryRepository
    {
        Task<Usuario> GetUsuarioByIdAsync(int id);
        Task<Usuario> GetUsuarioByEmailAndPasswordAsync(string email, string passwordHash);
    }
}