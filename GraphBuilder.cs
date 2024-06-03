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
        public void CreateGraph(List<double> data, string filename, string title, string xAxis, string yAxis, Color color)
        {
            GraphPane graphPane = new GraphPane();

            graphPane.Title.Text = title;
            graphPane.XAxis.Title.Text = xAxis;
            graphPane.YAxis.Title.Text = yAxis;

            PointPairList pointPairList = new PointPairList();

            for (int i = 0; i < data.Count; i += 30)
            {
                pointPairList.Add(i, data[i]);
            }

            LineItem lineItem = graphPane.AddCurve("Sample Line Series", pointPairList, color, SymbolType.Circle);

            // Настройка масштаба осей
            graphPane.XAxis.Scale.Min = 0;
            graphPane.XAxis.Scale.Max = data.Count;
            graphPane.YAxis.Scale.Min = data.Min();
            graphPane.YAxis.Scale.Max = data.Max();

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
    }
}
