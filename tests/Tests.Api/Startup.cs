using Kitpymes.Core.Validations;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Tests.Api
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
            services.AddMvc();

            /*** Configuración desde el AppSettings para FluentValidator. ***/
            services.LoadValidations(Configuration);

            /*** Configuración manual para FluentValidator. ***/
            // services.LoadValidations(validator => validator.WithEnabled().WithFluentValidation("App.Models"));
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
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