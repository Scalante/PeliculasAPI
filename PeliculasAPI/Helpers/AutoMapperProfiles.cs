using AutoMapper;
using PeliculasAPI.Core.DTOs.Actor;
using PeliculasAPI.Core.DTOs.Genero;
using PeliculasAPI.Core.Entities;

namespace PeliculasAPI.Helpers
{
    public class AutoMapperProfiles: Profile
    {
        public AutoMapperProfiles()
        {
            #region AutoMapper para la entidad Genero
            CreateMap<Genero, GeneroDTO>().ReverseMap();
            CreateMap<GeneroCreacionDTO, Genero>();
            #endregion

            #region AutoMapper para la entidad Actor
            CreateMap<Actor, ActorDTO>().ReverseMap();
            CreateMap<ActorCreacionDTO, Actor>();
            #endregion
        }

    }
}
