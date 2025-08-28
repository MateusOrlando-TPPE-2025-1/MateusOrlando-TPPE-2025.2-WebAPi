using MediatR;
using SpendWise.Application.Commands.Transacoes;
using SpendWise.Domain.Interfaces;

namespace SpendWise.Application.Handlers.Transacoes;

public class DeleteTransacaoCommandHandler : IRequestHandler<DeleteTransacaoCommand, bool>
{
    private readonly IUnitOfWork _unitOfWork;

    public DeleteTransacaoCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<bool> Handle(DeleteTransacaoCommand request, CancellationToken cancellationToken)
    {
        var transacao = await _unitOfWork.Transacoes.GetByIdAsync(request.Id);
        
        if (transacao == null)
            return false;

        await _unitOfWork.Transacoes.DeleteAsync(request.Id);
        await _unitOfWork.SaveChangesAsync();

        return true;
    }
}