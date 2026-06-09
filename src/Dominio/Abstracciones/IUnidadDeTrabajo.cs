namespace Dominio.Abstracciones;

public interface IUnidadDeTrabajo
{
    Task<int> GuardarCambiosAsync(CancellationToken cancellationToken = default);
}
