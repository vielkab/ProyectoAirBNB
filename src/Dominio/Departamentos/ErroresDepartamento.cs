using Dominio.Abstracciones;

namespace Dominio.Departamentos;

public static class ErroresDepartamento
{
    public static readonly Error NoEncontrado = new(
        "Departamento.NoEncontrado",
        "No se encontró el departamento con el identificador especificado"
    );
}
