using Microsoft.AspNetCore.Mvc;
using PeliculasAPI.Core.DTOs.ActorPelicula;
using PeliculasAPI.Helpers.ModelBinder;
using PeliculasAPI.Helpers.Validations;
using System.ComponentModel.DataAnnotations;

namespace PeliculasAPI.Core.DTOs.Pelicula
{
    public class PeliculaCreacionDTO: PeliculaPatchDTO
    {
        [PesoArchivoValidacion(pesoMaximoMegaBytes: 4)]
        [TipoArchivoValidacion(GrupoTipoArchivo.Imagen)]
        public IFormFile Poster { get; set; }

        [ModelBinder(BinderType = typeof(TypeBinder<List<int>>))]
        public List<int> GenerosIDs { get; set; }

        [ModelBinder(BinderType = typeof(TypeBinder<List<ActorPeliculaCreacionDTO>>))]
        public List<ActorPeliculaCreacionDTO> Actores { get; set; }
    }
}
