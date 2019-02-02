using AutomataNETjuegos.Contratos.Entorno;
using AutomataNETjuegos.Contratos.Robots;
using System;
using System.Collections.Generic;

namespace AutomataNETjuegos.Logica
{
    public interface IJuego2v2
    {
        Tablero Tablero { get; }

        void AgregarRobot(Type robotType);

        void AgregarRobot(string robotCode);

        bool JugarTurno();

        IRobot ObtenerRobotTurnoActual();

        IEnumerable<IRobot> GetJugadores();
    }
}