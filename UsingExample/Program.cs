using System;
using System.Drawing;
using System.Text;

namespace UsingExample
{
    class Program
    {
        static void Main(string[] args)
        {
            var size = new Size(27, 111);
            var printmap = new char[size.Width, size.Height];
            bool[,] map = MazeGenerator.MazeGenerator.Generate(size, new Point(1, 1));
            var path = AStar.AStar.Search(new Point(1, 1), new Point(size.Width - 2, size.Height-2), map);

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
                var builder = new StringBuilder();
                for (var j = 0; j < map.GetLength(1); j++)
                {
                    builder.Append(printmap[i, j]);
                }
                builder.Append(Environment.NewLine);
                Console.Write(builder.ToString());
            }
            Console.ReadKey();
        }
    }
}
