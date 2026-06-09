using Dominio.Abstracciones;

namespace Dominio.Reservas;

public record EstadoReserva : Enumerador
{
    public static readonly EstadoReserva Reservada = new(nameof(Reservada), 1);
    public static readonly EstadoReserva Confirmada = new(nameof(Confirmada), 2);
    public static readonly EstadoReserva Rechazada = new(nameof(Rechazada), 3);
    public static readonly EstadoReserva Cancelada = new(nameof(Cancelada), 4);
    public static readonly EstadoReserva Completada = new(nameof(Completada), 5);

    private EstadoReserva(string nombre, int valor)
        : base(nombre, valor) { }

    public static List<EstadoReserva> Lista =>
        [Reservada, Confirmada, Rechazada, Cancelada, Completada];

    public static EstadoReserva DesdeValor(int valor) => Lista.Single(s => s.Valor == valor);

    public static EstadoReserva DesdeNombre(string nombre) => Lista.Single(s => s.Nombre == nombre);
}
