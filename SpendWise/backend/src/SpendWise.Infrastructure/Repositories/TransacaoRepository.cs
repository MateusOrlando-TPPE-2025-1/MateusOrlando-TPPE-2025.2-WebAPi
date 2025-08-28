using Microsoft.EntityFrameworkCore;
using SpendWise.Domain.Entities;
using SpendWise.Domain.Enums;
using SpendWise.Domain.Interfaces;
using SpendWise.Domain.ValueObjects;
using SpendWise.Infrastructure.Data;

namespace SpendWise.Infrastructure.Repositories;

public class TransacaoRepository : ITransacaoRepository
{
    private readonly ApplicationDbContext _context;

    public TransacaoRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Transacao?> GetByIdAsync(Guid id)
    {
        return await _context.Transacoes
            .Include(t => t.Usuario)
            .Include(t => t.Categoria)
            .FirstOrDefaultAsync(t => t.Id == id);
    }

    public async Task<IEnumerable<Transacao>> GetAllAsync()
    {
        return await _context.Transacoes
            .Include(t => t.Usuario)
            .Include(t => t.Categoria)
            .OrderByDescending(t => t.DataTransacao)
            .ToListAsync();
    }

    public async Task<IEnumerable<Transacao>> GetByUsuarioIdAsync(Guid usuarioId)
    {
        return await _context.Transacoes
            .Include(t => t.Categoria)
            .Where(t => t.UsuarioId == usuarioId)
            .OrderByDescending(t => t.DataTransacao)
            .ToListAsync();
    }

    public async Task<IEnumerable<Transacao>> GetByPeriodoAsync(Guid usuarioId, Periodo periodo)
    {
        return await _context.Transacoes
            .Include(t => t.Categoria)
            .Where(t => t.UsuarioId == usuarioId 
                && t.DataTransacao.Date >= periodo.DataInicio 
                && t.DataTransacao.Date <= periodo.DataFim)
            .OrderByDescending(t => t.DataTransacao)
            .ToListAsync();
    }

    public async Task<IEnumerable<Transacao>> GetByTipoAsync(Guid usuarioId, TipoTransacao tipo)
    {
        return await _context.Transacoes
            .Include(t => t.Categoria)
            .Where(t => t.UsuarioId == usuarioId && t.Tipo == tipo)
            .OrderByDescending(t => t.DataTransacao)
            .ToListAsync();
    }

    public async Task<IEnumerable<Transacao>> GetByCategoriaAsync(Guid categoriaId)
    {
        return await _context.Transacoes
            .Include(t => t.Usuario)
            .Include(t => t.Categoria)
            .Where(t => t.CategoriaId == categoriaId)
            .OrderByDescending(t => t.DataTransacao)
            .ToListAsync();
    }

    public async Task<Transacao> AddAsync(Transacao transacao)
    {
        var result = await _context.Transacoes.AddAsync(transacao);
        return result.Entity;
    }

    public async Task UpdateAsync(Transacao transacao)
    {
        _context.Transacoes.Update(transacao);
        await Task.CompletedTask;
    }

    public async Task DeleteAsync(Guid id)
    {
        var transacao = await _context.Transacoes.FindAsync(id);
        if (transacao != null)
        {
            _context.Transacoes.Remove(transacao);
        }
    }

    public async Task<decimal> GetTotalByTipoAsync(Guid usuarioId, TipoTransacao tipo, Periodo periodo)
    {
        return await _context.Transacoes
            .Where(t => t.UsuarioId == usuarioId 
                && t.Tipo == tipo
                && t.DataTransacao.Date >= periodo.DataInicio 
                && t.DataTransacao.Date <= periodo.DataFim)
            .SumAsync(t => t.Valor.Valor);
    }

    public async Task<decimal> GetSaldoAsync(Guid usuarioId, Periodo periodo)
    {
        var receitas = await GetTotalByTipoAsync(usuarioId, TipoTransacao.Receita, periodo);
        var despesas = await GetTotalByTipoAsync(usuarioId, TipoTransacao.Despesa, periodo);
        
        return receitas - despesas;
    }
}
