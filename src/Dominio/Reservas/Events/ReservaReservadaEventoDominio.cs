using Dominio.Abstracciones;

namespace Dominio.Reservas.Events;

public sealed record ReservaReservadaEventoDominio(Guid ReservaId) : IEventoDominio;
