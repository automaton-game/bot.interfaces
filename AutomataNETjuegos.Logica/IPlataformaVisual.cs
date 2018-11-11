using AutomataNETjuegos.Contratos.Entorno;

namespace AutomataNETjuegos.Logica
{
    public interface IPlataformaVisual
    {
        void Dibujar(Tablero tablero);

        void Consola(string msg);
    }
}