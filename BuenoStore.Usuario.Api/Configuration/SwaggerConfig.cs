using Microsoft.OpenApi.Models;

namespace BuenoStore.Usuario.Api.Configuration
{
    public static class SwaggerConfig
    {
        public static IServiceCollection AddSwaggerConfiguration(this IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo()
                {
                    Title = "Bueno Store Usuário API",
                    Description = "API referente ao serviço de usuário do sistema Bueno Store",
                    Contact = new OpenApiContact() { Name = "Alexandre Bueno", Email = "alexandrebueno@outlook.com" }
                });

            });

            return services;
        }

        public static IApplicationBuilder UseSwaggerConfiguration(this IApplicationBuilder app)
        {
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
            });

            return app;
        }
    }
}
