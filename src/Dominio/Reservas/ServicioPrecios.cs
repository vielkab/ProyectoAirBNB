using Dominio.Compartido;
using Dominio.Departamentos;

namespace Dominio.Reservas;

public class ServicioPrecios
{
    public static DetallesPrecio CalcularPrecio(Departamento departamento, RangoFechas periodo)
    {
        var moneda = departamento.Precio.Moneda;

        var precioPorPeriodo = new Dinero(
            departamento.Precio.Monto * periodo.DuracionEnDias,
            moneda
        );

        decimal porcentajeDeRecargo = departamento.Comodidades.Sum(a => a.Porcentaje);

        var recargoComodidades = Dinero.Cero(moneda);
        if (porcentajeDeRecargo > 0)
        {
            recargoComodidades = new Dinero(precioPorPeriodo.Monto * porcentajeDeRecargo, moneda);
        }

        var precioTotal = Dinero.Cero(moneda);

        precioTotal += precioPorPeriodo;

        if (!departamento.TarifaLimpieza.EsCero())
        {
            precioTotal += departamento.TarifaLimpieza;
        }

        precioTotal += recargoComodidades;

        return new DetallesPrecio(
            precioPorPeriodo,
            departamento.TarifaLimpieza,
            recargoComodidades,
            precioTotal
        );
    }
}
