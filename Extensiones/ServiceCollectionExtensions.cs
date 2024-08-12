using FluentValidation.AspNetCore;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Tienda.Microservicio.Libro.Aplicacion;
using Tienda.Microservicio.Libro.Persistencia;

namespace Tienda.Microservicio.Libro.Extensiones
{
    public  static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddCustomService(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddControllers()
                .AddFluentValidation(cfg => cfg.RegisterValidatorsFromAssemblyContaining<Nuevo>());

            services.AddDbContext<ContextoLibreria>(options =>
            {
                options.UseNpgsql(configuration.GetConnectionString("DefaultConnection"));
            });


            services.AddCors(options =>
            {
                options.AddPolicy("AlowSpecificOrigins",
                    builder =>
                    {
                        builder.WithOrigins("http://localhost:4200", "localhost:4200")
                               .AllowAnyHeader()
                               .AllowAnyMethod()
                               .AllowCredentials();
                    });

                options.AddPolicy("AllowAll",
                    builder =>
                    {
                        builder.AllowAnyOrigin()
                               .AllowAnyHeader()
                               .AllowAnyMethod();
                    });
            });


            services.AddMediatR(typeof(Nuevo.Ejecuta).Assembly);
            services.AddAutoMapper(typeof(Consulta.Manejador));


            return services;
        }

    }
}
