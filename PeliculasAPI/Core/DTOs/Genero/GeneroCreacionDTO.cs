using System.ComponentModel.DataAnnotations;

namespace PeliculasAPI.Core.DTOs.Genero
{
    public class GeneroCreacionDTO
    {
        [Required]
        [StringLength(40)]
        public string Nombre { get; set; }
    }
}
