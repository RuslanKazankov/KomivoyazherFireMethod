using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZedGraph;

namespace KomivoyazherFireMethod
{
    public class GraphBuilder
    {
        private Random random = new Random();

        public void CreateGraph(List<List<double>> datas, string filename, string title, string xAxis, string yAxis)
        {
            GraphPane graphPane = new GraphPane();

            graphPane.Title.Text = title;
            graphPane.XAxis.Title.Text = xAxis;
            graphPane.YAxis.Title.Text = yAxis;

            double min = double.MaxValue;
            double max = double.MinValue;

            int dataI = 2500;
            foreach (List<double> data in datas)
            {
                PointPairList pointPairList = new PointPairList();

                for (int i = 0; i < data.Count; i += 30)
                {
                    if (min > data[i])
                        min = data[i];

                    if (max < data[i])
                        max = data[i];

                    pointPairList.Add(i, data[i]);
                }
                LineItem lineItem = graphPane.AddCurve($"distances", pointPairList, GetRandomColor(), SymbolType.None);
                dataI += 500;
            }


            // Настройка масштаба осей
            graphPane.XAxis.Scale.Min = 0;
            graphPane.XAxis.Scale.Max = datas[0].Count;
            graphPane.YAxis.Scale.Min = min;
            graphPane.YAxis.Scale.Max = max;

            graphPane.Rect = new RectangleF(0, 0, 1600, 1200);

            // Создание изображения
            using Bitmap bitmap = new Bitmap(1600, 1200);
            using (Graphics g = Graphics.FromImage(bitmap))
            {
                graphPane.AxisChange();
                graphPane.Draw(g);
            }
            bitmap.Save(filename, ImageFormat.Png);

            OpenGraph(filename);
        }

        private void OpenGraph(string filename)
        {
            try
            {
                Process.Start(new ProcessStartInfo
                {
                    FileName = filename,
                    UseShellExecute = true,
                    Verb = "open"
                });
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Не удалось открыть файл: {ex.Message}");
            }
        }

        private Color GetRandomColor()
        {
            return Color.FromArgb(random.Next(255), random.Next(255), random.Next(255));
        }
    }
}
