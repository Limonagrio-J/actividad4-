using ApiInicial.Dtos;

namespace ApiInicial.Services;

public interface ICategoriaService
{
    Task<List<CategoriaResponseDto>> ObtenerTodasAsync();
    Task<CategoriaResponseDto> BuscarPorIdAsync(int id);
    Task<CategoriaResponseDto> CrearAsync(CategoriaRequestDto request);
    Task<CategoriaResponseDto> ActualizarAsync(int id, CategoriaRequestDto request);
    Task EliminarAsync(int id);
}
