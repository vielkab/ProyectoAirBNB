using Dominio.Abstracciones;

namespace Dominio.Reservas.Events;

public sealed record ReservaConfirmadaEventoDominio(Guid ReservaId) : IEventoDominio;
