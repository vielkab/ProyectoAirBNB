using Microsoft.EntityFrameworkCore;
using Dominio.Usuarios;
using Dominio.Departamentos;
using Dominio.Reservas;
namespace Infraestructura.Datos;
public class ContextoAplicacion : DbContext
{
    public ContextoAplicacion(DbContextOptions<ContextoAplicacion> options) : base(options)
    {
    }

    public DbSet<Usuario> Usuarios => Set<Usuario>();
    public DbSet<Departamento> Departamentos => Set<Departamento>();
    
    public DbSet<Reserva> Reservas => Set<Reserva>();

}