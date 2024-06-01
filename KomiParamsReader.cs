using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KomivoyazherFireMethod
{
    public class KomiParamsReader : IKomiParams
    {
        private double temperature;
        private double coolingRate;

        public KomiParamsReader(string file) 
        {
            try
            {
                using StreamReader sr = new StreamReader(file);
                string? line;

                while ((line = sr.ReadLine()) != null)
                {
                    if (line.Contains("temperature="))
                    {
                        if (!double.TryParse(line.Split('=')[1], out temperature))
                        {
                            Console.WriteLine(line);
                            Console.WriteLine(nameof(temperature) + ": " + temperature);
                            Console.WriteLine(nameof(temperature) + " не прочитан");
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

        public double getTemperature()
        {
            return temperature;
        }
    }
}
