using System;
using AutomataNETjuegos.Contratos.Robots;
using AutomataNETjuegos.Logica;
using AutomataNETjuegos.Robots;

namespace AutomataNETjuegos.Web.WebTools
{
    public class FabricaRobot : IFabricaRobot
    {
        public IRobot ObtenerRobot(Type tipo)
        {
            return (IRobot)Activator.CreateInstance(tipo); ;
        }

        public IRobot ObtenerRobot(string t)
        {
            return new RobotDefensivo();
        }
    }
}
