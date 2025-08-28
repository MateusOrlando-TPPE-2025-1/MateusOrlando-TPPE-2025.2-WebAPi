using SpendWise.Application.DTOs;

namespace SpendWise.Application.Commands.Usuario;

public class CreateUsuarioCommand : IRequest<UsuarioDto>
{
    public string Nome { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public decimal RendaMensal { get; set; }
}

public class CreateUsuarioCommandValidator : AbstractValidator<CreateUsuarioCommand>
{
    public CreateUsuarioCommandValidator()
    {
        RuleFor(x => x.Nome)
            .NotEmpty().WithMessage("Nome é obrigatório")
            .MinimumLength(2).WithMessage("Nome deve ter pelo menos 2 caracteres")
            .MaximumLength(100).WithMessage("Nome deve ter no máximo 100 caracteres");

        RuleFor(x => x.Email)
            .NotEmpty().WithMessage("Email é obrigatório")
            .EmailAddress().WithMessage("Email deve ter um formato válido");

        RuleFor(x => x.Password)
            .NotEmpty().WithMessage("Password é obrigatório")
            .MinimumLength(6).WithMessage("Password deve ter pelo menos 6 caracteres");

        RuleFor(x => x.RendaMensal)
            .GreaterThanOrEqualTo(0).WithMessage("Renda mensal não pode ser negativa");
    }
}
