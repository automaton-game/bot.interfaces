using AutomataNETjuegos.Contratos.Entorno;
using System;

namespace AutomataNETjuegos.Logica
{
    public interface IJuego2v2
    {
        Tablero Tablero { get; }

        void AgregarRobot(Type robotType);

        Type AgregarRobot(string robotCode);

        string JugarTurno();

        string ObtenerUsuarioGanador();
    }
}