
using AutomataNETjuegos.JugadorManual;
using AutomataNETjuegos.Logica;

namespace AutomataNETjuegos.Fabrica
{
    public class FabricaJuego
    {
        private readonly IRobotInput robotInput;

        public FabricaJuego(IRobotInput robotInput)
        {
            this.robotInput = robotInput;
        }

        public IJuego2v2 Crear()
        {
            var juego = new Juego2v2(new FabricaTablero(), new[] { new RobotManual(robotInput), new RobotManual(robotInput) });
            return juego;
        }
    }
}
