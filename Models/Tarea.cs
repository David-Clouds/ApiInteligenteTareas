using System.ComponentModel.DataAnnotations;

namespace ApiInteligenteTareas.Models;

public class Tarea
{
    public int Id { get; set; }

    [Required(ErrorMessage = "El título es obligatorio")]
    public string Titulo { get; set; } = string.Empty;

    public string? Descripcion { get; set; }

    [Required(ErrorMessage = "El estado es obligatorio")]
    public string Estado { get; set; } = string.Empty;

    [Required(ErrorMessage = "La prioridad es obligatoria")]
    public string Prioridad { get; set; } = string.Empty;

    public DateTime FechaCreacion { get; set; } = DateTime.Now;

    public DateTime FechaVencimiento { get; set; }
}