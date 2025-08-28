using Microsoft.EntityFrameworkCore;
using SpendWise.Domain.Entities;
using SpendWise.Domain.Interfaces;
using SpendWise.Infrastructure.Data;

namespace SpendWise.Infrastructure.Repositories;

public class UsuarioRepository : IUsuarioRepository
{
    private readonly ApplicationDbContext _context;

    public UsuarioRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Usuario?> GetByIdAsync(Guid id)
    {
        return await _context.Usuarios
            .Include(u => u.Categorias)
            .Include(u => u.Transacoes)
            .FirstOrDefaultAsync(u => u.Id == id);
    }

    public async Task<Usuario?> GetByEmailAsync(string email)
    {
        return await _context.Usuarios
            .FirstOrDefaultAsync(u => u.Email.Valor == email.ToLowerInvariant());
    }

    public async Task<IEnumerable<Usuario>> GetAllAsync()
    {
        return await _context.Usuarios
            .Where(u => u.IsAtivo)
            .ToListAsync();
    }

    public async Task<Usuario> AddAsync(Usuario usuario)
    {
        var result = await _context.Usuarios.AddAsync(usuario);
        return result.Entity;
    }

    public async Task UpdateAsync(Usuario usuario)
    {
        _context.Usuarios.Update(usuario);
        await Task.CompletedTask;
    }

    public async Task DeleteAsync(Guid id)
    {
        var usuario = await _context.Usuarios.FindAsync(id);
        if (usuario != null)
        {
            usuario.Desativar();
            _context.Usuarios.Update(usuario);
        }
    }

    public async Task<bool> EmailExistsAsync(string email)
    {
        return await _context.Usuarios
            .AnyAsync(u => u.Email.Valor == email.ToLowerInvariant());
    }
}
