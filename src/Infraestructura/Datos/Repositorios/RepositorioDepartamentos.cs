using Dominio.Departamentos;
using Microsoft.EntityFrameworkCore;
namespace Infraestructura.Datos.Repositorios;

public class RepositorioDepartamentos(ContextoAplicacion contexto) : IRepositorioDepartamentos
{
    public async Task<Departamento?> ObtenerPorIdAsync(
        Guid id, 
        CancellationToken cancellationToken = default) =>
        //return await _contexto.Departamentos.FindAsync(id, cancellationToken);

        await contexto.Departamentos.FirstOrDefaultAsync(d => d.Id == id, cancellationToken);
}