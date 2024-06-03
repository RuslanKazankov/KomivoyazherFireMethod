using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static KomivoyazherFireMethod.CoolingFunctionHelper;

namespace KomivoyazherFireMethod
{
    public class KomiParamsReader : IKomiParams
    {
        private double startTemperature;
        private double endTemperature;
        private double coolingRate;
        private int limitIterations;
        private Probability? coolingFunction;

        public KomiParamsReader(string file) 
        {
            try
            {
                using StreamReader sr = new StreamReader(file);
                string? line;

                while ((line = sr.ReadLine()) != null)
                {
                    if (line.Contains("startTemperature="))
                    {
                        if (!double.TryParse(line.Split('=')[1], out startTemperature))
                        {
                            Console.WriteLine(line);
                            Console.WriteLine(nameof(startTemperature) + ": " + startTemperature);
                            Console.WriteLine(nameof(startTemperature) + " не прочитан");
                        }
                        continue;
                    }

                    if (line.Contains("endTemperature="))
                    {
                        if (!double.TryParse(line.Split('=')[1], out endTemperature))
                        {
                            Console.WriteLine(line);
                            Console.WriteLine(nameof(endTemperature) + ": " + endTemperature);
                            Console.WriteLine(nameof(endTemperature) + " не прочитан");
                        }
                        continue;
                    }

                    if (line.Contains("coolingRate="))
                    {
                        if (!double.TryParse(line.Split('=')[1], System.Globalization.CultureInfo.InvariantCulture, out coolingRate))
                        {
                            Console.WriteLine(line);
                            Console.WriteLine(nameof(coolingRate) + ": " + coolingRate);
                            Console.WriteLine(nameof(coolingRate) + " не прочитан");
                        }
                        continue;
                    }

                    if (line.Contains("limitIterations="))
                    {
                        if (!int.TryParse(line.Split('=')[1], System.Globalization.CultureInfo.InvariantCulture, out limitIterations))
                        {
                            Console.WriteLine(line);
                            Console.WriteLine(nameof(limitIterations) + ": " + limitIterations);
                            Console.WriteLine(nameof(limitIterations) + " не прочитан");
                        }
                        continue;
                    }

                    if (line.Contains("coolingFunction="))
                    {
                        coolingFunction = CoolingFunctionHelper.GetCoolingFunction(line.Split('=')[1]);
                        continue;
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Файл не прочитан:");
                Console.WriteLine(e.Message);
            }
        }

        public double getCoolingRate()
        {
            return coolingRate;
        }

        public double getStartTemperature()
        {
            return startTemperature;
        }

        public double getEndTemperature()
        {
            return endTemperature;
        }

        public int getLimitIterations()
        {
            return limitIterations;
        }

        public Probability? getCoolingFunction()
        {
            return coolingFunction;
        }
    }
}
