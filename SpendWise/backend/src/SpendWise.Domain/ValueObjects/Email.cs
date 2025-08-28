using System.Text.RegularExpressions;

namespace SpendWise.Domain.ValueObjects;

public class Email
{
    private static readonly Regex EmailRegex = new(
        @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$",
        RegexOptions.Compiled);

    public string Valor { get; private set; }

    private Email() { } // Para EF Core

    public Email(string valor)
    {
        if (string.IsNullOrWhiteSpace(valor))
            throw new ArgumentException("Email não pode ser vazio", nameof(valor));

        if (!EmailRegex.IsMatch(valor))
            throw new ArgumentException("Formato de email inválido", nameof(valor));

        Valor = valor.ToLowerInvariant();
    }

    public static implicit operator string(Email email) => email.Valor;
    public static implicit operator Email(string email) => new(email);

    public override string ToString() => Valor;

    public override bool Equals(object? obj)
    {
        if (obj is not Email other) return false;
        return Valor == other.Valor;
    }

    public override int GetHashCode() => Valor.GetHashCode();
}
