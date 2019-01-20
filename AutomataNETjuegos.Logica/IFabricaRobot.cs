using AutomataNETjuegos.Contratos.Robots;
using System;

namespace AutomataNETjuegos.Logica
{
    public interface IFabricaRobot
    {
        IRobot ObtenerRobot(Type tipo);

        IRobot ObtenerRobot(string t);
    }
}
