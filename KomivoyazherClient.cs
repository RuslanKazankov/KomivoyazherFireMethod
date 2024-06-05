using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static KomivoyazherFireMethod.CoolingFunctionHelper;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace KomivoyazherFireMethod
{
    public class KomivoyazherClient
    {
        //Results
        public List<double> AlgorithmDistances { get; } = [];
        public List<double> AlgorithmTemperatures { get; } = [];
        public double AlgorithmTime { get; private set; }

        //Params
        private Probability CoolingFunction;

        private Random random = new Random();

        private double[,] distanceMatrix;

        List<int> cities = new List<int>();

        private double currentTemperature;
        private double endTemperature;
        private double coolingRate;
        private int limitIterations;

        public KomivoyazherClient(double[,] distanceMatrix, Probability coolingFunction, double coolingRate, double startTemperature = 10000, double endTemperature = 1, int limitIterations = 5000)
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
            this.limitIterations = limitIterations;
            this.CoolingFunction = coolingFunction;
        }

        public KomivoyazherClient(IKomiData data, IKomiParams komiParams)
        {
            distanceMatrix = data.GetData();

            int citiesCount = (int)Math.Sqrt(distanceMatrix.Length);
            for (int i = 0; i < citiesCount; i++)
            {
                cities.Add(i);
            }

            currentTemperature = komiParams.getStartTemperature();
            endTemperature = komiParams.getEndTemperature();
            coolingRate = komiParams.getCoolingRate();
            limitIterations = komiParams.getLimitIterations();
            CoolingFunction = ExponentialCooling;
        }
        public KomivoyazherClient(IKomiData data, IKomiParams komiParams, Probability coolingFunction)
        {
            distanceMatrix = data.GetData();

            int citiesCount = (int)Math.Sqrt(distanceMatrix.Length);
            for (int i = 0; i < citiesCount; i++)
            {
                cities.Add(i);
            }

            currentTemperature = komiParams.getStartTemperature();
            endTemperature = komiParams.getEndTemperature();
            coolingRate = komiParams.getCoolingRate();
            limitIterations = komiParams.getLimitIterations();
            CoolingFunction = coolingFunction;
        }

        public void StartAlgorithm()
        {
            Console.WriteLine("Алгоритм запущен...");
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();

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

                AlgorithmDistances.Add(currentSolution.Distance);
                AlgorithmTemperatures.Add(currentTemperature);

                currentTemperature = CoolingFunction(currentTemperature, coolingRate);

                countIterations++;
            }

            foreach (int city in bestSolution.Cities)
            {
                Console.WriteLine(city);
            }
            Console.WriteLine("Кратчайшая дистанция найдена: " + bestSolution.Distance);
            Console.WriteLine("Количество итераций: " + countIterations);

            stopwatch.Stop();
            AlgorithmTime = stopwatch.Elapsed.TotalSeconds;
            Console.WriteLine("Время выполнения(с): " + AlgorithmTime);
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
