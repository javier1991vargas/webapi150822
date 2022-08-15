using AutoMapper;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApAutores.DTOs;
using WebApAutores.Entidades;

namespace WebApAutores.Controllers
{
    [ApiController]
    [Route("api/libros")]
    public class LibrosController : ControllerBase
    {
        private readonly ApplicationDbContext context;
        private readonly IMapper mapper;

        public LibrosController(ApplicationDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }


        [HttpGet]
        public async Task<ActionResult<List<LibroDTO>>> Get()
        {
            var libro = await context.Libros.Include(x=> x.comentarios).ToListAsync();
            return mapper.Map<List<LibroDTO>>(libro);
        }


        [HttpGet("{id:int}")]
        public async Task<ActionResult<LibroDTO>> Get(int id)
        {
            var libro = await context.Libros
                 .Include(basedb => basedb.autoresLibros).ThenInclude(basedb => basedb.Autor).FirstOrDefaultAsync(x => x.Id == id);

            libro.autoresLibros = libro.autoresLibros.OrderBy(x => x.Orden).ToList();
            return mapper.Map<LibroDTO>(libro);
        }


        [HttpGet("{nombre}")]
        public async Task<ActionResult<List<LibroDTO>>> get(string nombre)
        {
            var libro = await context.Libros.Where(x => x.Titulo.Contains(nombre)).ToListAsync();
            return mapper.Map<List<LibroDTO>>(libro);
        }

        [HttpPost(Name = "crearLibro")]
        public async Task<ActionResult> Post(LibroCreacionDTO libroCreacionDTO)
        {
            if (libroCreacionDTO.Autoreids == null)
            {
                return BadRequest("No se puede crear un libro sin autores...");
            }

            var autoreid = await context.Autores
                .Where(baseDd => libroCreacionDTO.Autoreids.Contains(baseDd.Id)).Select(x => x.Id).ToListAsync();

            if (libroCreacionDTO.Autoreids.Count != autoreid.Count)
            {
                return BadRequest("No xiste uno de los autores enviados:  ");
            }

            var libro = mapper.Map<Libro>(libroCreacionDTO);
            AsignarOrdenAutores(libro);

            context.Add(libro);
            await context.SaveChangesAsync();
            return Ok();

        }
        [HttpPut("{id:int}", Name = "actualizarLibro")]
        public async Task<ActionResult> put(int id, LibroCreacionDTO libroCreacionDTO)
        {
            var libroDB = await context.Libros.Include(x => x.autoresLibros).FirstOrDefaultAsync(x => x.Id == id);

            if (libroDB == null)
            {
                return NotFound();
            }

            libroDB = mapper.Map(libroCreacionDTO, libroDB);
            AsignarOrdenAutores(libroDB);
            await context.SaveChangesAsync();
            return NoContent();

        }

        private void AsignarOrdenAutores(Libro libro)
        {
            if (libro.autoresLibros != null)
            {
                for (int i = 0; i < libro.autoresLibros.Count; i++)
                {
                    libro.autoresLibros[i].Orden = i;
                }
            }
        }
        [HttpPatch(Name = "pathLibro")]
        public async Task<ActionResult> patch(int id,JsonPatchDocument<LibropatchDto> patchDocument)
        {
            if (patchDocument==null)
            {
                return BadRequest();
            }

            var librodb = await context.Libros.FirstOrDefaultAsync(x=> x.Id==id);
            if (librodb == null)
            {
                return NotFound();
            }
            // llenamos el libropatchdto con la base de datos de librop
            var librodto = mapper.Map<LibropatchDto>(librodb);
            
            //aplicamos a dicho librodto los cambios que vinieron del  patchdocument("titulo o fecha..etc")
            patchDocument.ApplyTo(librodto,ModelState);

            // ahora verificamos que las reglas de verificacion se esten cumpliendo

            var esValido = TryValidateModel(librodto);

            if (!esValido)
            {
                // en el modelsatte estan los errores de validacion encontrados
                return BadRequest(ModelState);
            }

            // ahora mapeamos de libropatchdto hacia librodb
            mapper.Map(librodto,librodb);
            await context.SaveChangesAsync();
            return NoContent();
            
        }

        [HttpDelete("{id:int}",Name = "eliminarLibro")]
        public async Task<ActionResult> Delete(int id)
        {
            var existeA = await context.Libros.AnyAsync(x => x.Id == id);
            if (!existeA)
            {
                return NotFound();
            }

            context.Remove(new Autor() { Id = id });
            await context.SaveChangesAsync();
            return Ok();
        }

    }
}
