﻿using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PeliculasAPI.Core.DTOs.Actor;
using PeliculasAPI.Core.DTOs.Genero;
using PeliculasAPI.Core.Entities;
using PeliculasAPI.Core.Interfaces.AzureStorageAccount;
using PeliculasAPI.Infrastructure.Context;

namespace PeliculasAPI.Controllers
{
    [ApiController]
    [Route("api/actores")]
    public class ActorController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly IAlmacenadorArchivos _almacenadorArchivos;
        //Nombre de la carpeta que se creará en Azure Storage
        private readonly string _contenedor = "actores";

        public ActorController(ApplicationDbContext context, IMapper mapper, IAlmacenadorArchivos almacenadorArchivos)
        {
            this._context = context;
            this._mapper = mapper;
            this._almacenadorArchivos = almacenadorArchivos;
        }

        [HttpGet]
        public async Task<ActionResult<List<ActorDTO>>> Get()
        {
            var entidades = await _context.Actores.ToListAsync();
            return _mapper.Map<List<ActorDTO>>(entidades);
        }


        [HttpGet("{id:int}", Name = "obtenerActor")]
        public async Task<ActionResult<ActorDTO>> Get(int id)
        {
            var entidad = await _context.Generos.FirstOrDefaultAsync(x => x.Id == id);

            if (entidad == null)
            {
                return NotFound();
            }
            return _mapper.Map<ActorDTO>(entidad);
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromForm] ActorCreacionDTO actorCreacionDTO)
        {
            var entidad = _mapper.Map<Actor>(actorCreacionDTO);

            if (actorCreacionDTO.Foto != null)
            {
                using (var memoryStream = new MemoryStream())
                {
                    await actorCreacionDTO.Foto.CopyToAsync(memoryStream);
                    var contenido = memoryStream.ToArray();
                    var extension = Path.GetExtension(actorCreacionDTO.Foto.FileName);
                    var contentType = actorCreacionDTO.Foto.ContentType;
                    entidad.Foto = await this._almacenadorArchivos.GuardarArchivo(contenido, extension, this._contenedor, contentType);
                }
            }

            _context.Add(entidad);
            await _context.SaveChangesAsync();
            var dto = _mapper.Map<ActorDTO>(entidad);
            return new CreatedAtRouteResult("obtenerActor", new { id = entidad.Id }, dto);
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult> Put(int id, [FromForm] ActorCreacionDTO actorCreacionDTO)
        {
            var actorDB = await _context.Actores.FirstOrDefaultAsync(x => x.Id == id);

            if (actorDB == null)
            {
                return NotFound();
            }

            actorDB = this._mapper.Map(actorCreacionDTO, actorDB);

            if (actorCreacionDTO.Foto != null)
            {
                using (var memoryStream = new MemoryStream())
                {
                    await actorCreacionDTO.Foto.CopyToAsync(memoryStream);
                    var contenido = memoryStream.ToArray();
                    var extension = Path.GetExtension(actorCreacionDTO.Foto.FileName);
                    var rutaActualFoto = actorDB.Foto;
                    var contentType = actorCreacionDTO.Foto.ContentType;
                    actorDB.Foto = await this._almacenadorArchivos.EditarArchivo(contenido, extension, this._contenedor, rutaActualFoto, contentType);
                }
            }

            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete(int id)
        {
            var existe = await _context.Generos.AnyAsync(x => x.Id == id);

            if (!existe)
            {
                return NotFound();
            }
            _context.Remove(new Actor() { Id = id });
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
