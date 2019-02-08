using AutomataNETjuegos.Logica;
using AutomataNETjuegos.Web.Models;
using System.Collections.Generic;

namespace AutomataNETjuegos.Web.Logica
{
    public interface IRegistroJuegosManuales
    {
        string Guardar(IJuego2v2 juego);
        IJuego2v2 Obtener(string id);
        ICollection<Tablero> GuardarTablero(string idTablero, Tablero tablero);

        ICollection<Tablero> ObtenerTableros(string idTablero);
    }
}