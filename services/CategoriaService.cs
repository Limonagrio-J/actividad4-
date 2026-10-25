using ApiInicial.Core;
using ApiInicial.Dtos;
using ApiInicial.Models;
using Microsoft.EntityFrameworkCore;

namespace ApiInicial.Services;

public class CategoriaService(AppDbContext context) : ICategoriaService
{
    public async Task<List<CategoriaResponseDto>> ObtenerTodasAsync()
    {
        return await context.Categorias
            .AsNoTracking()
            .OrderBy(c => c.Id)
            .Select(c => new CategoriaResponseDto
            {
                Id = c.Id,
                Nombre = c.Nombre,
                Descripcion = c.Descripcion,
                CantidadProductos = c.Productos.Count,
            })
            .ToListAsync();
    }

    public async Task<CategoriaResponseDto> BuscarPorIdAsync(int id)
    {
        return await context.Categorias
            .AsNoTracking()
            .Where(c => c.Id == id)
            .Select(c => new CategoriaResponseDto
            {
                Id = c.Id,
                Nombre = c.Nombre,
                Descripcion = c.Descripcion,
                CantidadProductos = c.Productos.Count,
            })
            .FirstOrDefaultAsync()
            ?? throw new NotFoundException($"No existe la categoría con id {id}.");
    }

    public async Task<CategoriaResponseDto> CrearAsync(CategoriaRequestDto request)
    {
        var nombre = request.Nombre.Trim();
        if (await context.Categorias.AnyAsync(c => c.Nombre == nombre))
            throw new ConflictException($"Ya existe una categoría llamada '{nombre}'.");

        var categoria = new Categoria { Nombre = nombre, Descripcion = request.Descripcion };
        context.Categorias.Add(categoria);
        await context.SaveChangesAsync();

        return await BuscarPorIdAsync(categoria.Id);
    }

    public async Task<CategoriaResponseDto> ActualizarAsync(int id, CategoriaRequestDto request)
    {
        var categoria = await context.Categorias.FirstOrDefaultAsync(c => c.Id == id)
            ?? throw new NotFoundException($"No existe la categoría con id {id}.");

        var nombre = request.Nombre.Trim();
        if (await context.Categorias.AnyAsync(c => c.Nombre == nombre && c.Id != id))
            throw new ConflictException($"Ya existe una categoría llamada '{nombre}'.");

        categoria.Nombre = nombre;
        categoria.Descripcion = request.Descripcion;
        await context.SaveChangesAsync();

        return await BuscarPorIdAsync(id);
    }

    public async Task EliminarAsync(int id)
    {
        var categoria = await context.Categorias.FindAsync(id)
            ?? throw new NotFoundException($"No existe la categoría con id {id}.");

        if (await context.Productos.AnyAsync(p => p.CategoriaId == id))
            throw new ConflictException("No se puede eliminar una categoría que tiene productos asociados.");

        context.Categorias.Remove(categoria);
        await context.SaveChangesAsync();
    }
}
