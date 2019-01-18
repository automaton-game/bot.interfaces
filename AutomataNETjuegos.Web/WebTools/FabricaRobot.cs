using AutomataNETjuegos.Contratos.Robots;
using AutomataNETjuegos.JugadorManual;
using AutomataNETjuegos.Logica;
using System;

namespace AutomataNETjuegos.Web.WebTools
{
    public class FabricaRobot : IFabricaRobot
    {
        private readonly IServiceProvider serviceProvider;

        public FabricaRobot(IServiceProvider serviceProvider)
        {
            this.serviceProvider = serviceProvider;
        }

        public IRobot ObtenerRobot(Type tipo)
        {
            return new RobotDefensivo();
            return (IRobot)serviceProvider.GetService(tipo);
        }
    }
}
