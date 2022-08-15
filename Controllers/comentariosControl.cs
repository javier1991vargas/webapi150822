using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using WebApAutores.DTOs;
using WebApAutores.Entidades;

namespace WebApAutores.Controllers
{
    [ApiController]
    [Route("api/libros/{libroId:int}/comentarios")]
    public class comentariosControl:ControllerBase
    {
             

        private readonly ApplicationDbContext context;
        private readonly IMapper mapper;
        private readonly UserManager<IdentityUser> userManager;

        public comentariosControl(ApplicationDbContext context, IMapper mapper, UserManager<IdentityUser> userManager)
        {
            this.context = context;
            this.mapper = mapper;
            this.userManager = userManager;
        }
        [HttpGet(Name ="obtenerComentariosLibros")]
        public  async Task<ActionResult<List<ComentarioDTO>>> get(int libroId)
        {
            var comentario = await context.Comentarios.Where(x => x.LibroID == libroId).ToListAsync();
            return mapper.Map<List<ComentarioDTO>>(comentario);
        }

        [HttpPost(Name = "crearComentario")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<ActionResult> Post(int libroId, ComentariosCreacionDTO _comentariosCreacionDTO)
        {
            var EmailClaim = HttpContext.User.Claims.Where(claim => claim.Type == "email").FirstOrDefault();
            var email = EmailClaim.Value;
            var usuario = await userManager.FindByEmailAsync(email);
            var usuarioid = usuario.Id;
            var existe = await context.Libros.AnyAsync(x => x.Id == libroId);

            if (!existe)
            {
                return NotFound();
            }
            var comentario_ = mapper.Map<Comentarios>(_comentariosCreacionDTO);
            comentario_.LibroID = libroId;
            comentario_.UsuarioId = usuarioid;
            context.Add(comentario_);
            await context.SaveChangesAsync();
            return Ok();

        }



        [HttpPut(Name = "actualizarComentario")]
       
        public async Task<ActionResult> put(int _libroID, int id, ComentariosCreacionDTO comentariosCreacionDTO)
        {
           
            var existeLibro = await context.Libros.AnyAsync(x => x.Id == _libroID);

            if (!existeLibro)
            {
                return NotFound();
            }

            var existeComentario = await context.Comentarios.AnyAsync(x => x.Id == id);
            if (!existeComentario)
            {
                return NotFound();
            }

            var comentario = mapper.Map<Comentarios>(comentariosCreacionDTO);
            comentario.Id = id;
            comentario.LibroID = _libroID;
            context.Update(comentario);
            await context.SaveChangesAsync();
            return NoContent();


        }

    }
}
