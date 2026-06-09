using Dominio.Abstracciones;

namespace Dominio.Reservas.Events;

public sealed record ReservaCompletadaEventoDominio(Guid ReservaId) : IEventoDominio;
