using Dominio.Abstracciones;
using Dominio.Compartido;
using Dominio.Departamentos;
using Dominio.Reservas.Events;

namespace Dominio.Reservas;

public sealed class Reserva : Entidad
{
    private Reserva(
        Guid departamentoId,
        Guid usuarioId,
        RangoFechas duracion,
        Dinero precioPorPeriodo,
        Dinero tarifaLimpieza,
        Dinero recargoComodidades,
        Dinero precioTotal,
        EstadoReserva estado,
        DateTime creadoEnUtc
    )
    {
        DepartamentoId = departamentoId;
        UsuarioId = usuarioId;
        Duracion = duracion;
        PrecioPorPeriodo = precioPorPeriodo;
        TarifaLimpieza = tarifaLimpieza;
        RecargoComodidades = recargoComodidades;
        PrecioTotal = precioTotal;
        Estado = estado;
        CreadoEnUtc = creadoEnUtc;
    }

    public Guid DepartamentoId { get; private set; }

    public Guid UsuarioId { get; private set; }

    public RangoFechas Duracion { get; private set; }

    public Dinero PrecioPorPeriodo { get; private set; }

    public Dinero TarifaLimpieza { get; private set; }

    public Dinero RecargoComodidades { get; private set; }

    public Dinero PrecioTotal { get; private set; }

    public EstadoReserva Estado { get; private set; }

    public DateTime CreadoEnUtc { get; private set; }

    public DateTime? ConfirmadoEnUtc { get; private set; }

    public DateTime? RechazadoEnUtc { get; private set; }

    public DateTime? CompletadoEnUtc { get; private set; }

    public DateTime? CanceladoEnUtc { get; private set; }

    public static Reserva Reservar(
        Departamento departamento,
        Guid usuarioId,
        RangoFechas duracion,
        DateTime utcNow
    )
    {
        var detallesPrecio = ServicioPrecios.CalcularPrecio(departamento, duracion);

        var reserva = new Reserva(
            departamento.Id,
            usuarioId,
            duracion,
            detallesPrecio.PrecioPorPeriodo,
            detallesPrecio.TarifaLimpieza,
            detallesPrecio.RecargoComodidades,
            detallesPrecio.PrecioTotal,
            EstadoReserva.Reservada,
            utcNow
        );

        reserva.RegistrarEventoDominio(new ReservaReservadaEventoDominio(reserva.Id));

        departamento.UltimaReservaEnUtc = utcNow;

        return reserva;
    }

    public Resultado Confirmar(DateTime utcNow)
    {
        if (Estado != EstadoReserva.Reservada)
        {
            return Resultado.Fallo(ErroresReserva.NoReservado);
        }

        Estado = EstadoReserva.Confirmada;
        ConfirmadoEnUtc = utcNow;

        RegistrarEventoDominio(new ReservaConfirmadaEventoDominio(Id));

        return Resultado.Exito();
    }

    public Resultado Rechazar(DateTime utcNow)
    {
        if (Estado != EstadoReserva.Reservada)
        {
            return Resultado.Fallo(ErroresReserva.NoReservado);
        }

        Estado = EstadoReserva.Rechazada;
        RechazadoEnUtc = utcNow;

        RegistrarEventoDominio(new ReservaRechazadaEventoDominio(Id));

        return Resultado.Exito();
    }

    public Resultado Completar(DateTime utcNow)
    {
        if (Estado != EstadoReserva.Confirmada)
        {
            return Resultado.Fallo(ErroresReserva.NoConfirmado);
        }

        Estado = EstadoReserva.Completada;
        CompletadoEnUtc = utcNow;

        RegistrarEventoDominio(new ReservaCompletadaEventoDominio(Id));

        return Resultado.Exito();
    }

    public Resultado Cancelar(DateTime utcNow)
    {
        if (Estado != EstadoReserva.Confirmada)
        {
            return Resultado.Fallo(ErroresReserva.NoConfirmado);
        }

        var currentDate = DateOnly.FromDateTime(utcNow);

        if (currentDate > Duracion.Inicio)
        {
            return Resultado.Fallo(ErroresReserva.YaIniciada);
        }

        Estado = EstadoReserva.Cancelada;
        CanceladoEnUtc = utcNow;

        RegistrarEventoDominio(new ReservaCanceladaEventoDominio(Id));

        return Resultado.Exito();
    }
}
