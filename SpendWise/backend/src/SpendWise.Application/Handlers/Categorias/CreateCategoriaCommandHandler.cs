using AutoMapper;
using MediatR;
using SpendWise.Application.DTOs;
using SpendWise.Application.Commands.Categorias;
using SpendWise.Domain.Entities;
using SpendWise.Domain.Interfaces;

namespace SpendWise.Application.Handlers.Categorias;

public class CreateCategoriaCommandHandler : IRequestHandler<CreateCategoriaCommand, CategoriaDto>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public CreateCategoriaCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<CategoriaDto> Handle(CreateCategoriaCommand request, CancellationToken cancellationToken)
    {
        var categoria = new Categoria(request.Nome, request.Tipo, request.UsuarioId, request.Descricao);

        var result = await _unitOfWork.Categorias.AddAsync(categoria);
        await _unitOfWork.SaveChangesAsync();

        return _mapper.Map<CategoriaDto>(result);
    }
}