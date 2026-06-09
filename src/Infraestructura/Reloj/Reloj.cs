namespace Infraestructura.Reloj;
using Aplicacion.Abstracciones;

public class Reloj : IReloj
{
    public DateTime UtcNow => DateTime.UtcNow;
}