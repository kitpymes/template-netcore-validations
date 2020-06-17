using Kitpymes.Core.Validations;
using Kitpymes.Core.Validations.Abstractions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Linq;

namespace Api.Nuget
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services
               .AddControllers()

               /*** Para lanzar la excepción cuando usamos FluentValidator ***/
               .ConfigureApiBehaviorOptions(x =>
               {
                   x.InvalidModelStateResponseFactory = context =>
                   {
                       var messages = context.ModelState
                           .Where(e => e.Value.Errors.Any())
                           .ToDictionary
                           (
                               key => key.Key,

                               value => string.Join(", ", value.Value.Errors.Select(e => e.ErrorMessage))
                           );

                       throw new ValidationsException(messages);
                   };
               });

            /*** Configuración desde el Appsetings para FluentValidator. ***/
            //services.LoadValidations(Configuration);

            /*** Configuración manual para FluentValidator. ***/
            services.LoadValidations(validator => validator.UseFluentValidator("Api.Models"));
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            /*** Agregamos el middlware para las validaciones. ***/
            app.LoadValidations();

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
