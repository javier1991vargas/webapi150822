using AutoMapper;
using WebApAutores.DTOs;
using WebApAutores.Entidades;

namespace WebApAutores.Utilidades
{
    public class AutoMaperProfiles :Profile
    {
        public AutoMaperProfiles()
        {
            //desde donde queremos mapear-->(AutorCreacionDTOs) y el destino-->(Autor)
            CreateMap<AutorCreacionDTOs, Autor>();
            CreateMap<Autor, AutorDTO>();
            CreateMap<LibroCreacionDTO, Libro>()
                .ForMember(libro => libro.autoresLibros, opciones => opciones.MapFrom(MapAutoresLibros));

            CreateMap<Libro, LibroDTO>()
                .ForMember(libroDTO=> libroDTO.Aut_ores, opcion=> opcion.MapFrom(mapLibroDtoAutores)) ;
            CreateMap<ComentariosCreacionDTO,Comentarios>();
            CreateMap<Comentarios, ComentarioDTO>().ReverseMap();
            CreateMap<LibropatchDto, Libro>().ReverseMap();
         }

        private List<AutorDTO> mapLibroDtoAutores(Libro libro, LibroDTO libroDTO)
        {
            var resultado = new List<AutorDTO>();

            if (libro.autoresLibros==null)
            {
                return resultado;
            }

            foreach (var autorlibro in libro.autoresLibros)
            {
                resultado.Add(new AutorDTO()
                { 
                Id=autorlibro.AutorId,
                Nombre=autorlibro.Autor.Nombre
                });

            }

            return resultado;
        }
        

        

        private List<AutoresLibros> MapAutoresLibros(LibroCreacionDTO libroCreacionDTO, Libro libro)
        {
            var resultado = new List<AutoresLibros>();

            if(libroCreacionDTO.Autoreids== null) { return resultado; } 

            foreach (var autorid  in libroCreacionDTO.Autoreids)
            {
                resultado.Add(new AutoresLibros() { AutorId = autorid });
            }

            return resultado;
        }
    }
}
