using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using PeliculasAPI.Core.Interfaces.AzureStorageAccount;
using PeliculasAPI.Infrastructure.Context;
using PeliculasAPI.Infrastructure.Repositories.AzureStorageAccount;
using System.Reflection;

namespace PeliculasAPI
{
    public class Startup
    {
        public static IConfiguration Configuration { get; set; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            //Registro del DbContext
            services.AddDbContext<ApplicationDbContext>(options =>
            options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            //Registro de AutoMapper
            services.AddAutoMapper(typeof(Startup));

            services.AddTransient<IAlmacenadorArchivos, AlmacenadorArchivos>();

            //Permite evitar los ciclos infinitos en las relaciones de clases
            //services.AddControllers().AddJsonOptions(x => x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);

            //No muestra en la respuesta de la API los atributos que tienen como valor NULL, esto funciona con Controllers
            services.AddControllers().AddJsonOptions(opciones => opciones.JsonSerializerOptions.DefaultIgnoreCondition
                            = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingNull);


            //Configuración de Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen(c =>
            {
                {
                    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                    c.IncludeXmlComments(xmlPath);
                }
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Peliculas API", Version = "v1" });
            });

            services.AddCors(options =>
            {
                options.AddPolicy("CorsRule", rule => {
                    rule.AllowAnyHeader().AllowAnyMethod().WithOrigins("*");
                });
            });
        }



        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            // Configure the HTTP request pipeline.
            if (env.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseCors("CorsRule");


            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
