namespace Aplicacion.ReservasFt.Comandos;

using Dominio.Abstracciones;
using Dominio.Departamentos;
using Dominio.Reservas;
using Dominio.Usuarios;
using Aplicacion.Abstracciones;

public interface IReservar
{
    Task<Resultado<Guid>> EjecutarAsync(
        Guid idUsuario, 
        Guid idDepartamento, 
        DateOnly fechaInicio, 
        DateOnly fachaFin,
        CancellationToken cancellationToken = default);
}

public class Reservar(IRepositorioUsuarios repositorioUsuarios, 
    IRepositorioDepartamentos repositorioDepartamentos, 
    IRepositorioReservas repositorioReservas, 
    IUnidadDeTrabajo unidadDeTrabajo, 
    IReloj reloj) : IReservar
{

    public async Task<Resultado<Guid>> EjecutarAsync(
        Guid idUsuario, 
        Guid idDepartamento, 
        DateOnly fechaInicio, 
        DateOnly fachaFin,
        CancellationToken cancellationToken = default)
    {
        var usuario = await repositorioUsuarios.ObtenerPorIdAsync(idUsuario, cancellationToken);

        if (usuario is null)
        {
            return Resultado.Fallo<Guid>(ErroresUsuario.NoEncontrado);
        }

        var departamento = await repositorioDepartamentos.ObtenerPorIdAsync(idDepartamento);

        if (departamento is null)
        {
            return Resultado.Fallo<Guid>(ErroresDepartamento.NoEncontrado);
        }

        var rangoFechas = RangoFechas.Crear(fechaInicio, fachaFin);

        var haySolapamiento = await repositorioReservas.HaySolapamientoAsync(departamento, rangoFechas);

        if (haySolapamiento)
        {
            return Resultado.Fallo<Guid>(ErroresReserva.Solapamiento);
        }
        var reserva = Reserva.Reservar(
            departamento, 
            usuario.Id, 
            rangoFechas, 
            reloj.UtcNow);
            
        repositorioReservas.Agregar(reserva);
        await unidadDeTrabajo.GuardarCambiosAsync(cancellationToken);
        return reserva.Id;

    }
}