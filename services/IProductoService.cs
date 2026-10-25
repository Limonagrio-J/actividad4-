using ApiInicial.Dtos;

namespace ApiInicial.Services;

public interface IProductoService
{
    Task<List<ProductoResponseDto>> ObtenerTodosAsync();
    Task<ProductoResponseDto> BuscarPorIdAsync(int id);
    Task<ProductoResponseDto> CrearAsync(ProductoRequestDto request);
    Task<ProductoResponseDto> ActualizarAsync(int id, ProductoRequestDto request);
    Task EliminarAsync(int id);
}
