using System.Collections.Generic;

namespace AutomataNETjuegos.Web.Models
{
    public class JuegoResponse
    {
        public ICollection<Tablero> Tableros { get; set; }

        public string Ganador { get; set; }

        public string MotivoDerrota { get; set; }
    }
}
