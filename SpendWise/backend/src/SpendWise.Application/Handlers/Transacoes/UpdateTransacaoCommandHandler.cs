using AutoMapper;
using MediatR;
using SpendWise.Application.DTOs;
using SpendWise.Application.Commands.Transacoes;
using SpendWise.Domain.Interfaces;

namespace SpendWise.Application.Handlers.Transacoes;

public class UpdateTransacaoCommandHandler : IRequestHandler<UpdateTransacaoCommand, TransacaoDto?>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public UpdateTransacaoCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<TransacaoDto?> Handle(UpdateTransacaoCommand request, CancellationToken cancellationToken)
    {
        var transacao = await _unitOfWork.Transacoes.GetByIdAsync(request.Id);
        
        if (transacao == null)
            return null;

        // Usar métodos da entidade para atualizar
        transacao.AtualizarDescricao(request.Descricao);
        transacao.AtualizarValor(request.Valor);
        transacao.AtualizarDataTransacao(request.DataTransacao);
        transacao.AtualizarCategoria(request.CategoriaId);
        transacao.AtualizarObservacoes(request.Observacoes);

        await _unitOfWork.Transacoes.UpdateAsync(transacao);
        await _unitOfWork.SaveChangesAsync();

        return _mapper.Map<TransacaoDto>(transacao);
    }
}