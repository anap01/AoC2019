using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AdventOfCode
{


    [TestClass]
    public class Day3
    {
        public TestContext TestContext { get; set; }

        [TestMethod]
        public void Day3Method2()
        {
//            var path1 = TestInput3.Split(',').ToList();
            var path1 = Day3Input1.Split(',').ToList();
            var coords1 = new Dictionary<(int, int), int>();
            int x = 0, y = 0;
            int step = 0;
            foreach (var leg1 in path1)
            {
                switch (leg1[0])
                {
                    case 'R':
                        var upperx = x + int.Parse(leg1.Substring(1));
                        for (; x <= upperx; x++)
                        {
                            if (coords1.ContainsKey((x, y)) == false)
                                coords1.Add((x, y), step);
                            step++;
                        }

                        x--;
                        break;
                    case 'L':
                        var lowerx = x - int.Parse(leg1.Substring(1));
                        for (; x >= lowerx; x--)
                        {
                            if (coords1.ContainsKey((x, y)) == false)
                                coords1.Add((x, y), step);
                            step++;
                        }

                        x++;
                        break;
                    case 'U':
                        var uppery = y + int.Parse(leg1.Substring(1));
                        for (; y <= uppery; y++)
                        {
                            if (coords1.ContainsKey((x, y)) == false)
                                coords1.Add((x, y), step);
                            step++;
                        }

                        y--;
                        break;
                    case 'D':
                        var lowery = y - int.Parse(leg1.Substring(1));
                        for (; y >= lowery; y--)
                        {
                            if (coords1.ContainsKey((x, y)) == false)
                                coords1.Add((x, y), step);
                            step++;
                        }

                        y++;
                        break;
                }

                step--;
            }

//            var path2 = TestInput4.Split(',').ToList();
            var path2 = Day3Input2.Split(',').ToList();
            x = 0;
            y = 0;
            step = 0;
            var coords2 = new Dictionary<(int, int), int>();
            foreach (var leg2 in path2)
            {
                switch (leg2[0])
                {
                    case 'R':
                        var upperx = x + int.Parse(leg2.Substring(1));
                        for (; x <= upperx; x++)
                        {
                            if (coords2.ContainsKey((x, y)) == false)
                                coords2.Add((x, y), step);
                            step++;
                        }

                        x--;
                        break;
                    case 'L':
                        var lowerx = x - int.Parse(leg2.Substring(1));
                        for (; x >= lowerx; x--)
                        {
                            if (coords2.ContainsKey((x, y)) == false)
                                coords2.Add((x, y), step);
                            step++;
                        }

                        x++;
                        break;
                    case 'U':
                        var uppery = y + int.Parse(leg2.Substring(1));
                        for (; y <= uppery; y++)
                        {
                            if (coords2.ContainsKey((x, y)) == false)
                                coords2.Add((x, y), step);
                            step++;
                        }

                        y--;
                        break;
                    case 'D':
                        var lowery = y - int.Parse(leg2.Substring(1));
                        for (; y >= lowery; y--)
                        {
                            if (coords2.ContainsKey((x, y)) == false)
                                coords2.Add((x, y), step);
                            step++;
                        }

                        y++;
                        break;
                }

                step--;
            }

            var intesections = coords1.Keys.Intersect(coords2.Keys);
            var result = intesections.Select(i => coords1[i] + coords2[i]);
            TestContext.WriteLine(result.Where(i => i != 0).Min().ToString());
        }

        private string TestInput5 = "R8,U5,L5,D3";
        private string TestInput6 = "U7,R6,D4,L4";

        [TestMethod]
        public void Day3Method1C()
        {
//            var path1 = TestInput3.Split(',').ToList();
            var path1 = Day3Input1.Split(',').ToList();
            var coords1 = new HashSet<(int, int)>();
            int x = 0, y = 0;
            foreach (var leg1 in path1)
            {
                switch (leg1[0])
                {
                    case 'R':
                        var upperx = x + int.Parse(leg1.Substring(1));
                        for (; x <= upperx; x++)
                        {
                            coords1.Add((x, y));
                        }

                        x--;
                        break;
                    case 'L':
                        var lowerx = x - int.Parse(leg1.Substring(1));
                        for (; x >= lowerx; x--)
                        {
                            coords1.Add((x, y));
                        }

                        x++;
                        break;
                    case 'U':
                        var uppery = y + int.Parse(leg1.Substring(1));
                        for (; y <= uppery; y++)
                        {
                            coords1.Add((x, y));
                        }

                        y--;
                        break;
                    case 'D':
                        var lowery = y - int.Parse(leg1.Substring(1));
                        for (; y >= lowery; y--)
                        {
                            coords1.Add((x, y));
                        }

                        y++;
                        break;
                }
            }

//            var path2 = TestInput4.Split(',').ToList();
            var path2 = Day3Input2.Split(',').ToList();
            x = 0;
            y = 0;
            var coords2 = new HashSet<(int, int)>();
            foreach (var leg2 in path2)
            {
                switch (leg2[0])
                {
                    case 'R':
                        var upperx = x + int.Parse(leg2.Substring(1));
                        for (; x <= upperx; x++)
                        {
                            coords2.Add((x, y));
                        }

                        x--;
                        break;
                    case 'L':
                        var lowerx = x - int.Parse(leg2.Substring(1));
                        for (; x >= lowerx; x--)
                        {
                            coords2.Add((x, y));
                        }

                        x++;
                        break;
                    case 'U':
                        var uppery = y + int.Parse(leg2.Substring(1));
                        for (; y <= uppery; y++)
                        {
                            coords2.Add((x, y));
                        }

                        y--;
                        break;
                    case 'D':
                        var lowery = y - int.Parse(leg2.Substring(1));
                        for (; y >= lowery; y--)
                        {
                            coords2.Add((x, y));
                        }

                        y++;
                        break;
                }
            }

            coords1.IntersectWith(coords2);
            var result = coords1.Select(i => Math.Abs(i.Item1) + Math.Abs(i.Item2));
            TestContext.WriteLine(result.Where(i => i != 0).Min().ToString());
        }


        [TestMethod]
        public void Day3Method1A()
        {
            const int i1 = 20000;
            var array1 = new int[i1, i1];
            var array2 = new int[i1, i1];
//            var path1 = TestInput1.Split(',').ToList();
            var path1 = Day3Input1.Split(',').ToList();
            const int origo = 10000;
            int x = origo, y = origo;
            foreach (var leg1 in path1)
            {
                switch (leg1[0])
                {
                    case 'R':
                        var upperx = x + int.Parse(leg1.Substring(1));
                        for (; x <= upperx; x++)
                        {
                            array1[x, y] = 1;
                        }

                        x--;
                        break;
                    case 'L':
                        var lowerx = x - int.Parse(leg1.Substring(1));
                        for (; x >= lowerx; x--)
                        {
                            array1[x, y] = 1;
                        }

                        x++;
                        break;
                    case 'U':
                        var uppery = y + int.Parse(leg1.Substring(1));
                        for (; y <= uppery; y++)
                        {
                            array1[x, y] = 1;
                        }

                        y--;
                        break;
                    case 'D':
                        var lowery = y - int.Parse(leg1.Substring(1));
                        for (; y >= lowery; y--)
                        {
                            array1[x, y] = 1;
                        }

                        y++;
                        break;
                }
            }

//            var path2 = TestInput2.Split(',').ToList();
            var path2 = Day3Input2.Split(',').ToList();
            x = origo;
            y = origo;
            foreach (var leg2 in path2)
            {
                switch (leg2[0])
                {
                    case 'R':
                        var upperx = x + int.Parse(leg2.Substring(1));
                        for (; x <= upperx; x++)
                        {
                            array2[x, y] = 1;
                        }

                        x--;
                        break;
                    case 'L':
                        var lowerx = x - int.Parse(leg2.Substring(1));
                        for (; x >= lowerx; x--)
                        {
                            array2[x, y] = 1;
                        }

                        x++;
                        break;
                    case 'U':
                        var uppery = y + int.Parse(leg2.Substring(1));
                        for (; y <= uppery; y++)
                        {
                            array2[x, y] = 1;
                        }

                        y--;
                        break;
                    case 'D':
                        var lowery = y - int.Parse(leg2.Substring(1));
                        for (; y >= lowery; y--)
                        {
                            array2[x, y] = 1;
                        }

                        y++;
                        break;
                }
            }

            var result = new List<int>();
            for (var j = 0; j < i1; j++)
            {
                for (int i = 0; i < i1; i++)
                {
                    if (array1[i, j] == 1 && array2[i, j] == 1)
                    {
                        result.Add(Math.Abs(i - origo) + Math.Abs(j - origo));
                    }
                }
            }

            TestContext.WriteLine(result.Except(new[] {0}).Min().ToString());
        }

        [TestMethod]
        public void Day3Method1()
        {
            var size = (int) Math.Sqrt(int.MaxValue);
            var origo = size / 2;
            var array1 = new BitArray(size * size);
            var array2 = new BitArray(size * size);
//            var path1 = TestInput3.Split(',').ToList();
            var path1 = Day3Input1.Split(',').ToList();
            int x = origo, y = origo;
            foreach (var leg1 in path1)
            {
                switch (leg1[0])
                {
                    case 'R':
                        var upper = x + int.Parse(leg1.Substring(1));
                        for (; x <= upper; x++)
                        {
                            array1.Set(x + y * size, true);
                        }

                        x--;
                        break;
                    case 'L':
                        var lower = x - int.Parse(leg1.Substring(1));
                        for (; x >= lower; x--)
                        {
                            array1.Set(x + y * size, true);
                        }

                        x++;
                        break;
                    case 'U':
                        var upperY = y + int.Parse(leg1.Substring(1));
                        for (; y <= upperY; y++)
                        {
                            array1.Set(x + y * size, true);
                        }

                        y--;
                        break;
                    case 'D':
                        var lowerY = y - int.Parse(leg1.Substring(1));
                        for (; y >= lowerY; y--)
                        {
                            array1.Set(x + y * size, true);
                        }

                        y++;
                        break;
                }
            }

//            var path2 = TestInput4.Split(',').ToList();
            var path2 = Day3Input2.Split(',').ToList();
            x = origo;
            y = origo;
            foreach (var leg2 in path2)
            {
                switch (leg2[0])
                {
                    case 'R':
                        var upper = x + int.Parse(leg2.Substring(1));
                        for (; x <= upper; x++)
                        {
                            array2.Set(x + y * size, true);
                        }

                        x--;
                        break;
                    case 'L':
                        var lower = x - int.Parse(leg2.Substring(1));
                        for (; x >= lower; x--)
                        {
                            array2.Set(x + y * size, true);
                        }

                        x++;
                        break;
                    case 'U':
                        var uppery = y + int.Parse(leg2.Substring(1));
                        for (; y <= uppery; y++)
                        {
                            array2.Set(x + y * size, true);
                        }

                        y--;
                        break;
                    case 'D':
                        var lowery = y - int.Parse(leg2.Substring(1));
                        for (; y >= lowery; y--)
                        {
                            array2.Set(x + y * size, true);
                        }

                        y++;
                        break;
                }
            }

            var crossings = array1.And(array2);
            var result = new List<int>();

            for (int i = 0; i < crossings.Count; i++)
            {
                if (crossings[i])
                {
                    result.Add(Math.Abs(i % size - origo) + Math.Abs(i / size - origo));
                }
            }

            TestContext.WriteLine(result.OrderBy(i => i).Skip(1).First().ToString());
        }

        private string TestInput1 = "R75,D30,R83,U83,L12,D49,R71,U7,L72";
        private string TestInput2 = "U62,R66,U55,R34,D71,R55,D58,R83";
        private string TestInput3 = "R98,U47,R26,D63,R33,U87,L62,D20,R33,U53,R51";
        private string TestInput4 = "U98,R91,D20,R16,D67,R40,U7,R15,U6,R7";

        private string Day3Input1 =
            "R1009,D117,L888,D799,L611,U766,L832,U859,L892,D79,R645,U191,L681,D787,R447,D429,L988,U536,L486,D832,R221,D619,R268,D545,L706,U234,L528,D453,R493,D24,L688,U658,L74,D281,R910,D849,L5,U16,R935,D399,L417,U609,R22,D782,L432,D83,L357,D982,L902,U294,L338,U102,R342,D621,R106,U979,L238,U158,R930,D948,L700,D808,R445,U897,R980,U227,L466,D416,R244,U396,R576,U157,R548,U795,R709,U550,R137,U212,L977,U786,L423,D792,R391,D974,R390,U771,R270,D409,L917,D9,R412,D699,L170,D276,L912,U710,R814,U656,R4,D800,R596,U970,L194,U315,L845,D490,L303,U514,L675,D737,L880,D86,L253,D525,R861,D5,R424,D113,L764,D900,R485,D421,R125,U684,R53,U96,L871,U260,R456,U378,L448,D450,L903,D482,R750,U961,R264,D501,R605,D367,R550,U642,R228,U164,L343,U868,R595,D318,R452,U845,L571,D281,R49,D889,L481,U963,R182,U358,R454,U267,L790,D252,R455,D188,L73,U256,L835,D816,R503,U895,L259,U418,R642,U818,L187,U355,R772,U466,R21,U91,R707,D349,L200,U305,R931,D982,L334,D416,L247,D935,L326,U449,L398,D914,R602,U10,R762,D944,L639,D141,L457,U579,L198,U527,R750,U167,R816,D753,R850,D281,L712,D583,L172,D254,L544,D456,R966,U839,R673,D479,R730,D912,R992,D969,R766,U205,R477,D719,R172,D735,R998,D687,R698,D407,R172,U945,R199,U348,L256,D876,R580,U770,L483,D437,R353,D214,R619,U541,R234,D962,R842,U639,R520,D354,L279,D15,R42,U138,L321,D376,L628,D893,L670,D574,L339,U298,L321,D120,L370,U408,L333,D353,L263,D79,R535,D487,R113,D638,R623,D59,L508,D866,R315,U166,L534,U927,L401,D626,L19,D994,L778,D317,L936,U207,L768,U948,R452,U165,R864,D283,L874";

        private string Day3Input2 =
            "L995,D93,L293,U447,L793,D605,R497,D155,L542,D570,R113,D779,L510,U367,L71,D980,R237,U290,L983,U49,R745,U182,L922,D174,L189,D629,R315,D203,R533,U72,L981,D848,L616,U654,R445,D864,R526,D668,L678,U378,L740,D840,L202,D429,R136,D998,L116,D554,L893,U759,R617,U942,R999,U582,L220,U447,R895,D13,R217,U743,L865,U950,R91,D381,R662,D518,L798,D637,L213,D93,L231,D185,R704,U581,L268,U773,R405,U862,R796,U73,L891,U553,L952,U450,R778,D868,R329,D669,L182,U378,L933,D83,R574,U807,R785,D278,R139,D362,R8,U546,R651,U241,L462,D309,L261,D307,L85,U701,L913,U271,R814,U723,L777,D256,R417,U814,L461,U652,R198,D747,R914,U520,R806,U956,L771,D229,R984,U685,R663,D812,R650,U214,R839,U574,L10,U66,R644,D371,L917,D819,L73,D236,R277,U611,R390,U723,L129,D496,L552,D451,R584,U105,L805,U165,R179,D372,L405,D702,R14,U332,L893,D419,R342,D146,R907,D672,L316,U257,L903,U919,L942,U771,R879,U624,L280,U150,L320,U220,R590,D242,R744,U291,R562,U418,L898,U66,L564,U495,R837,D555,L739,D780,R409,D122,L426,D857,R937,D600,R428,D592,R727,U917,R256,D680,L422,U630,L14,U240,R617,D664,L961,D554,L302,U925,L376,D187,L700,D31,L762,U397,L554,D217,R679,D683,R680,D572,R54,D164,L940,D523,R140,U52,L506,D638,R331,D415,R389,D884,R410,D62,R691,U665,R889,U864,L663,D690,R487,U811,L190,U780,L758,U267,R155,D344,L133,D137,R93,D229,L729,U878,L889,D603,R288,U890,R251,U531,L249,D995,R863,D257,R655,D311,R874,U356,L833,U151,L741,U246,R694,D899,L48,U915,L900,U757,L861,U402,R971,U537,R460,D844,R54,U956,L151,U74,R892,U248,R677,D881,R99,D931,R427";
    }
}