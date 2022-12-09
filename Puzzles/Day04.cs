using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCodePuzzles
{
    static public class Day04
    {
        private static List<string>? lines;
        public static string Part1()
        {
            var count = 0;
            if(lines == null)
                lines = File.ReadLines("Assets/Day04.txt").ToList();
            foreach(var line in lines)
                 count += Flag(line);
            return $"Part1:\n\tCount: {count}";
        }

        public static string Part2()
        {
            var count = 0;
            if(lines == null)
                return $"No Data...";
            foreach(var line in lines)
                count += Flag2(line);
            return $"Part2:\n\tCount: {count}";
        }

        private static int Flag(string line) => ComparePairs(line.Split(',')) ? 1 : 0;
        private static int Flag2(string line) => OverlappingPairs(line.Split(',')) ? 1 : 0;
 
        private static bool ComparePairs(string[] pairs){
            var firstSections = pairs[0].Split('-');
            var secondSections = pairs[1].Split('-');
            if(
                int.Parse(firstSections[0]) <= int.Parse(secondSections[0]) &&
                int.Parse(firstSections[1]) >= int.Parse(secondSections[1]))
                return true;
            if(
                int.Parse(firstSections[0]) >= int.Parse(secondSections[0]) &&
                int.Parse(firstSections[1]) <= int.Parse(secondSections[1]))
                return true;
            return false;
        }
        private static bool OverlappingPairs(string[] pairs){
            var firstSections = pairs[0].Split('-');
            var secondSections = pairs[1].Split('-');
            if(
                int.Parse(firstSections[0]) <= int.Parse(secondSections[1]) &&
                int.Parse(firstSections[1]) >= int.Parse(secondSections[0])){
                    return true;
                }
            if(
                int.Parse(firstSections[0]) <= int.Parse(secondSections[1]) &&
                int.Parse(firstSections[1]) >= int.Parse(secondSections[0])){
                    return true;
                }
            return false;
        }
    }
}
