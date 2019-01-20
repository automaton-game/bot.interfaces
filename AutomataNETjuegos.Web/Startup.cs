using AutoMapper;
using AutomataNETjuegos.Contratos.Entorno;
using AutomataNETjuegos.Logica;
using AutomataNETjuegos.Web.WebTools;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SpaServices.AngularCli;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace AutomataNETjuegos.Web
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            // In production, the Angular files will be served from this directory
            services.AddSpaStaticFiles(configuration =>
            {
                configuration.RootPath = "ClientApp/dist";
            });

            services.AddTransient(p => {
                var config = new MapperConfiguration(cfg => {
                    cfg.CreateMap<Tablero, Models.Tablero>();

                    cfg.CreateMap<FilaTablero, Models.FilaTablero>();

                    cfg.CreateMap<Casillero, Models.Casillero>()
                        .ForMember(m => m.Muralla, y => y.MapFrom(m => m.Muralla != null ? (int?)m.Muralla.GetHashCode() : null))
                        .ForMember(m => m.Robot, y => y.MapFrom(m => m.Robot != null ? (int?)m.Robot.GetHashCode() : null))
                        ;
                });

                var mapper = config.CreateMapper();
                return mapper;
            });

            services.AddTransient<Juego2v2>();
            services.AddTransient<IFabricaTablero, FabricaTablero>();
            services.AddTransient<IFabricaRobot, FabricaRobot>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
            }

            app.UseStaticFiles();
            app.UseSpaStaticFiles();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller}/{action=Index}/{id?}");
            });

            app.UseSpa(spa =>
            {
                // To learn more about options for serving an Angular SPA from ASP.NET Core,
                // see https://go.microsoft.com/fwlink/?linkid=864501

                spa.Options.SourcePath = "ClientApp";

                if (env.IsDevelopment())
                {
                    spa.UseAngularCliServer(npmScript: "start");
                }
            });
        }
    }
}
