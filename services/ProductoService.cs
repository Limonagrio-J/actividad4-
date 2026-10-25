using ApiInicial.Core;
using ApiInicial.Dtos;
using ApiInicial.Models;
using Microsoft.EntityFrameworkCore;

namespace ApiInicial.Services;

public class ProductoService(AppDbContext context) : IProductoService
{
    public async Task<List<ProductoResponseDto>> ObtenerTodosAsync()
    {
        return await context.Productos
            .AsNoTracking()
            .OrderBy(p => p.Id)
            .Select(p => new ProductoResponseDto
            {
                Id = p.Id,
                Nombre = p.Nombre,
                Precio = p.Precio,
                Stock = p.Stock,
                Estado = p.Estado,
                CategoriaId = p.CategoriaId,
                NombreCategoria = p.Categoria!.Nombre,
            })
            .ToListAsync();
    }

    public async Task<ProductoResponseDto> BuscarPorIdAsync(int id)
    {
        var producto = await context.Productos
            .AsNoTracking()
            .Include(p => p.Categoria)
            .FirstOrDefaultAsync(p => p.Id == id)
            ?? throw new NotFoundException($"No existe el producto con id {id}.");

        return ToDto(producto);
    }

    public async Task<ProductoResponseDto> CrearAsync(ProductoRequestDto request)
    {
        await ValidarCategoriaAsync(request.CategoriaId);

        var producto = new Producto
        {
            Nombre = request.Nombre.Trim(),
            Precio = request.Precio,
            Stock = request.Stock,
            Estado = true,
            CategoriaId = request.CategoriaId,
        };
        context.Productos.Add(producto);
        await context.SaveChangesAsync();

        return await BuscarPorIdAsync(producto.Id);
    }

    public async Task<ProductoResponseDto> ActualizarAsync(int id, ProductoRequestDto request)
    {
        var producto = await context.Productos.FirstOrDefaultAsync(p => p.Id == id)
            ?? throw new NotFoundException($"No existe el producto con id {id}.");

        await ValidarCategoriaAsync(request.CategoriaId);

        producto.Nombre = request.Nombre.Trim();
        producto.Precio = request.Precio;
        producto.Stock = request.Stock;
        producto.CategoriaId = request.CategoriaId;
        await context.SaveChangesAsync();

        return await BuscarPorIdAsync(id);
    }

    public async Task EliminarAsync(int id)
    {
        var producto = await context.Productos.FindAsync(id)
            ?? throw new NotFoundException($"No existe el producto con id {id}.");

        context.Productos.Remove(producto);
        await context.SaveChangesAsync();
    }

    private async Task ValidarCategoriaAsync(int categoriaId)
    {
        if (!await context.Categorias.AnyAsync(c => c.Id == categoriaId))
            throw new BadRequestException($"La categoría con id {categoriaId} no existe.");
    }

    private static ProductoResponseDto ToDto(Producto p) => new()
    {
        Id = p.Id,
        Nombre = p.Nombre,
        Precio = p.Precio,
        Stock = p.Stock,
        Estado = p.Estado,
        CategoriaId = p.CategoriaId,
        NombreCategoria = p.Categoria?.Nombre ?? string.Empty,
    };
}
