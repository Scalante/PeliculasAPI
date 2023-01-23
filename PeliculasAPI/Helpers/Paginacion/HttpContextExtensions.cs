using Microsoft.EntityFrameworkCore;

namespace PeliculasAPI.Helpers.Paginacion
{
    public static class HttpContextExtensions
    {

        public async static Task InsertarParametrosPaginacion<T>(this HttpContext httpContext, 
            IQueryable<T> queryable, int cantidadRegistrosPagina)
        {
            double cantidad = await queryable.CountAsync();
            double cantidadPaginas = Math.Ceiling(cantidad / cantidadRegistrosPagina);
            httpContext.Response.Headers.Add("cantidadPaginas", cantidadPaginas.ToString());
        }


    }
}
