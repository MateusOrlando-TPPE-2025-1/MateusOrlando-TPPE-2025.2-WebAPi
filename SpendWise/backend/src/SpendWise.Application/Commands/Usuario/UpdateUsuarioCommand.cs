using SpendWise.Application.DTOs;

namespace SpendWise.Application.Commands.Usuario;

public class UpdateUsuarioCommand : IRequest<UsuarioDto>
{
    public Guid Id { get; set; }
    public string Nome { get; set; } = string.Empty;
    public decimal RendaMensal { get; set; }
}

public class UpdateUsuarioCommandValidator : AbstractValidator<UpdateUsuarioCommand>
{
    public UpdateUsuarioCommandValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty().WithMessage("Id é obrigatório");

        RuleFor(x => x.Nome)
            .NotEmpty().WithMessage("Nome é obrigatório")
            .MinimumLength(2).WithMessage("Nome deve ter pelo menos 2 caracteres")
            .MaximumLength(100).WithMessage("Nome deve ter no máximo 100 caracteres");

        RuleFor(x => x.RendaMensal)
            .GreaterThanOrEqualTo(0).WithMessage("Renda mensal não pode ser negativa");
    }
}
