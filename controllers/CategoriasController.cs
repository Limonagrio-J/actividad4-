using ApiInicial.Dtos;
using ApiInicial.Services;
using Microsoft.AspNetCore.Mvc;

namespace ApiInicial.Controllers;

[ApiController]
[Route("api/v1/categorias")]
[Produces("application/json")]
public class CategoriasController(ICategoriaService categoriaService) : ControllerBase
{
    /// <summary>Lista todas las categorías con su cantidad de productos.</summary>
    [HttpGet]
    [ProducesResponseType(typeof(List<CategoriaResponseDto>), StatusCodes.Status200OK)]
    public async Task<ActionResult<List<CategoriaResponseDto>>> ObtenerTodas()
        => Ok(await categoriaService.ObtenerTodasAsync());

    /// <summary>Obtiene una categoría por su id.</summary>
    [HttpGet("{id:int}")]
    [ProducesResponseType(typeof(CategoriaResponseDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    public async Task<ActionResult<CategoriaResponseDto>> BuscarPorId(int id)
        => Ok(await categoriaService.BuscarPorIdAsync(id));

    /// <summary>Crea una categoría.</summary>
    [HttpPost]
    [ProducesResponseType(typeof(CategoriaResponseDto), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status409Conflict)]
    public async Task<ActionResult<CategoriaResponseDto>> Crear(CategoriaRequestDto request)
    {
        var creada = await categoriaService.CrearAsync(request);
        return CreatedAtAction(nameof(BuscarPorId), new { id = creada.Id }, creada);
    }

    /// <summary>Actualiza una categoría existente.</summary>
    [HttpPut("{id:int}")]
    [ProducesResponseType(typeof(CategoriaResponseDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status409Conflict)]
    public async Task<ActionResult<CategoriaResponseDto>> Actualizar(int id, CategoriaRequestDto request)
        => Ok(await categoriaService.ActualizarAsync(id, request));

    /// <summary>Elimina una categoría (solo si no tiene productos).</summary>
    [HttpDelete("{id:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status409Conflict)]
    public async Task<IActionResult> Eliminar(int id)
    {
        await categoriaService.EliminarAsync(id);
        return NoContent();
    }
}
