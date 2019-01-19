using AutomataNETjuegos.Contratos.Entorno;
using AutomataNETjuegos.Logica;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using AutoMapper;
using AutomataNETjuegos.Web.WebTools;
using System;
using System.Linq;
using AutomataNETjuegos.Robots;

namespace AutomataNETjuegos.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TableroController : Controller
    {
        private readonly IServiceProvider serviceProvider;

        public TableroController(IServiceProvider serviceProvider)
        {
            this.serviceProvider = serviceProvider;
        }

        [HttpGet("[action]")]
        public IEnumerable<Models.Tablero> GetTablero()
        {
            var juego = new Juego2v2(new FabricaTablero(), new FabricaRobot(serviceProvider));
            juego.Iniciar(new[] { typeof(RobotDefensivo), typeof(RobotDefensivo) });

            var config = new MapperConfiguration(cfg => {
                cfg.CreateMap<Tablero, Models.Tablero>();

                cfg.CreateMap<FilaTablero, Models.FilaTablero>();

                cfg.CreateMap<Casillero, Models.Casillero>()
                    .ForMember(m => m.Muralla, y => y.MapFrom(m => m.Muralla != null ? (int?)m.Muralla.GetHashCode() : null))
                    .ForMember(m => m.Robot, y => y.MapFrom(m => m.Robot != null ? (int?)m.Robot.GetHashCode() : null))
                    ;
            });

            var mapper = config.CreateMapper();

            return GetTableros(mapper, juego);
        }

        private IEnumerable<Models.Tablero> GetTableros(IMapper mapper, IJuego2v2 juego)
        {
            {
                var tablero = mapper.Map<Tablero, Models.Tablero>(juego.Tablero);
                yield return tablero;
            }
            
            while (juego.JugarTurno())
            {
                var tablero = mapper.Map<Tablero, Models.Tablero>(juego.Tablero);
                yield return tablero;
            }
        }
    }
}