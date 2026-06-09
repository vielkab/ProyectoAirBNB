using Dominio.Compartido;

namespace Dominio.Reservas;

public record DetallesPrecio(
    Dinero PrecioPorPeriodo,
    Dinero TarifaLimpieza,
    Dinero RecargoComodidades,
    Dinero PrecioTotal
);
