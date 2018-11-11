using AutomataNETjuegos.Contratos.Entorno;
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
        private readonly IEnumerable<IRobot> robots;
        
        private Tablero tablero;
        private IRobot robotJugado;

        public Juego2v2(
            IFabricaTablero fabricaTablero,
            IEnumerable<IRobot> robots)
        {
            this.fabricaTablero = fabricaTablero;
            this.robots = robots;
        }

        public Tablero Tablero { get; private set; }

        public void Iniciar()
        {
            this.tablero = fabricaTablero.Crear();
            this.Tablero = this.tablero;
            this.tablero.Filas.First().Casilleros.First().Robot = robots.First();
            this.tablero.Filas.Last().Casilleros.Last().Robot = robots.Last();
        }

        public bool JugarTurno()
        {
            var robot = ObtenerRobotTurnoActual();
            var accion = robot.GetAccionRobot();

            if (accion == null)
            {
                return false;
            }

            var accionMover = accion as AccionMoverDto;
            if (accionMover != null)
            {
                var direccion = accionMover.Direccion;
                var casilleroActual = ObtenerPosicion(robot);
                var nuevoCasillero = Desplazar(casilleroActual, direccion);
                nuevoCasillero.Robot = robot;
                casilleroActual.Robot = null;
                
            }

            var accionMurralla = accion as AccionConstruirDto;
            if (accionMurralla != null)
            {
                var casilleroActual = ObtenerPosicion(robot);
                casilleroActual.Muralla = robot;
            }

            robotJugado = robot;

            return false;
        }

        public IEnumerable<IRobot> GetJugadores()
        {
            return robots;
        }

        public IRobot ObtenerRobotTurnoActual()
        {
            return robots.FirstOrDefault(f => f != robotJugado);
        }

        private Casillero ObtenerPosicion(IRobot robot)
        {
            return this.tablero.Filas.SelectMany(f => f.Casilleros).FirstOrDefault(c => c.Robot == robot);
        }

        private Casillero Desplazar(Casillero casilleroOrigen, DireccionEnum movimiento)
        {
            var posFila = this.tablero.Filas.IndexOf(casilleroOrigen.Fila);
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

            var fila = this.tablero.Filas.ElementAtOrDefault(posFila);
            if (fila == null)
            {
                throw new Exception("Movimiento fuera del tablero!");
            }

            var casillero = fila.Casilleros.ElementAtOrDefault(posColumna);
            if (casillero == null)
            {
                throw new Exception("Movimiento fuera del tablero!");
            }

            if (casillero.Robot != null)
            {
                throw new Exception(string.Format("Hay un robot ocupando la posicion {0}, {1}", casillero.NroColumna, casillero.NroFila));
            }

            if (casillero.Muralla != null && casillero.Muralla != casilleroOrigen.Robot)
            {
                throw new Exception(string.Format("Hay una muralla ocupando la posicion {0}, {1}", casillero.NroColumna, casillero.NroFila));
            }

            return casillero;
        }

        
    }
}
