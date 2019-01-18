using AutomataNETjuegos.Contratos.Entorno;
using AutomataNETjuegos.Contratos.Robots;

namespace AutomataNETjuegos.JugadorManual
{
    public class RobotManual : IRobot
    {
        private readonly IRobotInput robotInput;

        public RobotManual(IRobotInput robotInput)
        {
            this.robotInput = robotInput;
        }

        public Tablero Tablero { get; set; }

        public AccionRobotDto GetAccionRobot()
        {
            var input = robotInput.Leer();
            switch (input)
            {
                case "U":
                    return new AccionMoverDto { Direccion = DireccionEnum.Arriba };
                case "D":
                    return new AccionMoverDto { Direccion = DireccionEnum.Abajo };
                case "L":
                    return new AccionMoverDto { Direccion = DireccionEnum.Izquierda };
                case "R":
                    return new AccionMoverDto { Direccion = DireccionEnum.Derecha };
                case "B":
                    return new AccionConstruirDto();
                case "":
                    return null;
                default:
                    return GetAccionRobot();
            }
        }
    }
}
