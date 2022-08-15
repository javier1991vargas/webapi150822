//using Microsoft.AspNetCore.Authentication.JwtBearer;
//using Microsoft.AspNetCore.Authorization;
//using Microsoft.AspNetCore.Mvc;
//using WebApAutores.DTOs;

//namespace WebApAutores.Controllers
//{
//    [ApiController]
//    [Route("api")]
//    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
//    public class RootController : ControllerBase
//    {
//        private readonly IAuthorizationService authorizationService;

//        public RootController(IAuthorizationService authorizationService)
//        {
//            this.authorizationService = authorizationService;
//        }
//        [HttpGet(Name = "obtenerRoot")]
//        [AllowAnonymous]
//        public async Task<ActionResult<IEnumerable<DatosHATEOS>>> Get()
//        {
//            var datoshateos = new List<DatosHATEOS>();

//            var esAdmin = await authorizationService.AuthorizeAsync(User "esAdmin");

//            datoshateos.Add(new DatosHATEOS(enlace: Url.Link("obtenerRoot", new { }), descripcion: "self", metodo: "get"));

//            datoshateos.Add(new DatosHATEOS(enlace: Url.Link("obtenerAutores", new { }), descripcion: "autores", metodo: "GET"));
//            datoshateos.Add(new DatosHATEOS(enlace: Url.Link("crearAutor", new { }), descripcion: "autor_crear", metodo: "Post"));

//            datoshateos.Add(new DatosHATEOS(enlace: Url.Link("crearLibro", new { }), descripcion: "libro_crear", metodo: "Post"));

//            return datoshateos;
//        }
//    }
//}
