namespace Dominio.Departamentos;

public interface IRepositorioDepartamentos
{
    Task<Departamento?> ObtenerPorIdAsync(Guid id, CancellationToken cancellationToken = default);
}
