using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KomivoyazherFireMethod
{
    public class KomivoyazherClient
    {
        private Random random = new Random();

        // Пример матрицы расстояний между городами
        private double[,] distanceMatrix;

        // Создание списка городов (индексы)
        List<int> cities = new List<int>();

        // Начальная температура и коэффициент охлаждения
        private double temperature;
        private double coolingRate;

        public KomivoyazherClient(double[,] distanceMatrix, double temperature = 10000, double coolingRate = 0.003) 
        {
            this.distanceMatrix = distanceMatrix;

            int citiesCount = (int)Math.Sqrt(distanceMatrix.Length);
            for (int i = 0; i < citiesCount; i++)
            {
                cities.Add(i);
            }

            this.temperature = temperature;
            this.coolingRate = coolingRate;
        }

        public void StartAlgorithm()
        {
            Solution currentSolution = new Solution(cities, distanceMatrix, random);
            Solution bestSolution = new Solution(cities, distanceMatrix, random);

            while (temperature > 1)
            {
                Solution newSolution = new Solution(currentSolution.Cities, distanceMatrix, random);
                newSolution.SwapCities();

                double currentEnergy = currentSolution.Distance;
                double neighbourEnergy = newSolution.Distance;

                if (AcceptanceProbability(currentEnergy, neighbourEnergy, temperature) > random.NextDouble())
                {
                    currentSolution = newSolution;
                }

                if (currentSolution.Distance < bestSolution.Distance)
                {
                    bestSolution = currentSolution;
                }

                temperature *= 1 - coolingRate;
            }

            Console.WriteLine("Shortest distance found: " + bestSolution.Distance);
            foreach (int city in bestSolution.Cities)
            {
                Console.WriteLine(city);
            }
        }

        private static double AcceptanceProbability(double currentEnergy, double neighbourEnergy, double temperature)
        {
            if (neighbourEnergy < currentEnergy)
            {
                return 1.0;
            }
            return Math.Exp((currentEnergy - neighbourEnergy) / temperature);
        }
    }
}
