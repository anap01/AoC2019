using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AdventOfCode
{
    [TestClass]
    public class Day7
    {
        public TestContext TestContext { get; set; }

        [TestMethod]
        public void Part1()
        {
            m_currentInput = Day7Input;
            var max = 0;
            foreach (var enumeration in GetPermutations(new List<int>{0, 1, 2, 3, 4}, 5))
            {
                var ints = enumeration.ToList();
                max = Math.Max(max, TestPhase(ints[0], ints[1], ints[2], ints[3], ints[4]));
            }

            TestContext.WriteLine(max.ToString());
        }
        
        [TestMethod]
        public void Part2()
        {
            m_currentInput = Day7Input;

            var max = 0;
            foreach (var enumeration in GetPermutations(new List<int>{5, 6, 7, 8, 9}, 5))
            {
                var ints = enumeration.ToList();
                max = Math.Max(max, TestPhaseWithFeedback(ints[0], ints[1], ints[2], ints[3], ints[4]));
            }

            TestContext.WriteLine(max.ToString());
        }



        static IEnumerable<IEnumerable<T>>
            GetPermutations<T>(IEnumerable<T> list, int length)
        {
            if (length == 1) return list.Select(t => new T[] { t });

            return GetPermutations(list, length - 1)
                .SelectMany(t => list.Where(e => !t.Contains(e)),
                    (t1, t2) => t1.Concat(new T[] { t2 }));
        }
      
        private int TestPhase(int phase1, int phase2, int phase3, int phase4, int phase5)
        {
            var output1 = IntCode(phase1, 0);
            var output2 = IntCode(phase2, output1);
            var output3 = IntCode(phase3, output2);
            var output4 = IntCode(phase4, output3);
            var output5 = IntCode(phase5, output4);
            return output5;
        }
        
        private int TestPhaseWithFeedback(int phase1, int phase2, int phase3, int phase4, int phase5)
        {
            var amp1 = new IntCodeStateMachine(m_currentInput, phase1);
            var amp2 = new IntCodeStateMachine(m_currentInput, phase2);
            var amp3 = new IntCodeStateMachine(m_currentInput, phase3);
            var amp4 = new IntCodeStateMachine(m_currentInput, phase4);
            var amp5 = new IntCodeStateMachine(m_currentInput, phase5);
            var input = 0;
            
            while (true)
            {
                var output1 = amp1.IntCode(input);
                if (output1 == -1)
                    break;
                var output2 = amp2.IntCode(output1);
                if (output2 == -1)
                    break;
                var output3 = amp3.IntCode(output2);
                if (output3 == -1)
                    break;
                var output4 = amp4.IntCode(output3);
                if (output4 == -1)
                    break;
                input = amp5.IntCode(output4);
            }
            
            return input;
        }

        public class IntCodeStateMachine
        {
            private readonly int[] m_originalInput;
            private readonly int m_phase;
            private int[] m_ints;
            private int m_index;
            private int m_op;
            private bool m_setPhase;

            public IntCodeStateMachine(int[] input, int phase)
            {
                m_originalInput = input;
                m_phase = phase;
                m_setPhase = true;
                m_ints = (int[]) m_originalInput.Clone();
                m_index = 0;
            }

                    public int IntCode(int input)
        {
            m_op = m_ints[m_index] % 100;
            while (m_op != 99)
            {
                var modes = m_ints[m_index] / 100;
                switch (m_op)
                {
                    case 1:
                        var term11 = modes % 10 == 1 ? m_ints[m_index + 1] : m_ints[m_ints[m_index + 1]];
                        modes /= 10;
                        var term12 = modes % 10 == 1 ? m_ints[m_index + 2] : m_ints[m_ints[m_index + 2]];
                        m_ints[m_ints[m_index + 3]] = term11 + term12;
                        m_index += 4;
                        break;
                    case 2:
                        var term21 = modes % 10 == 1 ? m_ints[m_index + 1] : m_ints[m_ints[m_index + 1]];
                        modes /= 10;
                        var term22 = modes % 10 == 1 ? m_ints[m_index + 2] : m_ints[m_ints[m_index + 2]];
                        m_ints[m_ints[m_index + 3]] = term21 * term22;
                        m_index += 4;
                        break;
                    case 3:
                        if (m_setPhase)
                            m_ints[m_ints[m_index + 1]] = m_phase;
                        else
                            m_ints[m_ints[m_index + 1]] = input;
                        m_setPhase = false;
                        m_index += 2;
                        break;
                    case 4:
                        var result = modes % 10 == 1 ? m_ints[m_index + 1] : m_ints[m_ints[m_index + 1]];
                        m_index += 2;
                        return result;
                    case 5:
                        var param51 = modes % 10 == 1 ? m_ints[m_index + 1] : m_ints[m_ints[m_index + 1]];
                        if (param51 != 0)
                        {
                            modes /= 10;
                            var param52 = modes % 10 == 1 ? m_ints[m_index + 2] : m_ints[m_ints[m_index + 2]];
                            m_index = param52;
                        }
                        else
                        {
                            m_index += 3;
                        }

                        break;
                    case 6:
                        var param61 = modes % 10 == 1 ? m_ints[m_index + 1] : m_ints[m_ints[m_index + 1]];
                        if (param61 == 0)
                        {
                            modes /= 10;
                            var param62 = modes % 10 == 1 ? m_ints[m_index + 2] : m_ints[m_ints[m_index + 2]];
                            m_index = param62;
                        }
                        else
                        {
                            m_index += 3;
                        }

                        break;
                    case 7:
                        var param71 = modes % 10 == 1 ? m_ints[m_index + 1] : m_ints[m_ints[m_index + 1]];
                        modes /= 10;
                        var param72 = modes % 10 == 1 ? m_ints[m_index + 2] : m_ints[m_ints[m_index + 2]];
                        m_ints[m_ints[m_index + 3]] = param71 < param72 ? 1 : 0;
                        m_index += 4;
                        break;
                    case 8:
                        var param81 = modes % 10 == 1 ? m_ints[m_index + 1] : m_ints[m_ints[m_index + 1]];
                        modes /= 10;
                        var param82 = modes % 10 == 1 ? m_ints[m_index + 2] : m_ints[m_ints[m_index + 2]];
                        m_ints[m_ints[m_index + 3]] = param81 == param82 ? 1 : 0;
                        m_index += 4;
                        break;
                }

                m_op = m_ints[m_index] % 100;
            }
            return -1;
        }

        }
        private int IntCode(int phase, int input)
        {
            var a = (int[]) m_currentInput.Clone();
            var i = 0;
            var setPhase = true;
            var op = a[i] % 100;
            while (op != 99)
            {
                var modes = a[i] / 100;
                switch (op)
                {
                    case 1:
                        var term11 = modes % 10 == 1 ? a[i + 1] : a[a[i + 1]];
                        modes /= 10;
                        var term12 = modes % 10 == 1 ? a[i + 2] : a[a[i + 2]];
                        a[a[i + 3]] = term11 + term12;
                        i += 4;
                        break;
                    case 2:
                        var term21 = modes % 10 == 1 ? a[i + 1] : a[a[i + 1]];
                        modes /= 10;
                        var term22 = modes % 10 == 1 ? a[i + 2] : a[a[i + 2]];
                        a[a[i + 3]] = term21 * term22;
                        i += 4;
                        break;
                    case 3:
                        if (setPhase)
                            a[a[i + 1]] = phase;
                        else
                            a[a[i + 1]] = input;
                        setPhase = false;
                        i += 2;
                        break;
                    case 4:
                        return modes % 10 == 1 ? a[i + 1] : a[a[i + 1]];
                    case 5:
                        var param51 = modes % 10 == 1 ? a[i + 1] : a[a[i + 1]];
                        if (param51 != 0)
                        {
                            modes /= 10;
                            var param52 = modes % 10 == 1 ? a[i + 2] : a[a[i + 2]];
                            i = param52;
                        }
                        else
                        {
                            i += 3;
                        }

                        break;
                    case 6:
                        var param61 = modes % 10 == 1 ? a[i + 1] : a[a[i + 1]];
                        if (param61 == 0)
                        {
                            modes /= 10;
                            var param62 = modes % 10 == 1 ? a[i + 2] : a[a[i + 2]];
                            i = param62;
                        }
                        else
                        {
                            i += 3;
                        }

                        break;
                    case 7:
                        var param71 = modes % 10 == 1 ? a[i + 1] : a[a[i + 1]];
                        modes /= 10;
                        var param72 = modes % 10 == 1 ? a[i + 2] : a[a[i + 2]];
                        a[a[i + 3]] = param71 < param72 ? 1 : 0;
                        i += 4;
                        break;
                    case 8:
                        var param81 = modes % 10 == 1 ? a[i + 1] : a[a[i + 1]];
                        modes /= 10;
                        var param82 = modes % 10 == 1 ? a[i + 2] : a[a[i + 2]];
                        a[a[i + 3]] = param81 == param82 ? 1 : 0;
                        i += 4;
                        break;
                }

                op = a[i] % 100;
            }
            throw new Exception("Unexpected end");
        }

        private readonly int[] Day7TestInput1 = {3,15,3,16,1002,16,10,16,1,16,15,15,4,15,99,0,0};
        private readonly int[] Day7TestInput2 = {3,23,3,24,1002,24,10,24,1002,23,-1,23,
            101,5,23,23,1,24,23,23,4,23,99,0,0};

        private readonly int[] Day7TestInput3 =
        {
            3, 31, 3, 32, 1002, 32, 10, 32, 1001, 31, -2, 31, 1007, 31, 0, 33,
            1002, 33, 7, 33, 1, 33, 31, 31, 1, 32, 31, 31, 4, 31, 99, 0, 0, 0
        };
        private readonly int[] Day7TestInput4 = {3,26,1001,26,-4,26,3,27,1002,27,2,27,1,27,26,
            27,4,27,1001,28,-1,28,1005,28,6,99,0,0,5};
        private readonly int[] Day5TestInput4 = {3,3,1107,-1,8,3,4,3,99};
        private readonly int[] Day5TestInput5 = {3,12,6,12,15,1,13,14,13,4,13,99,-1,0,1,9};
        private readonly int[] Day5TestInput6 = {3,3,1105,-1,9,1101,0,0,12,4,12,99,1};
        private readonly int[] Day5TestInput7 = {
            3, 21, 1008, 21, 8, 20, 1005, 20, 22, 107, 8, 21, 20, 1006, 20, 31,
            1106, 0, 36, 98, 0, 0, 1002, 21, 125, 20, 4, 20, 1105, 1, 46, 104,
            999, 1105, 1, 46, 1101, 1000, 1, 20, 4, 20, 1105, 1, 46, 98, 99
        };

        private readonly int[] Day7Input = new int[]
        {
            3, 8, 1001, 8, 10, 8, 105, 1, 0, 0, 21, 42, 67, 88, 101, 114, 195, 276, 357, 438, 99999, 3, 9, 101, 3, 9, 9,
            1002, 9, 4, 9, 1001, 9, 5, 9, 102, 4, 9, 9, 4, 9, 99, 3, 9, 1001, 9, 3, 9, 1002, 9, 2, 9, 101, 2, 9, 9, 102,
            2, 9, 9, 1001, 9, 5, 9, 4, 9, 99, 3, 9, 102, 4, 9, 9, 1001, 9, 3, 9, 102, 4, 9, 9, 101, 4, 9, 9, 4, 9, 99,
            3, 9, 101, 2, 9, 9, 1002, 9, 3, 9, 4, 9, 99, 3, 9, 101, 4, 9, 9, 1002, 9, 5, 9, 4, 9, 99, 3, 9, 102, 2, 9,
            9, 4, 9, 3, 9, 1001, 9, 1, 9, 4, 9, 3, 9, 101, 1, 9, 9, 4, 9, 3, 9, 1001, 9, 1, 9, 4, 9, 3, 9, 101, 1, 9, 9,
            4, 9, 3, 9, 1002, 9, 2, 9, 4, 9, 3, 9, 101, 1, 9, 9, 4, 9, 3, 9, 1002, 9, 2, 9, 4, 9, 3, 9, 102, 2, 9, 9, 4,
            9, 3, 9, 1002, 9, 2, 9, 4, 9, 99, 3, 9, 102, 2, 9, 9, 4, 9, 3, 9, 1002, 9, 2, 9, 4, 9, 3, 9, 1001, 9, 1, 9,
            4, 9, 3, 9, 1002, 9, 2, 9, 4, 9, 3, 9, 1002, 9, 2, 9, 4, 9, 3, 9, 1001, 9, 2, 9, 4, 9, 3, 9, 1001, 9, 2, 9,
            4, 9, 3, 9, 1001, 9, 2, 9, 4, 9, 3, 9, 1002, 9, 2, 9, 4, 9, 3, 9, 101, 1, 9, 9, 4, 9, 99, 3, 9, 102, 2, 9,
            9, 4, 9, 3, 9, 1002, 9, 2, 9, 4, 9, 3, 9, 1001, 9, 2, 9, 4, 9, 3, 9, 102, 2, 9, 9, 4, 9, 3, 9, 1001, 9, 2,
            9, 4, 9, 3, 9, 101, 2, 9, 9, 4, 9, 3, 9, 1001, 9, 1, 9, 4, 9, 3, 9, 101, 1, 9, 9, 4, 9, 3, 9, 101, 2, 9, 9,
            4, 9, 3, 9, 1001, 9, 1, 9, 4, 9, 99, 3, 9, 102, 2, 9, 9, 4, 9, 3, 9, 101, 1, 9, 9, 4, 9, 3, 9, 1001, 9, 1,
            9, 4, 9, 3, 9, 101, 1, 9, 9, 4, 9, 3, 9, 101, 1, 9, 9, 4, 9, 3, 9, 101, 1, 9, 9, 4, 9, 3, 9, 1001, 9, 2, 9,
            4, 9, 3, 9, 101, 2, 9, 9, 4, 9, 3, 9, 1002, 9, 2, 9, 4, 9, 3, 9, 1001, 9, 1, 9, 4, 9, 99, 3, 9, 1001, 9, 2,
            9, 4, 9, 3, 9, 102, 2, 9, 9, 4, 9, 3, 9, 1002, 9, 2, 9, 4, 9, 3, 9, 1002, 9, 2, 9, 4, 9, 3, 9, 1002, 9, 2,
            9, 4, 9, 3, 9, 1002, 9, 2, 9, 4, 9, 3, 9, 1002, 9, 2, 9, 4, 9, 3, 9, 1002, 9, 2, 9, 4, 9, 3, 9, 101, 2, 9,
            9, 4, 9, 3, 9, 101, 2, 9, 9, 4, 9, 99
        };
        
        private int[] m_currentInput;
    }
}