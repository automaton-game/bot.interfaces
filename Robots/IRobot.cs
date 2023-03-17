using automatonGame.bot.interfaces.Entorno;

namespace automatonGame.bot.interfaces.Robots
{
    public interface IRobot
    {
        AccionRobotDto GetAccionRobot();

        Tablero Tablero { get; set; }
    }
}
