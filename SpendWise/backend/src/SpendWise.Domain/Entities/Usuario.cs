using SpendWise.Domain.ValueObjects;

namespace SpendWise.Domain.Entities;

public class Usuario : BaseEntity
{
    public string Nome { get; private set; }
    public Email Email { get; private set; }
    public string PasswordHash { get; private set; }
    public decimal RendaMensal { get; private set; }
    public bool IsAtivo { get; private set; } = true;

    // Relacionamentos
    public virtual ICollection<Categoria> Categorias { get; private set; } = new List<Categoria>();
    public virtual ICollection<Transacao> Transacoes { get; private set; } = new List<Transacao>();

    // Construtor privado para EF Core
    private Usuario() { }

    public Usuario(string nome, Email email, string passwordHash, decimal rendaMensal)
    {
        if (string.IsNullOrWhiteSpace(nome))
            throw new ArgumentException("Nome não pode ser vazio", nameof(nome));
        
        if (rendaMensal < 0)
            throw new ArgumentException("Renda mensal não pode ser negativa", nameof(rendaMensal));

        Nome = nome;
        Email = email ?? throw new ArgumentNullException(nameof(email));
        PasswordHash = passwordHash ?? throw new ArgumentNullException(nameof(passwordHash));
        RendaMensal = rendaMensal;
    }

    public void AtualizarNome(string nome)
    {
        if (string.IsNullOrWhiteSpace(nome))
            throw new ArgumentException("Nome não pode ser vazio", nameof(nome));
        
        Nome = nome;
        UpdatedAt = DateTime.UtcNow;
    }

    public void AtualizarRendaMensal(decimal rendaMensal)
    {
        if (rendaMensal < 0)
            throw new ArgumentException("Renda mensal não pode ser negativa", nameof(rendaMensal));
        
        RendaMensal = rendaMensal;
        UpdatedAt = DateTime.UtcNow;
    }

    public void Desativar()
    {
        IsAtivo = false;
        UpdatedAt = DateTime.UtcNow;
    }

    public void Ativar()
    {
        IsAtivo = true;
        UpdatedAt = DateTime.UtcNow;
    }
}
