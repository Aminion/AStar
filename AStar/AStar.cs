using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace AStar
{

    public class AStar
    {
        private class Node : IComparable<Node>
        {
            public Node Previous;
            public Point Position;
            public int G { get; private set; }
            public float H { get; private set; }
            public float F => G + H;
            public int CompareTo(Node node) => F.CompareTo(node.F);
            public override int GetHashCode() => Position.GetHashCode();
            public Node(Node prev, Point pos, float h, int g)
            {
                Previous = prev;
                Position = pos;
                H = h;
                G = g;
            }
        }

        private static IEnumerable<Point> SupposedNeighborsOf(Point current)
        {
            var x = current.X;
            var y = current.Y;
            yield return new Point(x + 1, y);
            yield return new Point(x - 1, y);
            yield return new Point(x, y + 1);
            yield return new Point(x, y - 1);

            yield return new Point(x + 1, y + 1);
            yield return new Point(x - 1, y - 1);
            yield return new Point(x - 1, y + 1);
            yield return new Point(x + 1, y - 1);
        }
        private static IEnumerable<Point> PassableNeighborsOf(Point current, bool[,] map)
        {
            var mapSize = new Size(map.GetLength(0), map.GetLength(1));
            var ret = new List<Point>();
            foreach (var p in SupposedNeighborsOf(current))
            {
                if (p.X < 0 || p.X >= mapSize.Width || p.Y < 0 || p.Y >= mapSize.Height || !map[p.X, p.Y]) continue;
                ret.Add(p);
            }
            return ret;
        }
        private static int NeighborOffset(Point current, Point neighbor) => current.X == neighbor.X || current.Y == neighbor.Y ? 10 : 14;
        private static float H(Point from, Point to) => (float)Math.Sqrt(Math.Pow(to.X - from.X, 2) + Math.Pow(to.Y - from.Y, 2));
        public static List<Point> Search(Point start, Point end, bool[,] map)
        {
            var mapSize = new Size(map.GetLength(0), map.GetLength(1));
            var open = new List<Node>();
            var nodes = new Dictionary<Point, Node>();
            var startNode = new Node(null, start, H(start, end), 0);
            open.Add(startNode);
            nodes.Add(startNode.Position, startNode);
            while (open.Any())
            {
                open.Sort();
                var current = open.First();
                if (current.Position == end)
                {
                    return Result(current);
                }
                foreach (var p in PassableNeighborsOf(current.Position, map))
                {
                    if (nodes.ContainsKey(p)) continue;
                    var node = new Node(current, p, H(current.Position, p), current.G + NeighborOffset(current.Position, p));
                    nodes.Add(p, node);
                    open.Add(node);
                }
                open.Remove(current);
            }
            return null;

        }
        private static List<Point> Result(Node current)
        {
            var ret = new List<Point>();
            while (current != null)
            {
                ret.Add(current.Position);
                current = current.Previous;
            }
            return ret;
        }
    }
}