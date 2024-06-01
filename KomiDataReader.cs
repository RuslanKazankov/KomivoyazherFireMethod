using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KomivoyazherFireMethod
{
    public class KomiDataReader
    {
        private List<double[]> distanceMatrix = [];

        public KomiDataReader(String file)
        {
            try
            {
                using StreamReader sr = new StreamReader(file);
                string? line;

                while ((line = sr.ReadLine()) != null)
                {
                    distanceMatrix.Add(ConvertFromStrings(line.Split(' ')));
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Файл не прочитан:");
                Console.WriteLine(e.Message);
            }
        }

        private double[] ConvertFromStrings(string[] strings)
        {
            double[] returnArray = new double[strings.Length];
            for (int i = 0; i < strings.Length; i++)
            {
                if (!double.TryParse(strings[i], out returnArray[i]))
                {
                    Console.WriteLine("Не получилось конвертировать distanceMatrix");
                    break;
                }
            }
            return returnArray;
        }
        public double[,] getDistanceMatrix()
        {
            double[,] returnMatrix = new double[distanceMatrix.Count, distanceMatrix.Count];
            for (int i = 0; i < distanceMatrix.Count; i++)
            {
                for (int j = 0; j < distanceMatrix.Count; j++)
                {
                    returnMatrix[i, j] = distanceMatrix[i][j];
                }
            }
            return returnMatrix;
        }
    }
}
