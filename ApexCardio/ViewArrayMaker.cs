using System;
using System.Drawing;
using System.Windows.Forms;

namespace Polycardio2
{
    static class ViewArrayMaker
    {
        public static Point[] MakeArray(Control view, int[] DataSource, uint index, int max)
        {
            return MakeArray(view, DataSource, index, max, 1, 1);
        }

        public static Point[] MakeArray(Control view, int[] DataSource, uint index, int max, double scaleY)
        {
            return MakeArray(view, DataSource, index, max, scaleY, 1);
        }

        public static Point[] MakeArray(Control view, int[] DataSource, uint index, int max, double scaleY, int scaleX)
        {
            // ВАЖНО: ByteDecomposer.DataArrSize ДОЛЖЕН быть степенью двойки (256, 512, 1024 и т.д.)
            var size = ByteDecomposer.DataArrSize;
            var h = view.Height;
            var w = view.Width;
            int pointsCount = w / scaleX;
            var arr = new Point[pointsCount];

            for (int i = 0; i < pointsCount; i++)
            {
                arr[i].X = i * scaleX;

                // Рассчитываем циклический индекс в кольцевом буфере
                long dataIndex = (index - pointsCount + i) & (size - 1);

                double res = h / 2.0 - (scaleY * (h * DataSource[dataIndex])) / max;
                arr[i].Y = (int)Math.Round(res);
            }
            return arr;
        }

        public static Point[] MakeArrayForView(Control view, int[] dataSource, int shift, int max)
        {
            return MakeArrayForView(view, dataSource, shift, max, 1, 1);
        }

        public static Point[] MakeArrayForView(Control view, int[] dataSource, int shift, int max, double scaleY)
        {
            return MakeArrayForView(view, dataSource, shift, max, scaleY, 1);
        }

        public static Point[] MakeArrayForView(Control view, int[] dataSource, int shift, int max, double scaleY, int scaleX)
        {
            int H = view.Height;
            int W = view.Width;
            int pointsCount = W / scaleX;
            var arr = new Point[pointsCount];

            for (int i = 0; i < pointsCount; i++)
            {
                arr[i].X = i * scaleX;

                // ИСПРАВЛЕНО: Индекс в массиве должен учитывать шаг масштаба по X (i * scaleX)
                int currentDataIndex = shift + (i * scaleX);

                double res;
                // ИСПРАВЛЕНО: Проверяем реальный индекс чтения currentDataIndex, а не просто i
                if (currentDataIndex < 0 || currentDataIndex >= dataSource.Length)
                {
                    res = H / 2.0; // Если вышли за пределы, рисуем линию по центру (изолинию)
                }
                else
                {
                    res = H / 2.0 - scaleY * (H * dataSource[currentDataIndex]) / max;
                }

                arr[i].Y = (int)Math.Round(res);
            }
            return arr;
        }

        public static Point[] MakeArrayForView(Control view, double[] dataSource, int shift, double max, double scaleY, double scaleX)
        {
            double H = view.Height;
            int W = view.Width;
            int scaleXInt = (int)scaleX;
            int pointsCount = W / scaleXInt;
            var arr = new Point[pointsCount];

            for (int i = 0; i < pointsCount; i++)
            {
                arr[i].X = i * scaleXInt;

                // ИСПРАВЛЕНО: Учитываем масштаб по X при выборе элемента из массива
                int currentDataIndex = shift + (i * scaleXInt);

                double res;
                // ИСПРАВЛЕНО: Защита от IndexOutOfRangeException по текущему индексу данных
                if (currentDataIndex < 0 || currentDataIndex >= dataSource.Length)
                {
                    res = H / 2.0;
                }
                else
                {
                    res = H / 2.0 - scaleY * (H * dataSource[currentDataIndex]) / max;
                }

                arr[i].Y = (int)Math.Round(res);
            }
            return arr;
        }
    }
}
