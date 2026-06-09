namespace Dominio.Departamentos;

public record Direccion(
    string Pais,
    string Provincia,
    string CodigoPostal,
    string Ciudad,
    string Calle
);
