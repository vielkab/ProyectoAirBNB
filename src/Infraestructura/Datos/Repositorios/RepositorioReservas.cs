using Dominio.Departamentos;
using Dominio.Reservas;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
namespace Infraestructura.Datos.Repositorios;

public class RepositorioReservas(ContextoAplicacion contexto) : IRepositorioReservas
{
    public void Agregar(Reserva reserva)
    {
        contexto.Reservas.Add(reserva);
        contexto.SaveChanges();
    }
    public async Task<bool> HaySolapamientoAsync(
        Departamento departamento, 
        RangoFechas duracion, 
        CancellationToken cancellationToken = default) =>
    await contexto.Reservas
            .AnyAsync(r => 
            r.DepartamentoId == departamento.Id 
            && r.Duracion.Inicio < duracion.Fin
            && r.Duracion.Fin > duracion.Inicio, cancellationToken);

    public async Task<Reserva?> ObtenerPorIdAsync(
        Guid id,
        CancellationToken cancellationToken = default) =>
        await contexto.Reservas.FirstOrDefaultAsync(r => r.Id == id, cancellationToken);


}