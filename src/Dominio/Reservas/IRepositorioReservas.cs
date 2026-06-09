using Dominio.Departamentos;

namespace Dominio.Reservas;

public interface IRepositorioReservas
{
    Task<Reserva?> ObtenerPorIdAsync(Guid id, CancellationToken cancellationToken = default);

    Task<bool> HaySolapamientoAsync(
        Departamento departamento,
        RangoFechas duracion,
        CancellationToken cancellationToken = default
    );

    void Agregar(Reserva reserva);
}
