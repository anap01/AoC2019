using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AdventOfCode
{
    [TestClass]
    public class Day4
    {
        public TestContext TestContext { get; set; }

        [TestMethod]
        public void Day4Method1()
        {
            var match = 0;
            for (int i = 240920; i <= 789857; i++)
            {
                var s = i.ToString();
                if (Regex.IsMatch(s, @"(\d)\1") == false)
                    continue;
                var first = int.Parse(s[0].ToString());
                var b = true;
                for (int j = 1; j < s.Length; j++)
                {
                    var next = int.Parse(s[j].ToString());
                    if (next >= first)
                        first = next;
                    else
                    {
                        b = false;
                        break;
                    }

                }

                if (b)
                    match++;
            }
            TestContext.WriteLine(match.ToString());
        }
        
        [TestMethod]
        public void Day4Method2()
        {
            var match = 0;
            for (int i = 240920; i <= 789857; i++)
            {
                var s = i.ToString();
                if (Regex.IsMatch(s, @"(?=(\d)\1)(?<!\1)\1\1(?!\1)") == false)
                    continue;
                var first = int.Parse(s[0].ToString());
                var b = true;
                for (int j = 1; j < s.Length; j++)
                {
                    var next = int.Parse(s[j].ToString());
                    if (next >= first)
                        first = next;
                    else
                    {
                        b = false;
                        break;
                    }

                }

                if (b)
                    match++;
            }
            TestContext.WriteLine(match.ToString());
        }
    }
}