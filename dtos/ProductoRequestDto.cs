using System.ComponentModel.DataAnnotations;

namespace ApiInicial.Dtos;

public class ProductoRequestDto
{
    [Required(ErrorMessage = "El nombre es obligatorio")]
    [StringLength(100, MinimumLength = 2, ErrorMessage = "El nombre debe tener entre 2 y 100 caracteres")]
    public string Nombre { get; set; } = string.Empty;

    [Range(0.01, 99999999.99, ErrorMessage = "El precio debe ser mayor a cero")]
    public decimal Precio { get; set; }

    [Range(0, int.MaxValue, ErrorMessage = "El stock no puede ser negativo")]
    public int Stock { get; set; }

    [Range(1, int.MaxValue, ErrorMessage = "Debe indicar una categoría válida")]
    public int CategoriaId { get; set; }
}
