using AutomataNETjuegos.Contratos.Entorno;
using AutomataNETjuegos.Contratos.Helpers;
using AutomataNETjuegos.Contratos.Robots;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AutomataNETjuegos.Logica
{
    public class Juego2v2 : IJuego2v2
    {
        private readonly IFabricaTablero fabricaTablero;
        private readonly IFabricaRobot fabricaRobot;

        private ICollection<IRobot> robots => accionesRobot.Keys;
        private IRobot robotJugado;
        private IDictionary<IRobot, List<AccionRobotDto>> accionesRobot;

        public Juego2v2(
            IFabricaTablero fabricaTablero,
            IFabricaRobot fabricaRobot
            )
        {
            this.fabricaTablero = fabricaTablero;
            this.fabricaRobot = fabricaRobot;

            this.accionesRobot = new Dictionary<IRobot, List<AccionRobotDto>>();
        }

        public Tablero Tablero { get; private set; }

        public void AgregarRobot(Type robotType)
        {
            var r = fabricaRobot.ObtenerRobot(robotType);
            this.AgregarRobot(r);
        }

        public void AgregarRobot(string robotCode)
        {
            var r = fabricaRobot.ObtenerRobot(robotCode);
            this.AgregarRobot(r);
        }

        private void AgregarRobot(IRobot robot)
        {
            this.accionesRobot.Add(robot, new List<AccionRobotDto>());

            if (this.Tablero == null)
            {
                this.Tablero = fabricaTablero.Crear();
            }

            robot.Tablero = this.Tablero;

            switch (this.robots.Count)
            {
                case 1:
                    this.Tablero.Filas.First().Casilleros.First().AgregarRobot(robot);
                    break;

                case 2:
                    this.Tablero.Filas.Last().Casilleros.Last().AgregarRobot(robot);
                    break;

                case 3:
                    this.Tablero.Filas.Last().Casilleros.First().AgregarRobot(robot);
                    break;

                case 4:
                    this.Tablero.Filas.First().Casilleros.Last().AgregarRobot(robot);
                    break;
            }
        }

        public bool JugarTurno()
        {
            var robot = ObtenerRobotTurnoActual();
            var accion = robot.GetAccionRobot();

            if (accion == null)
            {
                return false;
            }

            this.accionesRobot[robot].Add(accion);

            // Valido que haya construido dentro de las ultimas aciones
            var movimientosSinConstruccion = this.accionesRobot[robot].Reverse<AccionRobotDto>().TakeWhile(a => a is AccionMoverDto).Count();
            if(movimientosSinConstruccion > Tablero.Filas.Count * 2)
            {
                return false;
            }

            var accionMover = accion as AccionMoverDto;
            if (accionMover != null)
            {
                var direccion = accionMover.Direccion;
                var casilleroActual = ObtenerPosicion(robot);
                var nuevoCasillero = Desplazar(casilleroActual, direccion);
                nuevoCasillero.AgregarRobot(robot);
                casilleroActual.QuitarRobot(robot);
                
            }

            var accionMurralla = accion as AccionConstruirDto;
            if (accionMurralla != null)
            {
                var casilleroActual = ObtenerPosicion(robot);
                casilleroActual.Muralla = robot;
            }

            robotJugado = robot;

            return true;
        }

        public IEnumerable<IRobot> GetJugadores()
        {
            return robots;
        }

        public IRobot ObtenerRobotTurnoActual()
        {
            return this.accionesRobot.OrderBy(d => d.Value.Count).Select(d => d.Key).First();
        }

        private Casillero ObtenerPosicion(IRobot robot)
        {
            return this.Tablero.GetPosition(robot);
        }

        private Casillero Desplazar(Casillero casilleroOrigen, DireccionEnum movimiento)
        {
            var posFila = this.Tablero.Filas.IndexOf(casilleroOrigen.Fila);
            var posColumna = casilleroOrigen.Fila.Casilleros.IndexOf(casilleroOrigen);

            switch (movimiento)
            {
                case DireccionEnum.Arriba:
                    posFila--;
                    break;
                case DireccionEnum.Abajo:
                    posFila++;
                    break;
                case DireccionEnum.Izquierda:
                    posColumna--;
                    break;
                case DireccionEnum.Derecha:
                    posColumna++;
                    break;
                default:
                    break;
            }

            var fila = this.Tablero.Filas.ElementAtOrDefault(posFila);
            if (fila == null)
            {
                throw new Exception("Movimiento fuera del tablero!");
            }

            var casillero = fila.Casilleros.ElementAtOrDefault(posColumna);
            if (casillero == null)
            {
                throw new Exception("Movimiento fuera del tablero!");
            }

            if (casillero.Robots != null)
            {
                throw new Exception(string.Format("Hay un robot ocupando la posicion {0}, {1}", casillero.NroColumna, casillero.NroFila));
            }

            if (casillero.Muralla != null && casillero.Muralla != casilleroOrigen.Robots)
            {
                throw new Exception(string.Format("Hay una muralla ocupando la posicion {0}, {1}", casillero.NroColumna, casillero.NroFila));
            }

            return casillero;
        }

        
    }
}
