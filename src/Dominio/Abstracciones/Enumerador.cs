namespace Dominio.Abstracciones;

public abstract record Enumerador
{
    public int Valor { get; }
    public string Nombre { get; }

    protected Enumerador(string nombre, int valor)
    {
        Nombre = nombre;
        Valor = valor;
    }
}
