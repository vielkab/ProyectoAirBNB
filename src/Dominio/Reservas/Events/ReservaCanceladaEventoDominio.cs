using Dominio.Abstracciones;

namespace Dominio.Reservas.Events;

public sealed record ReservaCanceladaEventoDominio(Guid ReservaId) : IEventoDominio;
