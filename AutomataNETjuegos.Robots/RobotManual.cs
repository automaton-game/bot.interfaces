using AutomataNETjuegos.Contratos.Entorno;
using AutomataNETjuegos.Contratos.Robots;

namespace AutomataNETjuegos.Robots
{
    public class RobotManual : IRobot
    {
        public Tablero Tablero { get; set; }

        public AccionRobotDto AccionRobot { get; set; }

        public AccionRobotDto GetAccionRobot()
        {
            return AccionRobot;
        }
    }
}
