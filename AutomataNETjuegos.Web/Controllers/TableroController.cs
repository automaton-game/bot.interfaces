using AutomataNETjuegos.Logica;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using AutoMapper;
using AutomataNETjuegos.Robots;
using AutomataNETjuegos.Web.Models;
using Tablero = AutomataNETjuegos.Contratos.Entorno.Tablero;
using AutomataNETjuegos.Compilador.Excepciones;
using Microsoft.Extensions.Logging;

namespace AutomataNETjuegos.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TableroController : Controller
    {
        private readonly Juego2v2 juego;
        private readonly IMapper mapper;
        private readonly ILogger logger;

        public TableroController(Juego2v2 juego, IMapper mapper, ILogger<TableroController> logger)
        {
            this.juego = juego;
            this.mapper = mapper;
            this.logger = logger;
        }

        [HttpGet("[action]")]
        public IEnumerable<Models.Tablero> GetTablero()
        {
            //var juego = new Juego2v2(new FabricaTablero(), new FabricaRobot());
            juego.Iniciar(new[] { typeof(RobotDefensivo), typeof(RobotDefensivo) });

            return GetTableros();
        }

        [HttpPost("[action]")]
        public IEnumerable<Models.Tablero> GetTablero(TableroRequest tableroRequest)
        {
            juego.Iniciar(tableroRequest.LogicasRobot);
            return GetTableros();
        }

        private IEnumerable<Models.Tablero> GetTableros()
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