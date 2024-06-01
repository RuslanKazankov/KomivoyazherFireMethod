﻿namespace KomivoyazherFireMethod
{
    class Program
    {
        static Random random = new Random();

        static void Main(string[] args)
        {
            IKomiParams komiParams = new KomiParamsReader("KomiParams.txt");
            KomiDataReader komiData = new KomiDataReader("KomiData.txt");

            double[,] distanceMatrix = komiData.getDistanceMatrix();
            double temperature = komiParams.getTemperature();
            double coolingRate = komiParams.getCoolingRate();

            KomivoyazherClient komi = new KomivoyazherClient(distanceMatrix, temperature, coolingRate);
            komi.StartAlgorithm();
        }
    }

}