using SpendWise.Domain.Enums;

namespace SpendWise.Domain.Entities;

public class Categoria : BaseEntity
{
    public string Nome { get; private set; }
    public string? Descricao { get; private set; }
    public TipoCategoria Tipo { get; private set; }
    public Guid UsuarioId { get; private set; }
    public bool IsAtiva { get; private set; } = true;

    // Relacionamentos
    public virtual Usuario Usuario { get; private set; }
    public virtual ICollection<Transacao> Transacoes { get; private set; } = new List<Transacao>();

    // Construtor privado para EF Core
    private Categoria() { }

    public Categoria(string nome, TipoCategoria tipo, Guid usuarioId, string? descricao = null)
    {
        if (string.IsNullOrWhiteSpace(nome))
            throw new ArgumentException("Nome da categoria não pode ser vazio", nameof(nome));
        
        if (usuarioId == Guid.Empty)
            throw new ArgumentException("UsuarioId não pode ser vazio", nameof(usuarioId));

        Nome = nome;
        Tipo = tipo;
        UsuarioId = usuarioId;
        Descricao = descricao;
    }

    public void AtualizarNome(string nome)
    {
        if (string.IsNullOrWhiteSpace(nome))
            throw new ArgumentException("Nome da categoria não pode ser vazio", nameof(nome));
        
        Nome = nome;
        UpdatedAt = DateTime.UtcNow;
    }

    public void AtualizarDescricao(string? descricao)
    {
        Descricao = descricao;
        UpdatedAt = DateTime.UtcNow;
    }

    public void Desativar()
    {
        IsAtiva = false;
        UpdatedAt = DateTime.UtcNow;
    }

    public void Ativar()
    {
        IsAtiva = true;
        UpdatedAt = DateTime.UtcNow;
    }
}
