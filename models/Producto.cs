using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ApiInicial.Models;

public class Producto
{
    [Key]
    public int Id { get; set; }

    [Required]
    [MaxLength(100)]
    public string Nombre { get; set; } = string.Empty;

    [Column(TypeName = "numeric(10,2)")]
    public decimal Precio { get; set; }

    public int Stock { get; set; }

    public bool Estado { get; set; } = true;

    [ForeignKey(nameof(Categoria))]
    public int CategoriaId { get; set; }

    public Categoria? Categoria { get; set; }
}
