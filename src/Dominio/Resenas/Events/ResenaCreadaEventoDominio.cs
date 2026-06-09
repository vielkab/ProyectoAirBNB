using Dominio.Abstracciones;

namespace Dominio.Resenas.Events;

public sealed record ResenaCreadaEventoDominio(Guid ResenaId) : IEventoDominio;
