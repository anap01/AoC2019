using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AdventOfCode
{
    public enum Direction : byte
    {
        Up, Right, Down, Left
    }
    
    [TestClass]
    public class Day11
    {
        public TestContext TestContext { get; set; }
            
        [TestMethod]
        public void Part1()
        {
            var intCodeComputer = new IntCodeComputerDay11(Day11Input, TestContext);
            var input = 0L;
            var track = CalculateTrack(input, intCodeComputer);
            TestContext.WriteLine(track.Count.ToString());
        }

        private static Dictionary<(int x, int y), long> CalculateTrack(long input, IntCodeComputerDay11 intCodeComputer)
        {
            var track = new Dictionary<(int x, int y), long>();
            (int x, int y) location = (0, 0);
            var direction = Direction.Up;
            while (input != -1)
            {
                var output = intCodeComputer.IntCode(input);
                track[location] = output;
                output = intCodeComputer.IntCode();
                if (output == 0)
                    direction = (Direction) ((byte) ((byte) direction - 1) % 4);
                else
                    direction = (Direction) ((byte) ((byte) direction + 1) % 4);
                switch (direction)
                {
                    case Direction.Up:
                        location.y++;
                        break;
                    case Direction.Right:
                        location.x++;
                        break;
                    case Direction.Down:
                        location.y--;
                        break;
                    case Direction.Left:
                        location.x--;
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }

                if (track.TryGetValue(location, out input) == false)
                    input = 0;
            }

            return track;
        }

        [TestMethod]
        public void Part2()
        {
            var intCodeComputer = new IntCodeComputerDay11(Day11Input, TestContext);
            var input = 1L;
            var track = CalculateTrack(input, intCodeComputer);
            var trackByRows = track.GroupBy(kvp => kvp.Key.y);
            var stringBuilder = new StringBuilder();
            foreach (var row in trackByRows)
            {
                var ubound = row.Max(kvp => kvp.Key.x);
                var valueByX = row.ToDictionary(k => k.Key.x, v => v.Value);
                
                for (var i = 0; i < ubound; i++)
                {
                    if (valueByX.ContainsKey(i))
                        stringBuilder.Append(valueByX[i] == 0 ? " " : "X");
                    else
                    {
                        stringBuilder.Append(" ");
                    }
                }

                stringBuilder.AppendLine();
            }
            TestContext.WriteLine(stringBuilder.ToString());
        }

        private long[] Day11Input =
        {
            3, 8, 1005, 8, 318, 1106, 0, 11, 0, 0, 0, 104, 1, 104, 0, 3, 8, 1002, 8, -1, 10, 1001, 10, 1, 10, 4, 10,
            108, 1, 8, 10, 4, 10, 1002, 8, 1, 28, 1, 107, 14, 10, 1, 107, 18, 10, 3, 8, 102, -1, 8, 10, 101, 1, 10, 10,
            4, 10, 108, 1, 8, 10, 4, 10, 102, 1, 8, 58, 1006, 0, 90, 2, 1006, 20, 10, 3, 8, 1002, 8, -1, 10, 101, 1, 10,
            10, 4, 10, 1008, 8, 1, 10, 4, 10, 1001, 8, 0, 88, 2, 103, 2, 10, 2, 4, 7, 10, 3, 8, 1002, 8, -1, 10, 101, 1,
            10, 10, 4, 10, 1008, 8, 1, 10, 4, 10, 1001, 8, 0, 118, 1, 1009, 14, 10, 1, 1103, 9, 10, 3, 8, 1002, 8, -1,
            10, 1001, 10, 1, 10, 4, 10, 108, 0, 8, 10, 4, 10, 1002, 8, 1, 147, 1006, 0, 59, 1, 104, 4, 10, 2, 106, 18,
            10, 3, 8, 102, -1, 8, 10, 1001, 10, 1, 10, 4, 10, 1008, 8, 0, 10, 4, 10, 101, 0, 8, 181, 2, 4, 17, 10, 1006,
            0, 36, 1, 107, 7, 10, 2, 1008, 0, 10, 3, 8, 1002, 8, -1, 10, 1001, 10, 1, 10, 4, 10, 108, 0, 8, 10, 4, 10,
            101, 0, 8, 217, 3, 8, 102, -1, 8, 10, 1001, 10, 1, 10, 4, 10, 1008, 8, 0, 10, 4, 10, 101, 0, 8, 240, 1006,
            0, 64, 3, 8, 102, -1, 8, 10, 1001, 10, 1, 10, 4, 10, 108, 0, 8, 10, 4, 10, 1002, 8, 1, 264, 3, 8, 1002, 8,
            -1, 10, 1001, 10, 1, 10, 4, 10, 1008, 8, 1, 10, 4, 10, 1001, 8, 0, 287, 1, 1104, 15, 10, 1, 102, 8, 10,
            1006, 0, 2, 101, 1, 9, 9, 1007, 9, 940, 10, 1005, 10, 15, 99, 109, 640, 104, 0, 104, 1, 21102, 932700857236,
            1, 1, 21101, 335, 0, 0, 1106, 0, 439, 21101, 0, 387511792424, 1, 21101, 346, 0, 0, 1106, 0, 439, 3, 10, 104,
            0, 104, 1, 3, 10, 104, 0, 104, 0, 3, 10, 104, 0, 104, 1, 3, 10, 104, 0, 104, 1, 3, 10, 104, 0, 104, 0, 3,
            10, 104, 0, 104, 1, 21101, 46372252675, 0, 1, 21102, 393, 1, 0, 1106, 0, 439, 21101, 97806162983, 0, 1,
            21102, 404, 1, 0, 1105, 1, 439, 3, 10, 104, 0, 104, 0, 3, 10, 104, 0, 104, 0, 21102, 1, 825452438376, 1,
            21101, 0, 427, 0, 1106, 0, 439, 21102, 709475586836, 1, 1, 21101, 0, 438, 0, 1106, 0, 439, 99, 109, 2,
            22101, 0, -1, 1, 21101, 40, 0, 2, 21102, 1, 470, 3, 21102, 1, 460, 0, 1106, 0, 503, 109, -2, 2106, 0, 0, 0,
            1, 0, 0, 1, 109, 2, 3, 10, 204, -1, 1001, 465, 466, 481, 4, 0, 1001, 465, 1, 465, 108, 4, 465, 10, 1006, 10,
            497, 1101, 0, 0, 465, 109, -2, 2105, 1, 0, 0, 109, 4, 2102, 1, -1, 502, 1207, -3, 0, 10, 1006, 10, 520,
            21102, 1, 0, -3, 21202, -3, 1, 1, 21202, -2, 1, 2, 21101, 0, 1, 3, 21101, 0, 539, 0, 1106, 0, 544, 109, -4,
            2105, 1, 0, 109, 5, 1207, -3, 1, 10, 1006, 10, 567, 2207, -4, -2, 10, 1006, 10, 567, 22101, 0, -4, -4, 1106,
            0, 635, 21202, -4, 1, 1, 21201, -3, -1, 2, 21202, -2, 2, 3, 21102, 586, 1, 0, 1105, 1, 544, 22101, 0, 1, -4,
            21102, 1, 1, -1, 2207, -4, -2, 10, 1006, 10, 605, 21102, 0, 1, -1, 22202, -2, -1, -2, 2107, 0, -3, 10, 1006,
            10, 627, 22101, 0, -1, 1, 21102, 1, 627, 0, 106, 0, 502, 21202, -2, -1, -2, 22201, -4, -2, -4, 109, -5,
            2105, 1, 0
        };

    }

    public class IntCodeComputerDay11
    {
        private AutoinsertingDictionary<long, long> m_program;
        private readonly long[] m_originalInput;
        private long m_relativeBase = 0;
        private long m_index;
        private TestContext TestContext { get; }
        
        public IntCodeComputerDay11(long[] originalInput, TestContext testContext)
        {
            m_originalInput = originalInput;
            TestContext = testContext;
            Reset();
        }

        public long IntCode(params long[] input)
        {
            var inputIndex = 0;
            var op = m_program[m_index] % 100;
            while (op != 99)
            {
                var modes = m_program[m_index] / 100;
                switch (op)
                {
                    case 1:
                        var term11 = GetTerm(m_index + 1, modes % 10);
                        modes /= 10;
                        var term12 = GetTerm(m_index + 2, modes % 10);
                        modes /= 10;
                        Write(term11 + term12, m_index + 3, modes % 10);
                        m_index += 4;
                        break;
                    case 2:
                        var term21 = GetTerm(m_index + 1, modes % 10);
                        modes /= 10;
                        var term22 = GetTerm(m_index + 2, modes % 10);
                        modes /= 10;
                        Write(term21 * term22, m_index + 3, modes % 10);
                        m_index += 4;
                        break;
                    case 3:
                        Write(input[inputIndex++], m_index + 1, modes % 10);
                        m_index += 2;
                        break;
                    case 4:
                        var result = GetTerm(m_index + 1, modes % 10);
                        m_index += 2;
                        return result;
                    case 5:
                        var param51 = GetTerm(m_index + 1, modes % 10);
                        if (param51 != 0)
                        {
                            modes /= 10;
                            var param52 = GetTerm(m_index + 2, modes % 10);
                            m_index = param52;
                        }
                        else
                        {
                            m_index += 3;
                        }

                        break;
                    case 6:
                        var param61 = GetTerm(m_index + 1, modes % 10);
                        if (param61 == 0)
                        {
                            modes /= 10;
                            var param62 = GetTerm(m_index + 2, modes % 10);
                            m_index = param62;
                        }
                        else
                        {
                            m_index += 3;
                        }

                        break;
                    case 7:
                        var param71 = GetTerm(m_index + 1, modes % 10);
                        modes /= 10;
                        var param72 = GetTerm(m_index + 2, modes % 10);
                        modes /= 10;
                        Write(param71 < param72 ? 1 : 0, m_index + 3, modes % 10);
                        m_index += 4;
                        break;
                    case 8:
                        var param81 = GetTerm(m_index + 1, modes % 10);
                        modes /= 10;
                        var param82 = GetTerm(m_index + 2, modes % 10);
                        modes /= 10;
                        Write(param81 == param82 ? 1 : 0, m_index + 3, modes % 10);
                        m_index += 4;
                        break;
                    case 9:
                        var param9 = GetTerm(m_index + 1, modes % 10);
                        m_relativeBase += param9;
                        m_index += 2;
                        break;
                }

                op = m_program[m_index] % 100;
            }

            return -1;
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
            m_index = 0L;
        }
    }
}