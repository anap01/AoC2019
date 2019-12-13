using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AdventOfCode
{
    [TestClass]
    public class Day10
    {
        public TestContext TestContext { get; set; }
            
        [TestMethod]
        public void Part1()
        {
            var asteroids = CreateAsteroidMap(Day10Input);

            var visibleAsteroids = asteroids.ToDictionary(k => k, v => 0);
            foreach (var asteroid in asteroids)
            {
                var otherAsteroids = asteroids.Except(new[] {asteroid});
                var visible = otherAsteroids.GroupBy(a => CalculateAngle(a.x - asteroid.x, a.y - asteroid.y));
                visibleAsteroids[asteroid] = visible.Count();
            }

            var output = visibleAsteroids.OrderByDescending(a => a.Value).First();
            TestContext.WriteLine($"{output.Key} : {output.Value}");
        }

        [TestMethod]
        public void Part2()
        {
            var asteroids = CreateAsteroidMap(Day10Input);

            var visibleAsteroids = new Dictionary<(int x, int y), IEnumerable<IGrouping<double, (int x, int y)>>>();
            foreach (var asteroid in asteroids)
            {
                var otherAsteroids = asteroids.Except(new[] {asteroid});
                var visible = otherAsteroids.GroupBy(a => CalculateAngle(a.x - asteroid.x, a.y - asteroid.y));
                visibleAsteroids[asteroid] = visible;
            }

            var output = visibleAsteroids.OrderByDescending(a => a.Value.Count()).First();
            var theAsteroid = output.Key;
            var visibles = output.Value.OrderBy(v => v.Key).ToDictionary(k => k.Key, v => v.ToList());
            var angles = visibles.Keys.OrderBy(k => k);
            var i = 0;
            var lap = 0;
            while (i < 200)
            {
                foreach (var angle in angles)
                {
                    if (visibles[angle].Count() > lap)
                        i++;
                    
                    if (i == 200)
                    {
                        var twohundreth = visibles[angle].OrderBy(a => (a.x - theAsteroid.x + a.y - theAsteroid.y)).Skip(lap).First();
                        TestContext.WriteLine((twohundreth.x * 100 + twohundreth.y).ToString());
                    }
                }
                lap++;
            }

            
            
            TestContext.WriteLine($"{output.Key} : {output.Value}");

        }
        
        private static HashSet<(int x, int y)> CreateAsteroidMap(string imput)
        {
            var stringReader = new StringReader(imput);
            string line;
            var y = 0;
            var asteroids = new HashSet<(int x, int y)>();
            while ((line = stringReader.ReadLine()) != null)
            {
                var x = 0;
                foreach (var character in line)
                {
                    if (character == '#')
                    {
                        asteroids.Add((x, y));
                    }

                    x++;
                }

                y++;
            }

            return asteroids;
        }

        [TestMethod]
        public void TestAngle()
        {
            TestContext.WriteLine("0");
            TestContext.WriteLine(CalculateAngle(0, -1).ToString());
            TestContext.WriteLine("45");
            TestContext.WriteLine(CalculateAngle(1, -1).ToString());
            TestContext.WriteLine("90");
            TestContext.WriteLine(CalculateAngle(1, 0).ToString());
            TestContext.WriteLine("135");
            TestContext.WriteLine(CalculateAngle(1, 1).ToString());
            TestContext.WriteLine("180");
            TestContext.WriteLine(CalculateAngle(0, 1).ToString());
            TestContext.WriteLine("225");
            TestContext.WriteLine(CalculateAngle(-1, 1).ToString());
            TestContext.WriteLine("270");
            TestContext.WriteLine(CalculateAngle(-1, 0).ToString());
            TestContext.WriteLine("315");
            TestContext.WriteLine(CalculateAngle(-1, -1).ToString());
        }
        private double CalculateAngle(double deltaX, double deltaY)
        {
            var angle = Math.Atan(deltaY / deltaX) * 180 / Math.PI;

            if (deltaX < 0 && deltaY < 0)
                return 270 + angle;
            if (deltaX < 0 && deltaY >= 0)
                return 270 + angle;
            if (deltaX > 0 && deltaY > 0)
                return 90 + angle;
            if (deltaX > 0 && deltaY < 0)
                return 90 + angle;
            return angle + 90;
        }

        const string Day10TestInput = @".#..#
.....
#####
....#
...##";

        private const string Day10TestInput2 = @"......#.#.
#..#.#....
..#######.
.#.#.###..
.#..#.....
..#....#.#
#..#....#.
.##.#..###
##...#..#.
.#....####";

        private const string Day10TestInput3 = @".#..##.###...#######
##.############..##.
.#.######.########.#
.###.#######.####.#.
#####.##.#.##.###.##
..#####..#.#########
####################
#.####....###.#.#.##
##.#################
#####.##.###..####..
..######..##.#######
####.##.####...##..#
.#####..#.######.###
##...#.##########...
#.##########.#######
.####.#.###.###.#.##
....##.##.###..#####
.#.#.###########.###
#.#.#.#####.####.###
###.##.####.##.#..##";
        
        private const string Day10Input = @"...###.#########.####
.######.###.###.##...
####.########.#####.#
########.####.##.###.
####..#.####.#.#.##..
#.################.##
..######.##.##.#####.
#.####.#####.###.#.##
#####.#########.#####
#####.##..##..#.#####
##.######....########
.#######.#.#########.
.#.##.#.#.#.##.###.##
######...####.#.#.###
###############.#.###
#.#####.##..###.##.#.
##..##..###.#.#######
#..#..########.#.##..
#.#.######.##.##...##
.#.##.#####.#..#####.
#.#.##########..#.##.";
    }
}