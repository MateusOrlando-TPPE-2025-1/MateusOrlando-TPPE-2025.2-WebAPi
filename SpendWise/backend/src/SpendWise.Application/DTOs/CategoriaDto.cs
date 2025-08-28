using SpendWise.Domain.Enums;

namespace SpendWise.Application.DTOs;

public class CategoriaDto
{
    public Guid Id { get; set; }
    public string Nome { get; set; } = string.Empty;
    public string? Descricao { get; set; }
    public TipoCategoria Tipo { get; set; }
    public Guid UsuarioId { get; set; }
    public bool IsAtiva { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
}

public class CreateCategoriaDto
{
    public string Nome { get; set; } = string.Empty;
    public string? Descricao { get; set; }
    public TipoCategoria Tipo { get; set; }
    public Guid UsuarioId { get; set; }
}

public class UpdateCategoriaDto
{
    public string Nome { get; set; } = string.Empty;
    public string? Descricao { get; set; }
}
