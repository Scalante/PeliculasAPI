using AutoMapper;
using PeliculasAPI.Core.DTOs.Genero;
using PeliculasAPI.Core.Entities;

namespace PeliculasAPI.Helpers
{
    public class AutoMapperProfiles: Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<Genero, GeneroDTO>().ReverseMap();
            CreateMap<GeneroCreacionDTO, Genero>();
        }

    }
}
