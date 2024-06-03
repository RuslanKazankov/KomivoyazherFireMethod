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
            //IKomiData komiData = new KomiDataGenerator(10000, 10, 1000);

            KomivoyazherClient komi = new KomivoyazherClient(komiData, komiParams);
            komi.StartAlgorithm();

            List<double> distances = komi.AlgorithmDistances;
            List<double> temperatures = komi.AlgorithmTemperatures;

            GraphBuilder gBuilder = new GraphBuilder();
            gBuilder.CreateGraph(distances, "distances.png", "distances", "iterations", "values", Color.Black);
            gBuilder.CreateGraph(temperatures, "temperatures.png", "temperatures", "iterations", "values", Color.Blue);
        }
    }

}
