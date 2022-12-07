using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCodePuzzles
{
    static public class Day06
    {
        const string FILENAME = "Assets/Day06.txt";
        private static List<string>? lines;
        public static string Part1()
        {
            var marker = 0;
            if (lines == null)
                lines = File.ReadLines(FILENAME).ToList();

            foreach (var line in lines)
            {
                Console.WriteLine($"{line}");
                marker = GetMarker(line, 4);
                Console.WriteLine($"\t{marker}");
            }
            return $"Part1: {marker}";
        }

        public static string Part2()
        {
            var marker = 0;
            if (lines == null)
                lines = File.ReadLines(FILENAME).ToList();

            foreach (var line in lines)
            {
                Console.WriteLine($"{line}");
                marker = GetMarker(line, 4);
                Console.WriteLine($"\t{marker}");
                marker += GetMarker(line.Substring(marker + 1), 14);
                Console.WriteLine($"\t{marker + 1}");
            }
            return $"Part2: {marker}";
        }


        private static int GetMarker(string line, int length)
        {
            for (var i = 0; i < line.Length - length + 1; i++)
            {
                if(CheckUnique(line.Substring(i,length)))
                    return i + length;
            }
            return 0;
        }

        private static bool CheckUnique(string str)
        {
            string one = "";
            string two = "";
            for (int i = 0; i < str.Length; i++)
            {
                one = str.Substring(i, 1);
                for (int j = 0; j < str.Length; j++)
                {
                    two = str.Substring(j, 1);
                    if ((one == two) && (i != j))
                        return false;
                }
            }
            Console.WriteLine($"\t{str}");
            return true;
        }
    }
}
