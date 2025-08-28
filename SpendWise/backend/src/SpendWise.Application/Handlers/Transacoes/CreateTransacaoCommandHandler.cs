using AutoMapper;
using MediatR;
using SpendWise.Application.DTOs;
using SpendWise.Application.Commands.Transacoes;
using SpendWise.Domain.Entities;
using SpendWise.Domain.Interfaces;

namespace SpendWise.Application.Handlers.Transacoes;

public class CreateTransacaoCommandHandler : IRequestHandler<CreateTransacaoCommand, TransacaoDto>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public CreateTransacaoCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<TransacaoDto> Handle(CreateTransacaoCommand request, CancellationToken cancellationToken)
    {
        var transacao = new Transacao(
            request.Descricao,
            request.Valor,
            request.DataTransacao,
            request.Tipo,
            request.UsuarioId,
            request.CategoriaId,
            request.Observacoes
        );

        var result = await _unitOfWork.Transacoes.AddAsync(transacao);
        await _unitOfWork.SaveChangesAsync();

        return _mapper.Map<TransacaoDto>(result);
    }
}