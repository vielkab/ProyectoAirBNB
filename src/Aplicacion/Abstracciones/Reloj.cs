namespace Aplicacion.Abstracciones;

public interface IReloj
{
    DateTime UtcNow { get; }
}