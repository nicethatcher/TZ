using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;

namespace task3
{
    class Program
    {
        public static List<double> ReadFile(string path) // чтение файла
        {
            var list = new List<double>();
            // {попытка
            try
            {
                using (StreamReader sr = new StreamReader(path, System.Text.Encoding.Default))
                {
                    string line;
                    while ((line = sr.ReadLine()) != null)
                    {
                        list.Add(Convert.ToDouble(line.Replace('.', ',')));
                    }
                }
                return list;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                Console.ReadKey();
                return null;
            }
            // }
        }
        static void Main(string[] args)
        {
            var list = new List<double>();
            double[,] array = new double[5, 16];
            for (int i = 0; i < 5; i++)
            {
                Console.WriteLine("Path to Cash{0}:", i + 1);
                string path = Console.ReadLine();
                list = ReadFile(path);
                for (int j = 0; j < 16; j++)
                    array[i, j] = list[j];
            }

            int period = 0;
            double max = 0;
            for (int j = 0; j < 16; j++) // обход временных отрезков
            {
                double sum = 0;
                for (int i = 0; i < 5; i++)
                {
                    sum = sum + array[i, j];
                }
                if (max < sum) // поиск максимального значения суммы 5 касс
                {
                    max = sum;
                    period = j + 1;
                }
            }
            Console.WriteLine("{0}", period);
            Console.ReadKey();
        }
    }
}
