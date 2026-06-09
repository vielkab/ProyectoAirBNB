using Dominio.Abstracciones;

namespace Dominio.Reservas;

public static class ErroresReserva
{
    public static Error NoEncontrado = new(
        "Reserva.NoEncontrado",
        "No se encontró la reserva con el identificador especificado"
    );

    public static Error Solapamiento = new(
        "Reserva.Solapamiento",
        "La reserva actual se solapa con una existente"
    );

    public static Error NoReservado = new("Reserva.NoReservado", "La reserva no está pendiente");

    public static Error NoConfirmado = new("Reserva.NoConfirmada", "La reserva no está confirmada");

    public static Error YaIniciada = new("Reserva.YaIniciada", "La reserva ya inició");
}
