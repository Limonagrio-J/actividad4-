using System.ComponentModel.DataAnnotations;

namespace ApiInicial.Dtos;

public class CategoriaRequestDto
{
    [Required(ErrorMessage = "El nombre es obligatorio")]
    [StringLength(100, MinimumLength = 2, ErrorMessage = "El nombre debe tener entre 2 y 100 caracteres")]
    public string Nombre { get; set; } = string.Empty;

    [StringLength(250, ErrorMessage = "La descripción no puede superar los 250 caracteres")]
    public string? Descripcion { get; set; }
}
