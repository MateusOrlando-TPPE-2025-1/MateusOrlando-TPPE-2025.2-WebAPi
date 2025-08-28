using SpendWise.Domain.Entities;
using SpendWise.Domain.Enums;
using SpendWise.Domain.ValueObjects;

namespace SpendWise.Domain.Interfaces;

public interface ITransacaoRepository
{
    Task<Transacao?> GetByIdAsync(Guid id);
    Task<IEnumerable<Transacao>> GetAllAsync();
    Task<IEnumerable<Transacao>> GetByUsuarioIdAsync(Guid usuarioId);
    Task<IEnumerable<Transacao>> GetByPeriodoAsync(Guid usuarioId, Periodo periodo);
    Task<IEnumerable<Transacao>> GetByTipoAsync(Guid usuarioId, TipoTransacao tipo);
    Task<IEnumerable<Transacao>> GetByCategoriaAsync(Guid categoriaId);
    Task<Transacao> AddAsync(Transacao transacao);
    Task UpdateAsync(Transacao transacao);
    Task DeleteAsync(Guid id);
    Task<decimal> GetTotalByTipoAsync(Guid usuarioId, TipoTransacao tipo, Periodo periodo);
    Task<decimal> GetSaldoAsync(Guid usuarioId, Periodo periodo);
}
