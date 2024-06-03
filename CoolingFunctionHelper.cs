using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KomivoyazherFireMethod
{
    public static class CoolingFunctionHelper
    {
        public delegate double Probability(double temperature, double coolingRate);
        public static Probability GetCoolingFunction(string nameCoolingFunction)
        {
            return nameCoolingFunction switch
            {
                nameof(LinearCooling) => LinearCooling,
                nameof(ExponentialCooling) => ExponentialCooling,
                nameof(LogarithmicCooling) => LogarithmicCooling,
                nameof(GeometricCooling) => GeometricCooling,
                _ => ExponentialCooling,
            };
        }

        public static double LinearCooling(double temperature, double coolingRate)
        {
            return temperature - coolingRate;
        }

        public static double ExponentialCooling(double temperature, double coolingRate)
        {
            return temperature * (1 - coolingRate);
        }

        public static double LogarithmicCooling(double temperature, double coolingRate)
        {
            return temperature / (1 + coolingRate * Math.Log(1 + temperature));
        }

        public static double GeometricCooling(double temperature, double coolingRate)
        {
            return temperature * coolingRate;
        }
    }
}
