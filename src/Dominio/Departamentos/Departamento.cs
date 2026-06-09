using Dominio.Abstracciones;
using Dominio.Compartido;

namespace Dominio.Departamentos;

public sealed class Departamento : Entidad
{
    public Departamento(
        Nombre nombre,
        Descripcion descripcion,
        Direccion direccion,
        Dinero precio,
        Dinero tarifaLimpieza,
        List<Comodidad> comodidades
    )
    {
        Nombre = nombre;
        Descripcion = descripcion;
        Direccion = direccion;
        Precio = precio;
        TarifaLimpieza = tarifaLimpieza;
        Comodidades = comodidades;
    }

    public Nombre Nombre { get; private set; }

    public Descripcion Descripcion { get; private set; }

    public Direccion Direccion { get; private set; }

    public Dinero Precio { get; private set; }

    public Dinero TarifaLimpieza { get; private set; }

    public DateTime? UltimaReservaEnUtc { get; internal set; }

    public List<Comodidad> Comodidades { get; private set; } = new();
}
