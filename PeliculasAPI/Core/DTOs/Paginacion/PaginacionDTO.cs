namespace PeliculasAPI.Core.DTOs.Paginacion
{
    public class PaginacionDTO
    {
        public int Pagina { get; set; } = 1;

        private int cantidadRegistrosPagina = 10;
        private readonly int cantidadMaximaRegistrosPagina = 50;

        public int CantidadRegistrosPagina
        {
            get => cantidadRegistrosPagina;
            set
            {
                cantidadRegistrosPagina = (value > cantidadMaximaRegistrosPagina) ? cantidadMaximaRegistrosPagina : value;
            }
        }
    }
}
