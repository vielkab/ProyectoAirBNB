namespace Dominio.Reservas;

public record RangoFechas
{
    private RangoFechas() { }

    public DateOnly Inicio { get; init; }

    public DateOnly Fin { get; init; }

    public int DuracionEnDias => Fin.DayNumber - Inicio.DayNumber;

    public static RangoFechas Crear(DateOnly inicio, DateOnly fin)
    {
        if (inicio > fin)
        {
            throw new ApplicationException("La fecha de fin precede a la fecha de inicio");
        }

        return new RangoFechas { Inicio = inicio, Fin = fin };
    }
}
