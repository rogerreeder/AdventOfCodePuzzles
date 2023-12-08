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
            var lines = File.ReadLines("Assets/2023/Day02.txt").ToList();
            var sum = 0;
            var cubeLimits = new Dictionary<string, int>
            {
                {"red", 12 },
                {"green", 13 },
                {"blue", 14 },
            };
            if (lines != null)
            {
                foreach (var line in lines)
                {
                    var pieces = line.Split(':');
                    var gameID = int.Parse(pieces[0].Split(' ')[1]);
                    var handfulls = pieces[1].Split(';');
                    var exceedsLimit = false;
                    foreach (var handfull in handfulls)
                    {
                        var cubes = handfull.Split(",");
                        foreach(var cube in cubes)
                        {
                            var cubeTotal = cube.Trim().Split(' ');
                            if(!exceedsLimit && int.Parse(cubeTotal[0]) > cubeLimits[cubeTotal[1]])
                                exceedsLimit=true;
                        }
                    }
                    if (exceedsLimit)
                        File.AppendAllText(filePath, $"{pieces[0]} exceeds\n");
                    else
                        sum += gameID;
                }

            }
            File.AppendAllText(filePath, $"------\n{sum}\n");
            return $"Part1:\n\tTotal: {sum}";
        }

        public static string Part2()
        {
            var filePath = @"F:\logs\20230202.log";
            if (File.Exists(filePath))
                File.Delete(filePath);
            var lines = File.ReadLines("Assets/2023/Day02.txt").ToList();
            var sum = 0L;
            if (lines != null)
            {
                foreach (var line in lines)
                {
                    var pieces = line.Split(':');
                    var gameID = int.Parse(pieces[0].Split(' ')[1]);
                    var handfulls = pieces[1].Split(';');
                    var exceedsLimit = false;
                    var cubeLimits = new Dictionary<string, int>
                    {
                        {"red", 0 },
                        {"green", 0 },
                        {"blue", 0 },
                    };
                    foreach (var handfull in handfulls)
                    {
                        var cubes = handfull.Split(",");
                        foreach (var cube in cubes)
                        {
                            var cubeTotals = cube.Trim().Split(' ');
                            var cubeTotal = int.Parse(cubeTotals[0]);
                            if (cubeTotal > cubeLimits[cubeTotals[1]])
                                cubeLimits[cubeTotals[1]] = cubeTotal;
                        }
                    }
                    File.AppendAllText(filePath, $"{(cubeLimits["red"] * cubeLimits["green"] * cubeLimits["blue"]).ToString().PadLeft(4)} red:{cubeLimits["red"]} green:{cubeLimits["green"]} blue:{cubeLimits["blue"]}\n");
                    sum += cubeLimits["red"] * cubeLimits["green"] * cubeLimits["blue"];
                }

            }
            File.AppendAllText(filePath, $"------\n{sum}\n");
            return $"Part2:\n\tTotal: {sum}";

        }
    }
}
