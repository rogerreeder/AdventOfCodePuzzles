using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCodePuzzles
{
    static public class Day08
    {
        const string FILENAME = "Assets/Day05.txt";
        private static List<string>? lines;
        public static string Part1()
        {
            if(lines == null)
                lines = File.ReadLines(FILENAME).ToList();
            return $"Part1: {false}";
        }

        public static string Part2()
        {
            if(lines == null)
                lines = File.ReadLines(FILENAME).ToList();
            if(lines == null)
                lines = File.ReadLines(FILENAME).ToList();
            return $"Part2: {false}";
        }

    }
}
