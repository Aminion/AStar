using System;
using System.Drawing;

namespace UsingExample
{
    class Program
    {
        static void Main(string[] args)
        {
            var printmap = new char[5, 5];
            bool[,] map = {
             { true, true, false, true, true},
             { true, true, false, false, true},
             { true, true, false, true, true},
             { true, true, false, true, false},
             { true, true, true, true, true}
            };
            var path = AStar.AStar.Search(new Point(2, 0), new Point(0, 3), map);
            for (var i = 0; i < 5; i++)
            {
                for (var j = 0; j < 5; j++)
                {
                    printmap[i, j] = map[i, j] ? '_' : '#';
                }
            }
            foreach (var p in path)
            {
                printmap[p.X, p.Y] = '*';
            }
            for (var i = 0; i < 5; i++)
            {
                for (var j = 0; j < 5; j++)
                {
                    Console.Write(printmap[i, j]);
                }
                Console.Write(Environment.NewLine);
            }
            Console.ReadKey();
        }
    }
}
