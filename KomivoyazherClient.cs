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

        private double[,] distanceMatrix;

        List<int> cities = new List<int>();

        private double currentTemperature;
        private double endTemperature;
        private double coolingRate;

        private int limitIterations = 5000;

        public KomivoyazherClient(double[,] distanceMatrix, double startTemperature = 10000, double endTemperature = 1, double coolingRate = 0.003) 
        {
            this.distanceMatrix = distanceMatrix;

            int citiesCount = (int)Math.Sqrt(distanceMatrix.Length);
            for (int i = 0; i < citiesCount; i++)
            {
                cities.Add(i);
            }

            this.currentTemperature = startTemperature;
            this.endTemperature = endTemperature;
            this.coolingRate = coolingRate;
        }

        public void StartAlgorithm()
        {
            Solution currentSolution = new Solution(cities, distanceMatrix, random);
            Solution bestSolution = new Solution(cities, distanceMatrix, random);

            int countIterations = 0;

            while (currentTemperature > endTemperature && countIterations < limitIterations)
            {
                Solution newSolution = new Solution(currentSolution.Cities, distanceMatrix, random);
                newSolution.SwapCities();

                double currentEnergy = currentSolution.Distance;
                double neighbourEnergy = newSolution.Distance;

                if (AcceptanceProbability(currentEnergy, neighbourEnergy, currentTemperature) > random.NextDouble())
                {
                    currentSolution = newSolution;
                }

                if (currentSolution.Distance < bestSolution.Distance)
                {
                    bestSolution = currentSolution;
                }

                currentTemperature *= 1 - coolingRate;
                countIterations++;
            }

            Console.WriteLine("Кратчайшая дистанция найдена: " + bestSolution.Distance);
            Console.WriteLine("Количество итераций: " + countIterations);
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
