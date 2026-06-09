using Dominio.Abstracciones;

namespace Dominio.Resenas;

public static class ErroresResena
{
    public static readonly Error NoElegible = new(
        "Resena.NoElegible",
        "La reseña no es elegible porque la reserva aún no está completada"
    );
}
