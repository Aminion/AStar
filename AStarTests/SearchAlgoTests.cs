using Microsoft.VisualStudio.TestTools.UnitTesting;
using AStar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace AStar.Tests
{
    [TestClass()]
    public class SearchAlgoTests
    {
        static bool[,] map = {
         { true, true, true, true, true},
         { true, true, false, true, true},
         { true, true, false, true, true},
         { true, true, false, false, true},
         { true, true, true, true, true}
        };
        [TestMethod()]
        public void SearchTest()
        {
            var path = SearchAlgo.Search(new Point(0, 2), new Point(4, 2), map);
            Assert.IsTrue(path != null);
        }

        [TestMethod()]
        public void HTest()
        {
            var res = SearchAlgo.H(new Point(0, 3), new Point(4, 0));
            Assert.IsTrue(Math.Round(res) == 5);
        }

        [TestMethod()]
        public void PassableNeighborsOfTest()
        {
            var point = new Point(0, 0);
            var res = SearchAlgo.PassableNeighborsOf(point, map).ToList();
            Assert.IsTrue(res.Count == 3);
            Assert.IsTrue(res.Contains(new Point(1, 0)));
            Assert.IsTrue(res.Contains(new Point(1, 1)));
            Assert.IsTrue(res.Contains(new Point(0, 1)));
        }
        [TestMethod()]
        public void PassableNeighborsOfTestWalls()
        {
            var point = new Point(2, 1);
            var res = SearchAlgo.PassableNeighborsOf(point, map).ToList();
            Assert.IsTrue(res.Count == 5);
        }

        [TestMethod()]
        public void SupposedNeighborsOfTest()
        {
            var res = SearchAlgo.SupposedNeighborsOf(new Point(1, 1)).ToList();
            Assert.IsTrue(res.Count == 8);
        }
    }
}