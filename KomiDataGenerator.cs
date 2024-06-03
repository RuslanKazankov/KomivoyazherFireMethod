using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KomivoyazherFireMethod
{
    public class KomiDataGenerator : IKomiData
    {
        private Random random;
        private int size;
        private double min;
        private double max;

        public KomiDataGenerator(int size, double min, double max) 
        {
            this.size = size;
            this.min = min;
            this.max = max;
            random = new Random();
        }

        public double[,] GetData()
        {
            double[,] data = new double[size, size];

            for (int i = 0; i < size; i++)
            {
                for (int j = i; j < size; j++)
                {
                    if (i == j)
                    {
                        data[i, j] = 0;
                    }
                    else
                    {
                        data[i, j] = random.Next() % (max - min) + min;
                        data[j, i] = data[i, j];
                    }
                }
            }

            LogDataInFile(data);

            return data;
        }

        private void LogData(double[,] data)
        {
            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    Console.Write(data[i, j] + " ");
                }
                Console.WriteLine();
            }
        }

        private void LogDataInFile(double[,] data)
        {
            using (StreamWriter writer = new StreamWriter("KomiData.txt"))
            {
                for (int i = 0; i < size; i++)
                {
                    for (int j = 0; j < size; j++)
                    {
                        writer.Write(data[i, j] + " ");
                    }
                    writer.WriteLine();
                }
            }
        }


    }
}
