using System.Collections.Generic;

namespace AutomataNETjuegos.Contratos.Entorno
{
    public class FilaTablero
    {
        public int NroFila { get; set; }

        public IList<Casillero> Casilleros { get; set; }

        public Tablero Tablero { get; set; }
    }
}
