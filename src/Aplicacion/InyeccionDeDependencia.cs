using Aplicacion.ReservasFt.Comandos;
using Microsoft.Extensions.DependencyInjection;
using Aplicacion.DepartamentosFt.Queries;

namespace Aplicacion;

public static class InyecionDeDependencia
{
    public static void AgregarAplicacion(this IServiceCollection servicios)
    {
        servicios.AddScoped<IReservar, Reservar>();
        servicios.AddScoped<IObtenerPorId, ObtenerPorId>();
    }

}
