using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AdventOfCode
{
    [TestClass]
    public class Day9
    {
        public TestContext TestContext { get; set; }
            
        [TestMethod]
        public void Part1()
        {
            var intCodeComputer = new IntCodeComputer(Day9Input, TestContext);
            intCodeComputer.IntCode(1);
        }

        [TestMethod]
        public void Part2()
        {

            var intCodeComputer = new IntCodeComputer(Day9Input, TestContext);
            intCodeComputer.IntCode(2);
        }

        private readonly long[] Day5TestInput1 = {3, 9, 8, 9, 10, 9, 4, 9, 99, -1, 8};
        private readonly long[] Day5TestInput2 = {3,9,7,9,10,9,4,9,99,-1,8};
        private readonly long[] Day5TestInput3 = {3,3,1108,-1,8,3,4,3,99};
        private readonly long[] Day5TestInput4 = {3,3,1107,-1,8,3,4,3,99};

        private readonly long[] Day9TestInput1 =
            {109, 1, 204, -1, 1001, 100, 1, 100, 1008, 100, 16, 101, 1006, 101, 0, 99};

        private readonly long[] Day9TestInput2 = {1102,34915192,34915192,7,4,7,99,0 };
        private readonly long[] Day9TestInput3 = { 104,1125899906842624,99};

        private long[] Day9Input =
        {
            1102, 34463338, 34463338, 63, 1007, 63, 34463338, 63, 1005, 63, 53, 1101, 3, 0, 1000, 109, 988, 209, 12, 9,
            1000, 209, 6, 209, 3, 203, 0, 1008, 1000, 1, 63, 1005, 63, 65, 1008, 1000, 2, 63, 1005, 63, 904, 1008, 1000,
            0, 63, 1005, 63, 58, 4, 25, 104, 0, 99, 4, 0, 104, 0, 99, 4, 17, 104, 0, 99, 0, 0, 1101, 25, 0, 1016, 1102,
            760, 1, 1023, 1102, 1, 20, 1003, 1102, 1, 22, 1015, 1102, 1, 34, 1000, 1101, 0, 32, 1006, 1101, 21, 0, 1017,
            1102, 39, 1, 1010, 1101, 30, 0, 1005, 1101, 0, 1, 1021, 1101, 0, 0, 1020, 1102, 1, 35, 1007, 1102, 1, 23,
            1014, 1102, 1, 29, 1019, 1101, 767, 0, 1022, 1102, 216, 1, 1025, 1102, 38, 1, 1011, 1101, 778, 0, 1029,
            1102, 1, 31, 1009, 1101, 0, 28, 1004, 1101, 33, 0, 1008, 1102, 1, 444, 1027, 1102, 221, 1, 1024, 1102, 1,
            451, 1026, 1101, 787, 0, 1028, 1101, 27, 0, 1018, 1101, 0, 24, 1013, 1102, 26, 1, 1012, 1101, 0, 36, 1002,
            1102, 37, 1, 1001, 109, 28, 21101, 40, 0, -9, 1008, 1019, 41, 63, 1005, 63, 205, 1001, 64, 1, 64, 1105, 1,
            207, 4, 187, 1002, 64, 2, 64, 109, -9, 2105, 1, 5, 4, 213, 1106, 0, 225, 1001, 64, 1, 64, 1002, 64, 2, 64,
            109, -9, 1206, 10, 243, 4, 231, 1001, 64, 1, 64, 1105, 1, 243, 1002, 64, 2, 64, 109, -3, 1208, 2, 31, 63,
            1005, 63, 261, 4, 249, 1106, 0, 265, 1001, 64, 1, 64, 1002, 64, 2, 64, 109, 5, 21108, 41, 41, 0, 1005, 1012,
            287, 4, 271, 1001, 64, 1, 64, 1105, 1, 287, 1002, 64, 2, 64, 109, 6, 21102, 42, 1, -5, 1008, 1013, 45, 63,
            1005, 63, 307, 1105, 1, 313, 4, 293, 1001, 64, 1, 64, 1002, 64, 2, 64, 109, -9, 1201, 0, 0, 63, 1008, 63,
            29, 63, 1005, 63, 333, 1106, 0, 339, 4, 319, 1001, 64, 1, 64, 1002, 64, 2, 64, 109, -13, 2102, 1, 4, 63,
            1008, 63, 34, 63, 1005, 63, 361, 4, 345, 1105, 1, 365, 1001, 64, 1, 64, 1002, 64, 2, 64, 109, 5, 1201, 7, 0,
            63, 1008, 63, 33, 63, 1005, 63, 387, 4, 371, 1105, 1, 391, 1001, 64, 1, 64, 1002, 64, 2, 64, 109, 7, 1202,
            1, 1, 63, 1008, 63, 32, 63, 1005, 63, 411, 1105, 1, 417, 4, 397, 1001, 64, 1, 64, 1002, 64, 2, 64, 109, 20,
            1205, -7, 431, 4, 423, 1106, 0, 435, 1001, 64, 1, 64, 1002, 64, 2, 64, 109, 2, 2106, 0, -3, 1001, 64, 1, 64,
            1105, 1, 453, 4, 441, 1002, 64, 2, 64, 109, -7, 21101, 43, 0, -9, 1008, 1014, 43, 63, 1005, 63, 479, 4, 459,
            1001, 64, 1, 64, 1105, 1, 479, 1002, 64, 2, 64, 109, -5, 21108, 44, 43, 0, 1005, 1018, 495, 1105, 1, 501, 4,
            485, 1001, 64, 1, 64, 1002, 64, 2, 64, 109, -7, 1205, 9, 517, 1001, 64, 1, 64, 1105, 1, 519, 4, 507, 1002,
            64, 2, 64, 109, 11, 1206, -1, 531, 1106, 0, 537, 4, 525, 1001, 64, 1, 64, 1002, 64, 2, 64, 109, -15, 1208,
            0, 36, 63, 1005, 63, 557, 1001, 64, 1, 64, 1106, 0, 559, 4, 543, 1002, 64, 2, 64, 109, 7, 2101, 0, -7, 63,
            1008, 63, 35, 63, 1005, 63, 581, 4, 565, 1106, 0, 585, 1001, 64, 1, 64, 1002, 64, 2, 64, 109, -3, 21107, 45,
            46, 4, 1005, 1015, 607, 4, 591, 1001, 64, 1, 64, 1105, 1, 607, 1002, 64, 2, 64, 109, -16, 2102, 1, 10, 63,
            1008, 63, 31, 63, 1005, 63, 631, 1001, 64, 1, 64, 1106, 0, 633, 4, 613, 1002, 64, 2, 64, 109, 1, 2107, 33,
            10, 63, 1005, 63, 649, 1106, 0, 655, 4, 639, 1001, 64, 1, 64, 1002, 64, 2, 64, 109, 17, 2101, 0, -9, 63,
            1008, 63, 31, 63, 1005, 63, 679, 1001, 64, 1, 64, 1106, 0, 681, 4, 661, 1002, 64, 2, 64, 109, -6, 2107, 34,
            0, 63, 1005, 63, 703, 4, 687, 1001, 64, 1, 64, 1106, 0, 703, 1002, 64, 2, 64, 109, 5, 1207, -5, 34, 63,
            1005, 63, 719, 1105, 1, 725, 4, 709, 1001, 64, 1, 64, 1002, 64, 2, 64, 109, -15, 1202, 6, 1, 63, 1008, 63,
            20, 63, 1005, 63, 751, 4, 731, 1001, 64, 1, 64, 1105, 1, 751, 1002, 64, 2, 64, 109, 21, 2105, 1, 5, 1001,
            64, 1, 64, 1106, 0, 769, 4, 757, 1002, 64, 2, 64, 109, 5, 2106, 0, 5, 4, 775, 1001, 64, 1, 64, 1106, 0, 787,
            1002, 64, 2, 64, 109, -27, 1207, 4, 35, 63, 1005, 63, 809, 4, 793, 1001, 64, 1, 64, 1106, 0, 809, 1002, 64,
            2, 64, 109, 13, 2108, 33, -1, 63, 1005, 63, 831, 4, 815, 1001, 64, 1, 64, 1106, 0, 831, 1002, 64, 2, 64,
            109, 4, 21107, 46, 45, 1, 1005, 1014, 851, 1001, 64, 1, 64, 1105, 1, 853, 4, 837, 1002, 64, 2, 64, 109, 3,
            21102, 47, 1, -3, 1008, 1013, 47, 63, 1005, 63, 875, 4, 859, 1106, 0, 879, 1001, 64, 1, 64, 1002, 64, 2, 64,
            109, -9, 2108, 28, 2, 63, 1005, 63, 895, 1106, 0, 901, 4, 885, 1001, 64, 1, 64, 4, 64, 99, 21101, 27, 0, 1,
            21102, 1, 915, 0, 1106, 0, 922, 21201, 1, 59074, 1, 204, 1, 99, 109, 3, 1207, -2, 3, 63, 1005, 63, 964,
            21201, -2, -1, 1, 21102, 942, 1, 0, 1105, 1, 922, 21201, 1, 0, -1, 21201, -2, -3, 1, 21102, 1, 957, 0, 1105,
            1, 922, 22201, 1, -1, -2, 1106, 0, 968, 22102, 1, -2, -2, 109, -3, 2105, 1, 0
        };

    }

    public class IntCodeComputer
    {
        private AutoinsertingDictionary<long, long> m_program;
        private readonly long[] m_originalInput;
        private long m_relativeBase = 0;
        private TestContext TestContext { get; }
        
        public IntCodeComputer(long[] originalInput, TestContext testContext)
        {
            m_originalInput = originalInput;
            TestContext = testContext;
            Reset();
        }

        public void IntCode(params int[] input)
        {
            var i = 0L;
            var inputIndex = 0;
            var op = m_program[i] % 100;
            while (op != 99)
            {
                var modes = m_program[i] / 100;
                switch (op)
                {
                    case 1:
                        var term11 = GetTerm(i + 1, modes % 10);
                        modes /= 10;
                        var term12 = GetTerm(i + 2, modes % 10);
                        modes /= 10;
                        Write(term11 + term12, i + 3, modes % 10);
                        i += 4;
                        break;
                    case 2:
                        var term21 = GetTerm(i + 1, modes % 10);
                        modes /= 10;
                        var term22 = GetTerm(i + 2, modes % 10);
                        modes /= 10;
                        Write(term21 * term22, i + 3, modes % 10);
                        i += 4;
                        break;
                    case 3:
                        Write(input[inputIndex++], i + 1, modes % 10);
                        i += 2;
                        break;
                    case 4:
                        var result = GetTerm(i + 1, modes % 10);
                        TestContext.WriteLine(result.ToString());
                        i += 2;
                        break;
                    case 5:
                        var param51 = GetTerm(i + 1, modes % 10);
                        if (param51 != 0)
                        {
                            modes /= 10;
                            var param52 = GetTerm(i + 2, modes % 10);
                            i = param52;
                        }
                        else
                        {
                            i += 3;
                        }

                        break;
                    case 6:
                        var param61 = GetTerm(i + 1, modes % 10);
                        if (param61 == 0)
                        {
                            modes /= 10;
                            var param62 = GetTerm(i + 2, modes % 10);
                            i = param62;
                        }
                        else
                        {
                            i += 3;
                        }

                        break;
                    case 7:
                        var param71 = GetTerm(i + 1, modes % 10);
                        modes /= 10;
                        var param72 = GetTerm(i + 2, modes % 10);
                        modes /= 10;
                        Write(param71 < param72 ? 1 : 0, i + 3, modes % 10);
                        i += 4;
                        break;
                    case 8:
                        var param81 = GetTerm(i + 1, modes % 10);
                        modes /= 10;
                        var param82 = GetTerm(i + 2, modes % 10);
                        modes /= 10;
                        Write(param81 == param82 ? 1 : 0, i + 3, modes % 10);
                        i += 4;
                        break;
                    case 9:
                        var param9 = GetTerm(i + 1, modes % 10);
                        m_relativeBase += param9;
                        i += 2;
                        break;
                }

                op = m_program[i] % 100;
            }
        }

        private void Write(long value, long i, long mode)
        {
            switch (mode)
            {
                case 0:
                    m_program[m_program[i]] = value;
                    break;
                case 2:
                    m_program[m_program[i] + m_relativeBase] = value;
                    break;
            }
        }

        private long GetTerm(long i, long mode)
        {
            switch (mode)
            {
                case 0:
                    return m_program[m_program[i]];
                case 1:
                    return m_program[i];
                case 2:
                    return m_program[m_program[i] + m_relativeBase];
                default:
                    throw new ArgumentException($"Unknown mode '{mode}'", nameof(mode));
            }
        }

        public void Reset()
        {
            m_program = new AutoinsertingDictionary<long, long>();
            foreach (var (key, value) in m_originalInput.Select((value, key) => (key, value)))
            {
                m_program.Add(key, value);
            }
        }
    }
}