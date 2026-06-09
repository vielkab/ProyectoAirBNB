using Dominio.Abstracciones;

namespace Dominio.Reservas.Events;

public sealed record ReservaRechazadaEventoDominio(Guid ReservaId) : IEventoDominio;
