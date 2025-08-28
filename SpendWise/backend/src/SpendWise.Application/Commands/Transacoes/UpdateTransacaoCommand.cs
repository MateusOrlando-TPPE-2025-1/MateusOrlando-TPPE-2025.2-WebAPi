using MediatR;
using SpendWise.Application.DTOs;
using SpendWise.Domain.ValueObjects;

namespace SpendWise.Application.Commands.Transacoes;

public record UpdateTransacaoCommand(
    Guid Id,
    string Descricao,
    Money Valor,
    DateTime DataTransacao,
    Guid CategoriaId,
    string? Observacoes
) : IRequest<TransacaoDto?>;