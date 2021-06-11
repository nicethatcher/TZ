using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;

namespace task1
{
    class Program
    {
        public static double Percentile(double[] elements)
        {
            Array.Sort(elements);
            double realIndex = 0.9 * (elements.Length - 1);
            int index = (int)realIndex;
            double frac = realIndex - index;
            if (index + 1 < elements.Length)
                return elements[index] * (1 - frac) + elements[index + 1] * frac;
            else
                return elements[index];
        }

        private static int Mediana(double[] elements)
        {
            var sum = elements.Sum();
            //перебираем элементы, пока не достигнем 50% от суммы:
            double accum = 0;
            for (int i = 0; i < elements.Length; i++)
            {
                accum += elements[i];
                if (accum >= sum / 2)
                    return i;
            }

            return elements.Length;
        }

        static void Main(string[] args)
        {
            var list = new List<double>();
            double[] array;
            // {чтение файла
            string path = Directory.GetCurrentDirectory() + "\\" + args[0] + ".txt";
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
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                Console.ReadKey();
                return;
            }
            // }

            array = list.ToArray(); // массив значений из файла

            var perc = Percentile(array); // 90 перцентиль
            array = list.ToArray();
            var med = Mediana(array); // медиана

            var sum = array.Sum();
            int min = 0;
            int max = 0;
            for (int i = 0; i < array.Length; i++)
            {
                if (array[min] > array[i])
                    min = i; // поиск минимального
                if (array[max] < array[i])
                    max = i; // поиск максимального
            }
            var sred = sum / array.Length; // среднее значение
            // { вывод на экран
            Console.WriteLine("{0:N}", perc);
            Console.WriteLine("{0:N}", array[med]);
            Console.WriteLine("{0:N}", array[max]);
            Console.WriteLine("{0:N}", array[min]);
            Console.WriteLine("{0:N}", sred);
            // }
            Console.ReadKey();
        }
    }
}
