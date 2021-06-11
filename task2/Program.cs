using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;

namespace task2
{
    class Program
    {
        public static double[,] ReadFile(string path) // чтение файла
        {
            var list = new List<string>();
            double[,] array = new double [4, 2];
            // {попытка
            try
            {
                using (StreamReader sr = new StreamReader(path, System.Text.Encoding.Default))
                {
                    string line;
                    int i = 0;
                    while ((line = sr.ReadLine()) != null)
                    {
                        string[] s_arr = line.Split();
                        array[i, 0] = Convert.ToDouble(s_arr[0].Replace(".", ","));
                        array[i, 1] = Convert.ToDouble(s_arr[1].Replace(".", ","));
                        i = i + 1;
                    }
                }
                return array;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                Console.ReadKey();
                return null;
            }
            // }
        }

        public static bool IsInside(int npol, double[,] polygon, double x, double y) //метод трассировки лучей (x и y координаты точки)
        {
            bool c = false;
            double[] xp = new double[npol]; //xp - координаты Х вершин, yp - координаты Y вершин
            double[] yp = new double[npol];
            for (int i = 0; i < npol; i++)
            {
                xp[i] = polygon[i, 0];
                yp[i] = polygon[i, 1];
            }
            for (int i = 0, j = npol - 1; i < npol; j = i++)
            {
                if ((((yp[i] <= y) && (y < yp[j])) || ((yp[j] <= y) && (y < yp[i]))) &&
                  (((yp[j] - yp[i]) != 0) && (x > ((xp[j] - xp[i]) * (y - yp[i]) / (yp[j] - yp[i]) + xp[i]))))
                    c = !c;
            }
            return c;
        }

        static void Main(string[] args)
        {
            // координаты 4-угольника
            string path = Directory.GetCurrentDirectory() + "\\" + args[0] + ".txt";
            double[,] polygon = ReadFile(path);
            if (polygon == null) // ошибка чтения
            {
                return;
            }
            // координаты точек
            path = Directory.GetCurrentDirectory() + "\\" + args[1] + ".txt";
            double[,] dots = ReadFile(path);
            if (dots == null) // ошибка чтения
            {
                return;
            }

            for (int i = 0; i < dots.GetUpperBound(0) + 1; i++)
            {
                // вершина?
                if (dots[i, 0] == polygon[0, 0] && dots[i, 1] == polygon[0, 1] ||
                    dots[i, 0] == polygon[1, 0] && dots[i, 1] == polygon[1, 1] ||
                    dots[i, 0] == polygon[2, 0] && dots[i, 1] == polygon[2, 1] ||
                    dots[i, 0] == polygon[3, 0] && dots[i, 1] == polygon[3, 1])
                {
                    Console.WriteLine("0");
                    continue;
                }
                // на орезке?
                if ((dots[i, 0] - polygon[0, 0]) / (polygon[1, 0] - polygon[0, 0]) == (dots[i, 1] - polygon[0, 1]) / (polygon[1, 1] - polygon[0, 1]) ||
                    (dots[i, 0] - polygon[1, 0]) / (polygon[2, 0] - polygon[1, 0]) == (dots[i, 1] - polygon[1, 1]) / (polygon[2, 1] - polygon[1, 1]) ||
                    (dots[i, 0] - polygon[2, 0]) / (polygon[3, 0] - polygon[2, 0]) == (dots[i, 1] - polygon[2, 1]) / (polygon[3, 1] - polygon[2, 1]) ||
                    (dots[i, 0] - polygon[3, 0]) / (polygon[0, 0] - polygon[3, 0]) == (dots[i, 1] - polygon[3, 1]) / (polygon[0, 1] - polygon[3, 1]))
                {
                    Console.WriteLine("1");
                    continue;
                }
                // внутри или снаружи?
                if (IsInside(4, polygon, dots[i, 0], dots[i, 1]))
                    Console.WriteLine("2");
                else
                    Console.WriteLine("3");
            }
            Console.ReadKey();
        }
    }
}
