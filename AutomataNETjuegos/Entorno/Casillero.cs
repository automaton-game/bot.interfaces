using AutomataNETjuegos.Contratos.Robots;

namespace AutomataNETjuegos.Contratos.Entorno
{
    public class Casillero
    {
        public int NroFila { get; set; }

        public int NroColumna { get; set; }

        public IRobot Robot { get; set; }

        public IRobot Muralla { get; set; }

        public FilaTablero Fila { get; set; }
    }
}
