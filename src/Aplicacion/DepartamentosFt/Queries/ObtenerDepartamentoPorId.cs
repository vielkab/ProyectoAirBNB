using System.Data.Common;
using Dominio.Abstracciones;
using Dominio.Departamentos;

namespace Aplicacion.DepartamentosFt.Queries;

public interface IObtenerPorId 
{
    Task<Resultado<DepartamentoDto>> EjecutarAsync(Guid id, CancellationToken cancellationToken = default);
}

public class ObtenerPorId(IRepositorioDepartamentos repositorioDepartamentos) : IObtenerPorId
{
    public async Task<Resultado<DepartamentoDto>> EjecutarAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var departamento = await repositorioDepartamentos.ObtenerPorIdAsync(id, cancellationToken);
        if (departamento is null)
        {
            return Resultado.Fallo<DepartamentoDto>(ErroresDepartamento.NoEncontrado);
        }
        var dto = DepartamentoDto.Mapear(departamento);
        return dto;
    }
}

public record DepartamentoDto
{
    private DepartamentoDto(Guid id, string nombre, decimal precio)
    {
        Id = id;
        Nombre = nombre;
        Precio = precio;
    }
    public string Nombre { get; private init; }
    public Guid Id { get; private init; }
    public decimal Precio { get; private init; }

    public static DepartamentoDto Mapear(Departamento departamento)
    {
        return new DepartamentoDto(departamento.Id, departamento.Nombre.Valor, departamento.Precio.Monto);
    }

    public static DepartamentoDto Crear(Guid guid, string nombre, decimal precio)
    {
        return new DepartamentoDto(guid, nombre, precio);
    }
}
