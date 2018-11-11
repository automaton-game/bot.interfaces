using AutomataNETjuegos.Contratos.Robots;

namespace AutomataNETjuegos.JugadorManual
{
    public class RobotInputBox : IRobot
    {
        public AccionRobotDto GetAccionRobot()
        {
            var input = Microsoft.VisualBasic.Interaction.InputBox("MovimientoRobot");
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
