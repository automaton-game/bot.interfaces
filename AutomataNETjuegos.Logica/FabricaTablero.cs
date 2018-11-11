using System.Linq;
using AutomataNETjuegos.Contratos.Entorno;

namespace AutomataNETjuegos.Logica
{
    public class FabricaTablero : IFabricaTablero
    {
        private const int filas = 4;
        private const int columnas = 4;

        public Tablero Crear()
        {
            var tablero = new Tablero();
            tablero.Filas = Enumerable.Range(1, filas).Select(f => {
                var fila = new FilaTablero
                {
                    NroFila = f
                };

                fila.Casilleros = Enumerable.Range(1, columnas).Select(c => new Casillero { Fila = fila, NroFila = f, NroColumna = c }).ToArray();
                return fila;
            }).ToArray();
            return tablero;
        }
    }
}
