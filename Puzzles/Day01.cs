using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCodePuzzles
{
    static public class Day01
    {
        public static string MaxCalories()
        {
            var elf = 1;
            var largestSum = 0;
            var lines = File.ReadLines("Assets/Day01.txt").ToList();
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
            return $"First Puzzle: larget calories is {largestSum}";
        }

        public static string Top3GroupOfCalories()
        {
            var lines = File.ReadLines("Assets/Day01.txt").ToList();
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
            sum = 0;
            foreach (var calories in totals)
                sum += calories;
            return $"Second Puzzle: Top 3 total is {sum}";
        }
    }
}
