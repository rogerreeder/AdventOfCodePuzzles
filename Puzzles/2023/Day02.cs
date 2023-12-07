using AdventOfCodePuzzles.Helpers;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCodePuzzles.Year2023
{
    static public class Day02
    {
        public static string Part1()
        {
            var filePath = @"F:\logs\20230201.log";
            if (File.Exists(filePath))
                File.Delete(filePath);
            var lines = File.ReadLines("Assets/2023/Day02.demo1.txt").ToList();
            var sum = 0;
            if (lines != null)
            {
                foreach (var line in lines)
                {
                    var pieces = line.Split(':');
                    var game = pieces[0];
                    var handfulls = pieces[1].Split(';');
                    foreach(var handfull in handfulls)
                    {
                        var cubes = handfull.Split(",");

                    }
                }

            }
            File.AppendAllText(filePath, $"------\n{sum}\n");
            return $"Part1:\n\tTotal: {sum}";
        }

        public static string Part2()
        {
            var filePath = @"F:\logs\2023010202.log";
            if(File.Exists(filePath))
                File.Delete(filePath);
            var lines = File.ReadLines("Assets/2023/Day02.txt").ToList();
            var sum = 0;
            if (lines != null)
            {
                foreach (var line in lines)
                {
                    var parsedLine = UsingHumanizer.ConvertWordsToNumbers(line);
                    var parsed = new string(parsedLine.Where(c => char.IsDigit(c)).ToArray());
                    var numericString = parsed.Substring(0, 1) + parsed.Substring(parsed.Length - 1);
                    File.AppendAllText(filePath, $"{numericString} {line} {parsedLine} {parsed}\n");
                    sum += int.Parse(numericString);
                }

            }
            File.AppendAllText(filePath, $"------\n{sum}\n");

            return $"Part2:\n\tTotal: {sum}";

        }
    }
}
