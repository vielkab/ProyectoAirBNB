using Dominio.Abstracciones;

namespace Dominio.Resenas;

public sealed record Calificacion
{
    public static readonly Error Invalida = new(
        "Calificacion.Invalida",
        "La calificación es inválida"
    );

    private Calificacion(int valor) => Valor = valor;

    public int Valor { get; init; }

    public static Resultado<Calificacion> Crear(int valor)
    {
        if (valor < 1 || valor > 5)
        {
            return Resultado.Fallo<Calificacion>(Invalida);
        }

        return new Calificacion(valor);
    }
}
