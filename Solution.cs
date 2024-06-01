using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KomivoyazherFireMethod
{
    public class Solution
    {
        public List<int> Cities { get; set; }
        public double Distance { get; set; }
        private double[,] distanceMatrix;

        private Random random;

        public Solution(List<int> cities, double[,] distanceMatrix, Random random)
        {
            this.distanceMatrix = distanceMatrix;
            this.random = random;
            Cities = new List<int>(cities);
            Distance = CalculateTotalDistance();
        }

        // Перестановка двух случайных городов
        public void SwapCities()
        {
            int indexA = random.Next(Cities.Count);
            int indexB = (indexA + random.Next(1, Cities.Count)) % Cities.Count;

            int temp = Cities[indexA];
            Cities[indexA] = Cities[indexB];
            Cities[indexB] = temp;

            Distance = CalculateTotalDistance();
        }

        // Рассчитать общее расстояние
        private double CalculateTotalDistance()
        {
            double totalDistance = 0.0;
            for (int i = 0; i < Cities.Count; i++)
            {
                int currentCity = Cities[i];
                int nextCity = Cities[(i + 1) % Cities.Count];
                totalDistance += distanceMatrix[currentCity, nextCity];
            }
            return totalDistance;
        }
    }
}
