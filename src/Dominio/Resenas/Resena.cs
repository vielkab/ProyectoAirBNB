using Dominio.Abstracciones;
using Dominio.Resenas.Events;
using Dominio.Reservas;

namespace Dominio.Resenas;

public sealed class Resena : Entidad
{
    private Resena(
        Guid departamentoId,
        Guid reservaId,
        Guid usuarioId,
        Calificacion calificacion,
        Comentario comentario,
        DateTime creadoEnUtc
    )
    {
        DepartamentoId = departamentoId;
        ReservaId = reservaId;
        UsuarioId = usuarioId;
        Calificacion = calificacion;
        Comentario = comentario;
        CreadoEnUtc = creadoEnUtc;
    }

    public Guid DepartamentoId { get; private set; }

    public Guid ReservaId { get; private set; }

    public Guid UsuarioId { get; private set; }

    public Calificacion Calificacion { get; private set; }

    public Comentario Comentario { get; private set; }

    public DateTime CreadoEnUtc { get; private set; }

    public static Resultado<Resena> Crear(
        Reserva reserva,
        Calificacion calificacion,
        Comentario comentario,
        DateTime creadoEnUtc
    )
    {
        if (reserva.Estado != EstadoReserva.Completada)
        {
            return Resultado.Fallo<Resena>(ErroresResena.NoElegible);
        }

        var resena = new Resena(
            reserva.DepartamentoId,
            reserva.Id,
            reserva.UsuarioId,
            calificacion,
            comentario,
            creadoEnUtc
        );

        resena.RegistrarEventoDominio(new ResenaCreadaEventoDominio(resena.Id));

        return resena;
    }
}
