using Microsoft.AspNetCore.Mvc;
using Aplicacion.DepartamentosFt.Queries;

namespace Api.Controllers;

[ApiController]
[Route("[controller]")]
public class DepartamentoController : ControllerBase
{

    [HttpGet(Name = "{id}")]
    public async Task<ActionResult<DepartamentoDto>> ObtenerPorId(Guid id)
    {
        var Departamento = DepartamentoDto.Crear(Guid.NewGuid(), "Departamento de prueba", 1000);
        return Ok(Departamento);
    }
}