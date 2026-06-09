using Dominio.Abstracciones;

namespace Dominio.Usuarios;

public static class ErroresUsuario
{
    public static Error NoEncontrado = new(
        "Usuario.NoEncontrado",
        "No se encontró el usuario con el identificador especificado"
    );

    public static Error CredencialesInvalidas = new(
        "Usuario.CredencialesInvalidas",
        "Las credenciales proporcionadas no son válidas"
    );
}
