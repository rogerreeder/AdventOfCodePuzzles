using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCodePuzzles
{
    static public class Day09
    {
        const string FILENAME = "Assets/Day09.demo.txt";
        private static List<string>? lines;
        public static string Part1()
        {
            if(lines == null)
                lines = File.ReadLines(FILENAME).ToList();
            return $"Part1:"
                + $"\n\tNot Implemented";
        }

        public static string Part2()
        {
            return $"Part2:"
                + $"\n\tNot Implemented";
        }

    }
}
