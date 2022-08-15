using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using WebApAutores.DTOs;
using WebApAutores.Entidades;

namespace WebApAutores.Controllers
{
    [ApiController]
    [Route("Api/Autores")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy ="esAdmin")]
    public class AutoresController : ControllerBase
    {
        private readonly ApplicationDbContext Context;
        private readonly IMapper mapper;

        public AutoresController(ApplicationDbContext Context, IMapper mapper)
        {
            this.Context = Context;
            this.mapper = mapper;
        }

        [HttpGet(Name ="obtenerAutores")]
        //permite acceder sin ser autorizado (Excepcion para que usuarios no rgistrado puedan acceder)
        [AllowAnonymous]
        public async Task<List<AutorDTO>> Get()
        {
         var autores= await Context.Autores.ToListAsync();
            return mapper.Map<List<AutorDTO>>(autores);
        }

        [HttpGet("{id:int}", Name = "obtenerAutores")]
        public async Task<ActionResult<AutorDTO>> Get(int id)
        {

            var autores = await Context.Autores.FirstOrDefaultAsync(autorBD=> autorBD.Id==id);
            if (autores == null)
            {
                return NotFound();
            }
            return mapper.Map<AutorDTO>(autores);

        
        }
        [HttpGet("{nombre}", Name = "obtenerAutoresPorNombre")]
        public async Task<ActionResult<List<AutorDTO>>> Get(string nombre)
        {
            var autor = await Context.Autores.Where(AutorBD => AutorBD.Nombre.Contains(nombre)).ToListAsync();
            return mapper.Map<List<AutorDTO>>(autor);
        }

        
        [HttpPost(Name = "crearAutor")]
        public async Task<ActionResult<Autor>> Post(AutorCreacionDTOs autorCreacionDTOs)
        {
            var autorNombre = await Context.Autores.AnyAsync(x => x.Nombre == autorCreacionDTOs.Nombre);
            if (autorNombre)
            {
                return BadRequest($"Ya existe con el mismo nombre{autorCreacionDTOs.Nombre}");
            }

            var autor = mapper.Map<Autor>(autorCreacionDTOs);
            Context.Add(autor);
            await Context.SaveChangesAsync();
            return Ok();
        }
                

        [HttpPut("{id:int}", Name = "actualizarAutor")]
        //ACTUALIZAMOS REGISTRO DE LA BASE DE DATOS
        public async Task<ActionResult> Put(AutorCreacionDTOs autorcreaciondto, int id)
        {
            var existe = await Context.Autores.AnyAsync(x=> x.Id==id);

            if (!existe)
            {
                return BadRequest("no exixte Autor");
            }
            var autor=mapper.Map<Autor>(autorcreaciondto);
            autor.Id = id;
            Context.Update(autor);
            await Context.SaveChangesAsync();
            return Ok();
        }

        [HttpDelete("{id:int}", Name = "borrarAutor")]
        public async Task<ActionResult> Delete(int id)
        {
            var existeA = await Context.Autores.AnyAsync(x=> x.Id==id);
            if (!existeA)
            {
                return NotFound();
            }

            Context.Remove(new Autor() { Id = id });
            await Context.SaveChangesAsync();
            return Ok();
        }

    }
}
