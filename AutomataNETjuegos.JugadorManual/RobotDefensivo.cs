using AutomataNETjuegos.Contratos.Entorno;
using AutomataNETjuegos.Contratos.Robots;
using System.Linq;

namespace AutomataNETjuegos.JugadorManual
{
    public class RobotDefensivo : IRobot
    {
        public Tablero Tablero { get; set; }

        public AccionRobotDto GetAccionRobot()
        {
            
            return new AccionConstruirDto() { };
        }
    }
}
