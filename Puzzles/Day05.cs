using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCodePuzzles
{
    static public class Day05
    {
        private static List<string>? lines;
        public static string Part1()
        {
            if(lines == null)
                lines = File.ReadLines("Assets/Day05.txt").ToList();
            return $"Part1: {false}";
        }

        public static string Part2()
        {
            if(lines == null)
                lines = File.ReadLines("Assets/Day05.txt").ToList();
            return $"Part2: {false}";
        }
    }
}
