using SpendWise.Application.DTOs;

namespace SpendWise.Application.Queries.Usuario;

public class GetUsuarioByIdQuery : IRequest<UsuarioDto?>
{
    public Guid Id { get; set; }
}

public class GetUsuarioByEmailQuery : IRequest<UsuarioDto?>
{
    public string Email { get; set; } = string.Empty;
}

public class GetAllUsuariosQuery : IRequest<IEnumerable<UsuarioDto>>
{
}
