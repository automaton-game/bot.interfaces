using AutomataNETjuegos.Logica;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using AutoMapper;
using AutomataNETjuegos.Robots;
using AutomataNETjuegos.Web.Models;
using Tablero = AutomataNETjuegos.Contratos.Entorno.Tablero;
using AutomataNETjuegos.Compilador.Excepciones;
using Microsoft.Extensions.Logging;
using System.Linq;
using AutomataNETjuegos.Web.Logica;

namespace AutomataNETjuegos.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TableroController : Controller
    {
        private readonly IJuego2v2 juego;
        private readonly IMapper mapper;
        private readonly ILogger logger;
        private readonly IRegistroRobots registroRobots;

        private string motivo;

        public TableroController(
            IJuego2v2 juego,
            IMapper mapper,
            ILogger<TableroController> logger,
            IRegistroRobots registroRobots)
        {
            this.juego = juego;
            this.mapper = mapper;
            this.logger = logger;
            this.registroRobots = registroRobots;
        }

        [HttpGet("[action]")]
        public IEnumerable<Models.Tablero> GetTablero()
        {
            var jugador = typeof(RobotDefensivo);
            juego.AgregarRobot(jugador);
            juego.AgregarRobot(jugador);

            return GetTableros();
        }

        [HttpPost("[action]")]
        public JuegoResponse GetTablero(TableroRequest tableroRequest)
        {
            var tipo = juego.AgregarRobot(tableroRequest.LogicaRobot);
            registroRobots.Registrar(tipo.Name, tableroRequest.LogicaRobot);
            
            var ultimoCampeon = registroRobots.ObtenerUltimoCampeon();
            if (ultimoCampeon != null)
            {
                juego.AgregarRobot(ultimoCampeon);
            }
            else
            {
                var jugador = typeof(RobotDefensivo);
                juego.AgregarRobot(jugador);
            }

            var tableros = GetTableros().ToArray();
            var usuarioGanador = juego.ObtenerUsuarioGanador();
            registroRobots.RegistrarVictoria(usuarioGanador);

            return new JuegoResponse { Tableros = tableros, Ganador = usuarioGanador, MotivoDerrota = this.motivo };
        }

        private IEnumerable<Models.Tablero> GetTableros()
        {
            {
                var tablero = mapper.Map<Tablero, Models.Tablero>(juego.Tablero);
                yield return tablero;
            }
            
            while (JugarTurno())
            {
                var tablero = mapper.Map<Tablero, Models.Tablero>(juego.Tablero);
                yield return tablero;
            }
        }

        private bool JugarTurno()
        {
            var motivo = juego.JugarTurno();
            this.motivo = motivo;
            return motivo == null;
        }
    }
}