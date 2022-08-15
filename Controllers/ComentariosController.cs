//using AutoMapper;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.EntityFrameworkCore;
//using WebApAutores.DTOs;
//using WebApAutores.Entidades;

//namespace WebApAutores.Controllers
//{
//    [ApiController]
//    [Route("api/libros/{libroId:int}/comentarios")]
//    public class ComentariosController : ControllerBase
//    {
//        private readonly ApplicationDbContext context;
//        private readonly IMapper mapper;

//        public ComentariosController(ApplicationDbContext context, IMapper mapper)
//        {
//            this.context = context;
//            this.mapper = mapper;
//        }

//        //[HttpGet]
//        //public async Task<ActionResult<List<ComentarioDTO>>> get(int libroid)
//        //{
//        //    var existe = await context.Libros.AnyAsync(x => x.Id == libroid);

//        //    if (!existe)
//        //    {
//        //        return NotFound();
//        //    }
//        //    var comentario = await context.Comentarios
//        //        .Where(x => x.LibroID == libroid).ToListAsync();

//        //    return mapper.Map<List<ComentarioDTO>>(comentario);
//        //}

//        [HttpGet]
//        public async Task<ActionResult<List<ComentarioDTO>>> get2(int libroid)
//        {
//            var comentario = await context.Comentarios.Where(x => x.LibroID == libroid).ToListAsync();

//            return mapper.Map<List<ComentarioDTO>>(comentario);
//        }





//        [HttpPost]
//        public async Task<ActionResult> Post(int libroId, ComentariosCreacionDTO _comentariosCreacionDTO)
//        {
//            var existe = await context.Libros.AnyAsync(x => x.Id == libroId);

//            if (!existe)
//            {
//                return NotFound();
//            }
//            var comentario_ = mapper.Map<Comentarios>(_comentariosCreacionDTO);
//            comentario_.LibroID = libroId;
//            context.Add(comentario_);
//            await context.SaveChangesAsync();
//            return Ok();

//        }

                

//        [HttpPut]
//        public async Task<ActionResult> put (int _libroID, int id, ComentariosCreacionDTO comentariosCreacionDTO)
//        {
//            var existeLibro = await context.Libros.AnyAsync(x => x.Id == _libroID);

//            if (!existeLibro)
//            {
//                return NotFound();
//            }

//            var existeComentario = await context.Comentarios.AnyAsync(x => x.Id == id);
//            if (!existeComentario)
//            {
//                return NotFound();
//            }

//            var comentario = mapper.Map<Comentarios>(comentariosCreacionDTO);
//            comentario.Id = id;
//            comentario.LibroID = _libroID;
//            context.Update(comentario);
//            await context.SaveChangesAsync();
//            return NoContent();


//        }

//    }
//}

