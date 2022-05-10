using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FinanceGO.Core.Entities;
using FinanceGO.Core.Repositories.UsuarioRepositories;
using Microsoft.EntityFrameworkCore;

namespace FinanceGO.Infrastructure.Persistence.Repositories.UsuarioRepositories
{
    public class UsuarioCommandRepository : IUsuarioCommandRepository
    {
        private readonly FinanceGODbContext _context;

        public UsuarioCommandRepository(FinanceGODbContext context)
        {
            _context = context;
        }

        public async Task CreateUsuarioAsync(Usuario usuario)
        {
            await _context.Usuarios.AddAsync(usuario);
            await SaveChangesAsync();
        }

        public async Task DeleteUsuario(int id)
        {
            var usuarioASerRemovido = await _context.Usuarios.SingleOrDefaultAsync(u => u.Id == id);
            _context.Usuarios.Remove(usuarioASerRemovido);
            await SaveChangesAsync();
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }

        public async Task UpdatePasswordAsync(Usuario usuarioComSenhaAlterada)
        {
            var usuario = await _context.Usuarios
                .SingleOrDefaultAsync(u => u.Id == usuarioComSenhaAlterada.Id);

            usuario = usuarioComSenhaAlterada;

            await SaveChangesAsync();
        }

        public async Task UpdateUsuarioAsync(Usuario usuarioComDadosAlterados)
        {
            var usuario = await _context.Usuarios
                .SingleOrDefaultAsync(u => u.Id == usuarioComDadosAlterados.Id);

            usuario = usuarioComDadosAlterados;

            await SaveChangesAsync();
        }
    }
}