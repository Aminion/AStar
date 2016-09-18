using System;
using System.Drawing;
using MazeGenerator;
using AStar;

namespace UsingExample
{
    class Program
    {
        static void Main(string[] args)
        {
            var size = new Size(93, 227);
            var printmap = new char[size.Width, size.Height];
            bool[,] map = MazeGenerator.MazeGenerator.Generate(size, new Point(1, 1));
            var path = AStar.AStar.Search(new Point(1, 1), new Point(91, 225), map);

            for (var i = 0; i < map.GetLength(0); i++)
            {
                for (var j = 0; j < map.GetLength(1); j++)
                {
                    printmap[i, j] = map[i, j] ? ' ' : '#';
                }
            }
            foreach (var p in path)
            {
                printmap[p.X, p.Y] = '.';
            }
            for (var i = 0; i < map.GetLength(0); i++)
            {
                for (var j = 0; j < map.GetLength(1); j++)
                {

                    Console.Write(printmap[i, j]);
                }
                Console.Write(Environment.NewLine);
            }
            Console.ReadKey();
        }
    }
}
