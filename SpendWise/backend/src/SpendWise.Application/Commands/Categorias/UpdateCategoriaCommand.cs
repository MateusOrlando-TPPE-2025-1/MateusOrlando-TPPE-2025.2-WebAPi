using MediatR;
using SpendWise.Application.DTOs;
using SpendWise.Domain.Enums;

namespace SpendWise.Application.Commands.Categorias;

public record UpdateCategoriaCommand(
    Guid Id,
    string Nome,
    string? Descricao,
    TipoCategoria Tipo,
    bool IsAtiva
) : IRequest<CategoriaDto?>;