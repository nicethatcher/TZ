using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;

namespace task4
{
    class Program
    {
        static void Main(string[] args)
        {
            var enter = new List<string>(); // список входящих
            var exit = new List<string>(); // список выходящих
            // {чтение файла
            string path = Directory.GetCurrentDirectory() + "\\" + args[0] + ".txt";
            try
            {
                using (StreamReader sr = new StreamReader(path, System.Text.Encoding.Default))
                {
                    string line;
                    while ((line = sr.ReadLine()) != null)
                    {
                        string[] s_arr = line.Split();
                        enter.Add(s_arr[0]);
                        exit.Add(s_arr[1]);
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
            // интервалы
            var time = enter.Union(exit).ToList();
            time.Sort();

            int max = 0;
            var maxtime = new List<string>();
            // обход интервалов
            for (int i = 0; i < time.Count - 1; i++)
            {
                int c = 0;
                for (int j = 0; j < enter.Count; j++)
                {
                    // считаем количество посетителей в каждом интервале
                    int entertime = Int32.Parse(enter[j].Replace(":", ""));
                    int a = Int32.Parse(time[i].Replace(":", ""));
                    int exittime = Int32.Parse(exit[j].Replace(":", ""));
                    int b = Int32.Parse(time[i + 1].Replace(":", ""));
                    // проверка посещения в данном интервале
                    if (entertime <= a && exittime >= b)
                        c = c + 1;
                }
                if (c >= max)
                {
                    // добавляем интервал с таким же значением
                    if (max == c)
                    {
                        maxtime.Add(time[i]);
                        maxtime.Add(time[i + 1]);
                    }
                    // список нового максимального значения
                    else
                    {
                        maxtime.Clear();
                        max = c;
                        maxtime.Add(time[i]);
                        maxtime.Add(time[i + 1]);
                    }
                }
            }
            // объединяем пограничные интервалы
            for (int i = 0; ; i++)
            {
                if (i == maxtime.Count - 1)
                    break;
                if (maxtime[i] == maxtime[i + 1])
                {
                    maxtime.RemoveAt(i);
                    maxtime.RemoveAt(i);
                    i = i - 1;
                }
            }
            for (int i = 0; i < maxtime.Count; i = i + 2)
                Console.WriteLine("{0} {1}", maxtime[i], maxtime[i + 1]);
            Console.ReadKey();
        }
    }
}
