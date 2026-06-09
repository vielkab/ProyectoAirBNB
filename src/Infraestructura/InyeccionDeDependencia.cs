using Microsoft.EntityFrameworkCore;
using Infraestructura.Datos;
using Microsoft.Extensions.DependencyInjection;
using Dominio.Reservas;
using Dominio.Usuarios;
using Dominio.Departamentos;
using Infraestructura.Datos.Repositorios;
using Aplicacion.Abstracciones;
using Microsoft.Extensions.Configuration;
namespace Infraestructura;

public static class InyecionDeDependencia
{
    public static void AgregarInfraestructura(this IServiceCollection servicios, IConfiguration configuracion)
    {
        servicios.AddDbContext<ContextoAplicacion>(options =>
            options.UseNpgsql("servidorpostgres"));
        servicios.AddScoped<IRepositorioReservas, RepositorioReservas>();
        servicios.AddScoped<IRepositorioDepartamentos, RepositorioDepartamentos>();
        servicios.AddScoped<IRepositorioUsuarios, RepositorioUsuarios>();
        servicios.AddScoped<IReloj, Reloj.Reloj>();
    }

}
