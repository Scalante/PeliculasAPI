using PeliculasAPI.Helpers.Validations;
using System.ComponentModel.DataAnnotations;

namespace PeliculasAPI.Core.DTOs.Actor
{
    public class ActorCreacionDTO: ActorPatchDTO
    {
        [PesoArchivoValidacion(pesoMaximoMegaBytes: 4)]
        [TipoArchivoValidacion(grupoTipoArchivo:GrupoTipoArchivo.Imagen)]
        public IFormFile Foto { get; set; }
    }
}
