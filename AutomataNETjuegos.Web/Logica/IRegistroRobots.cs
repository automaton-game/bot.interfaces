using System.Collections.Generic;

namespace AutomataNETjuegos.Web.Logica
{
    public interface IRegistroRobots
    {
        void Registrar(string key, string logica);

        int RegistrarVictoria(string key);

        string ObtenerUltimoCampeon();

        IDictionary<string, int> ObtenerResumen();
    }
}
