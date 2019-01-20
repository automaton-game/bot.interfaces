using System;
using AutomataNETjuegos.Contratos.Robots;
using AutomataNETjuegos.Logica;

namespace AutomataNETjuegos.Web.WebTools
{
    public class FabricaRobot : IFabricaRobot
    {
        public IRobot ObtenerRobot(Type tipo)
        {
            return (IRobot)Activator.CreateInstance(tipo); ;
        }
    }
}
