using ApiInicial.Dtos;
using ApiInicial.Services;
using Microsoft.AspNetCore.Mvc;

namespace ApiInicial.Controllers;

[ApiController]
[Route("api/v1/productos")]
[Produces("application/json")]
public class ProductosController(IProductoService productoService) : ControllerBase
{
    /// <summary>Lista todos los productos.</summary>
    [HttpGet]
    [ProducesResponseType(typeof(List<ProductoResponseDto>), StatusCodes.Status200OK)]
    public async Task<ActionResult<List<ProductoResponseDto>>> ObtenerTodos()
        => Ok(await productoService.ObtenerTodosAsync());

    /// <summary>Obtiene un producto por su id.</summary>
    [HttpGet("{id:int}")]
    [ProducesResponseType(typeof(ProductoResponseDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    public async Task<ActionResult<ProductoResponseDto>> BuscarPorId(int id)
        => Ok(await productoService.BuscarPorIdAsync(id));

    /// <summary>Crea un producto.</summary>
    [HttpPost]
    [ProducesResponseType(typeof(ProductoResponseDto), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<ProductoResponseDto>> Crear(ProductoRequestDto request)
    {
        var creado = await productoService.CrearAsync(request);
        return CreatedAtAction(nameof(BuscarPorId), new { id = creado.Id }, creado);
    }

    /// <summary>Actualiza un producto existente.</summary>
    [HttpPut("{id:int}")]
    [ProducesResponseType(typeof(ProductoResponseDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    public async Task<ActionResult<ProductoResponseDto>> Actualizar(int id, ProductoRequestDto request)
        => Ok(await productoService.ActualizarAsync(id, request));

    /// <summary>Elimina un producto.</summary>
    [HttpDelete("{id:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Eliminar(int id)
    {
        await productoService.EliminarAsync(id);
        return NoContent();
    }
}
