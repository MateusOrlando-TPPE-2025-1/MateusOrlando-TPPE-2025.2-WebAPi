namespace SpendWise.Application.Commands.Usuario;

public class DeleteUsuarioCommand : IRequest<bool>
{
    public Guid Id { get; set; }
}

public class DeleteUsuarioCommandValidator : AbstractValidator<DeleteUsuarioCommand>
{
    public DeleteUsuarioCommandValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty().WithMessage("Id é obrigatório");
    }
}
