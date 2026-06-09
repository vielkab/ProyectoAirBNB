using Dominio.Usuarios;
using Microsoft.EntityFrameworkCore;
namespace Infraestructura.Datos.Repositorios;

public class RepositorioUsuarios(ContextoAplicacion contexto) : IRepositorioUsuarios
{
    public void Agregar(Usuario usuario)
    {
        contexto.Usuarios.Add(usuario);
        contexto.SaveChanges();
    }
    public async Task<Usuario?> ObtenerPorIdAsync(
        Guid id, 
        CancellationToken cancellationToken = default) =>
        //return await _contexto.Departamentos.FindAsync(id, cancellationToken);

        await contexto.Usuarios.FirstOrDefaultAsync(d => d.Id == id, cancellationToken);
}