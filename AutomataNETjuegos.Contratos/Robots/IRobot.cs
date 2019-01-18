using AutomataNETjuegos.Contratos.Entorno;

namespace AutomataNETjuegos.Contratos.Robots
{
    public interface IRobot
    {
        AccionRobotDto GetAccionRobot();

        Tablero Tablero { get; set; }
    }
}
