using Dominio.Abstracciones;

namespace Dominio.Departamentos;

public abstract record Comodidad : Enumerador
{
    public static readonly Comodidad WiFi = new ComodidadWiFi();
    public static readonly Comodidad AireAcondicionado = new ComodidadAireAcondicionado();
    public static readonly Comodidad Estacionamiento = new ComodidadEstacionamiento();
    public static readonly Comodidad AceptaMascotas = new ComodidadAceptaMascotas();
    public static readonly Comodidad Piscina = new ComodidadPiscina();
    public static readonly Comodidad Gimnasio = new ComodidadGimnasio();
    public static readonly Comodidad Spa = new ComodidadSpa();
    public static readonly Comodidad Terraza = new ComodidadTerraza();
    public static readonly Comodidad VistaMontana = new ComodidadVistaMontana();
    public static readonly Comodidad VistaJardin = new ComodidadVistaJardin();

    private Comodidad(string nombre, int valor)
        : base(nombre, valor) { }

    public abstract decimal Porcentaje { get; }
    public static List<Comodidad> Lista =>
        new()
        {
            WiFi,
            AireAcondicionado,
            Estacionamiento,
            AceptaMascotas,
            Piscina,
            Gimnasio,
            Spa,
            Terraza,
            VistaMontana,
            VistaJardin,
        };

    public static Comodidad DesdeValor(int valor) => Lista.Single(a => a.Valor == valor);

    public static Comodidad DesdeNombre(string nombre) => Lista.Single(a => a.Nombre == nombre);

    private record ComodidadWiFi : Comodidad
    {
        public override decimal Porcentaje => 0.05m;

        public ComodidadWiFi()
            : base(nameof(WiFi), 1) { }
    }

    private record ComodidadAireAcondicionado : Comodidad
    {
        public override decimal Porcentaje => 0.1m;

        public ComodidadAireAcondicionado()
            : base(nameof(AireAcondicionado), 2) { }
    }

    private record ComodidadEstacionamiento : Comodidad
    {
        public override decimal Porcentaje => 0.08m;

        public ComodidadEstacionamiento()
            : base(nameof(Estacionamiento), 3) { }
    }

    private record ComodidadAceptaMascotas : Comodidad
    {
        public override decimal Porcentaje => 0.12m;

        public ComodidadAceptaMascotas()
            : base(nameof(AceptaMascotas), 4) { }
    }

    private record ComodidadPiscina : Comodidad
    {
        public override decimal Porcentaje => 0.15m;

        public ComodidadPiscina()
            : base(nameof(Piscina), 5) { }
    }

    private record ComodidadGimnasio : Comodidad
    {
        public override decimal Porcentaje => 0.1m;

        public ComodidadGimnasio()
            : base(nameof(Gimnasio), 6) { }
    }

    private record ComodidadSpa : Comodidad
    {
        public override decimal Porcentaje => 0.2m;

        public ComodidadSpa()
            : base(nameof(Spa), 7) { }
    }

    private record ComodidadTerraza : Comodidad
    {
        public override decimal Porcentaje => 0.07m;

        public ComodidadTerraza()
            : base(nameof(Terraza), 8) { }
    }

    private record ComodidadVistaMontana : Comodidad
    {
        public override decimal Porcentaje => 0.1m;

        public ComodidadVistaMontana()
            : base(nameof(VistaMontana), 9) { }
    }

    private record ComodidadVistaJardin : Comodidad
    {
        public override decimal Porcentaje => 0.08m;

        public ComodidadVistaJardin()
            : base(nameof(VistaJardin), 10) { }
    }
}
