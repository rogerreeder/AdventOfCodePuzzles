using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCodePuzzles.Year2022
{
    static public class Day01
    {
        private static List<string>? lines;
        public static string Part1()
        {
            var elf = 1;
            var largestSum = 0;
            lines = File.ReadLines("Assets/Day01.txt").ToList();
            var lineNo = 0;
            if (lines != null)
            {
                var sum = 0;
                foreach (var line in lines)
                {
                    lineNo++;
                    if (line == "")
                    {
                        if(sum > largestSum)
                        {
                            largestSum = sum;
                        }
                        elf++;
                        sum = 0;
                    }
                    else
                        sum += int.Parse(line);
                }
            }
            return $"Part1:\n\tTop Calories: {largestSum}";
        }

        public static string Part2()
        {
            var list = new List<int>();
            var totals = new int[3];
            var sum = 0;
            if (lines != null)
            {
                foreach (var line in lines)
                {
                    if (line == "")
                    {
                        if (totals[0] < sum)
                        {
                            totals[2] = totals[1];
                            totals[1] = totals[0];
                            totals[0] = sum;
                        }
                        else if (totals[1] < sum)
                        {
                            totals[2] = totals[1];
                            totals[1] = sum;
                        }
                        else if (totals[2] < sum)
                        {
                            totals[2] = sum;
                        }
                        sum = 0;
                    }
                    else
                        sum += int.Parse(line);
                }
            }
            return $"Part2:\n\tTop 3 is: {totals.Sum()}";
        }
    }
}
