using System.Diagnostics;
using System.Drawing;

namespace KomivoyazherFireMethod
{
    class Program
    {
        static Random random = new Random();

        static void Main(string[] args)
        {
            IKomiParams komiParams = new KomiParamsReader("KomiParams.txt");
            IKomiData komiData = new KomiDataReader("KomiData.txt");
            //IKomiData komiData = new KomiDataGenerator(15, 10, 100);

            KomivoyazherClient komi = new KomivoyazherClient(komiData, komiParams);
            komi.StartAlgorithm();

            List<List<double>> datas = [];
            datas.Add(komi.AlgorithmDistances);
            //datas.Add(komi.AlgorithmTemperatures);

            GraphBuilder gBuilder = new GraphBuilder();
            gBuilder.CreateGraph(datas, "graph.png", "distances", "iterations", "values");

            Console.WriteLine("Enter to exit...");
            Console.ReadLine();

            //List<List<double>> datas = [];

            //double coolingRate = 0.0005;
            //for (int i = 2500; i < 5000; i += 500)
            //{
            //    Console.WriteLine("StartTemperature: " + i);
            //    KomivoyazherClient komi = new KomivoyazherClient(komiData.GetData(), CoolingFunctionHelper.ExponentialCooling, coolingRate, startTemperature: i, limitIterations: 10000);
            //    komi.StartAlgorithm();

            //    List<double> distances = komi.AlgorithmDistances;
            //    datas.Add(distances);
            //}

            //GraphBuilder gBuilder = new GraphBuilder();
            //gBuilder.CreateGraph(datas, $"coolingRate{coolingRate}.png", "distances", "iterations", "values");
        }
    }

}
