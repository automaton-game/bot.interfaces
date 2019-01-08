using System.Collections.Generic;

namespace AutomataNETjuegos.Web.Models
{
    public class FilaTablero
    {
        public int NroFila { get; set; }

        public IList<Casillero> Casilleros { get; set; }
    }
}
