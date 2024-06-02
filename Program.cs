namespace KomivoyazherFireMethod
{
    class Program
    {
        static Random random = new Random();

        static void Main(string[] args)
        {
            IKomiParams komiParams = new KomiParamsReader("KomiParams.txt");
            KomiDataReader komiData = new KomiDataReader("KomiData.txt");

            double[,] distanceMatrix = komiData.getDistanceMatrix();
            double startTemperature = komiParams.getStartTemperature();
            double endTemperature = komiParams.getEndTemperature();
            double coolingRate = komiParams.getCoolingRate();

            KomivoyazherClient komi = new KomivoyazherClient(distanceMatrix, startTemperature, endTemperature, coolingRate);
            komi.StartAlgorithm();
        }
    }

}
