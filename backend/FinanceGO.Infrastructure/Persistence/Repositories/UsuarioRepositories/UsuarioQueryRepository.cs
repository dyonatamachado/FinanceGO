using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FinanceGO.Core.Entities;
using FinanceGO.Core.Repositories.UsuarioRepositories;
using Microsoft.EntityFrameworkCore;

namespace FinanceGO.Infrastructure.Persistence.Repositories.UsuarioRepositories
{
    public class UsuarioQueryRepository : IUsuarioQueryRepository
    {
        private readonly FinanceGODbContext _context;

        public UsuarioQueryRepository(FinanceGODbContext context)
        {
            _context = context;
        }

        public async Task<Usuario> GetUsuarioByEmailAndPasswordAsync(string email, string passwordHash)
        {
            return await _context.Usuarios.SingleOrDefaultAsync(u => u.Email == email && u.Senha == passwordHash);
        }

        public async Task<Usuario> GetUsuarioByEmailAsync(string email)
        {
            return await _context.Usuarios.SingleOrDefaultAsync(u => u.Email == email);
        }

        public async Task<Usuario> GetUsuarioByIdAsync(int id)
        {
            return await _context.Usuarios.SingleOrDefaultAsync(u => u.Id == id);
        }
    }
}