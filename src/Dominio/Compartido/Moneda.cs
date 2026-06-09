namespace Dominio.Compartido;

public record Moneda
{
    internal static readonly Moneda Ninguno = new("");
    public static readonly Moneda Dolar = new("USD");
    public static readonly Moneda Euro = new("EUR");

    private Moneda(string codigo) => Codigo = codigo;

    public string Codigo { get; init; }

    public static Moneda DesdeCodigo(string codigo)
    {
        return Todas.FirstOrDefault(c => c.Codigo == codigo)
            ?? throw new ApplicationException("El código de moneda es inválido");
    }

    public static readonly IReadOnlyCollection<Moneda> Todas = [Dolar, Euro];
}
