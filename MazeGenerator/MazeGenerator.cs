using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace MazeGenerator
{
    public static class MazeGenerator
    {
        public static bool[,] Generate(Size mazeSize, Point start)
        {
            var maze = GenerateBlankField(mazeSize);
            var searchStack = new Stack<Point>();
            var visited = new HashSet<Point>();
            var rnd = new Random((int)DateTime.Now.Ticks);
            var current = start;
            do
            {
                if (!visited.Contains(current)) visited.Add(current);
                var randNeighbor = NeighborsOf(current, mazeSize).Shuffle(rnd).FirstOrDefault(p => !visited.Contains(p));
                if (randNeighbor == default(Point))
                {
                    current = searchStack.Pop();
                    continue;
                }
                else
                {
                    var b = PointBetween(current, randNeighbor);
                    maze[b.X, b.Y] = true;
                    searchStack.Push(current);
                    current = randNeighbor;
                    continue;
                }
            } while (searchStack.Any());
            return maze;
        }
        private static Point PointBetween(Point p1, Point p2) => new Point((p1.X + p2.X) / 2, (p1.Y + p2.Y) / 2);
        private static bool[,] GenerateBlankField(Size size)
        {
            var field = new bool[size.Width, size.Height];
            for (var i = 0; i < size.Height; i++)
            {
                for (var j = 0; j < size.Width; j++)
                {
                    if ((i % 2 != 0 && j % 2 != 0) && (i < size.Height - 1 && j < size.Width - 1)) field[j, i] = true;
                }
            }
            return field;
        }
        private static IEnumerable<T> Shuffle<T>(this IEnumerable<T> source, Random rng)
        {
            T[] elements = source.ToArray();
            for (int i = elements.Length - 1; i >= 0; i--)
            {
                int swapIndex = rng.Next(i + 1);
                yield return elements[swapIndex];
                elements[swapIndex] = elements[i];
            }
        }
        private static IEnumerable<Point> NeighborsOf(Point current, Size mapSize)
        {
            foreach (var p in SupposedNeighborsOf(current))
            {
                if (p.X < 0 || p.X >= mapSize.Width || p.Y < 0 || p.Y >= mapSize.Height)
                    continue;
                yield return p;
            }
        }
        private static IEnumerable<Point> SupposedNeighborsOf(Point point)
        {
            var x = point.X;
            var y = point.Y;
            yield return new Point(x + 2, y);
            yield return new Point(x - 2, y);
            yield return new Point(x, y + 2);
            yield return new Point(x, y - 2);
        }
    }
}