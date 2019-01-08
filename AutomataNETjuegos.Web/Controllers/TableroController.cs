using AutomataNETjuegos.Contratos.Entorno;
using AutomataNETjuegos.Logica;
using Microsoft.AspNetCore.Mvc;
using AutomataNETjuegos.JugadorManual;
using System.Collections.Generic;
using AutoMapper;

namespace AutomataNETjuegos.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TableroController : Controller
    {
        [HttpGet("[action]")]
        public IEnumerable<Models.Tablero> GetTablero()
        {
            var juego = new Juego2v2(new FabricaTablero(), new[] { new RobotDefensivo(), new RobotDefensivo() });
            juego.Iniciar();

            var config = new MapperConfiguration(cfg => {
                cfg.CreateMap<Tablero, Models.Tablero>();
            });

            var mapper = config.CreateMapper();

            var tablero = mapper.Map<Tablero, Models.Tablero>(juego.Tablero);

            return new[] { tablero };
        }
    }
}