using AutomataNETjuegos.Contratos.Robots;

namespace AutomataNETjuegos.JugadorManual
{
    public class RobotDefensivo : IRobot
    {
        public AccionRobotDto GetAccionRobot()
        {
            return new AccionConstruirDto() { };
        }
    }
}
