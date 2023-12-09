using AdventOfCodePuzzles.Helpers;
using System.Diagnostics;

namespace AdventOfCodePuzzles.Year2023
{
    static public class Day06
    {
        static int[,] buttonStats = {
              { 0, 0, 0}
            , { 0, 0, 0}
            , { 0, 0, 0}
            , { 0, 0, 0}
            , { 0, 0, 0}
            , { 0, 0, 0}
            , { 0, 0, 0}
            , { 0, 0, 0}
        };
        public static string Part1()
        {
            var sum = 0L;
            var filePath = @"F:\logs\20230601.log";
            var sw = Stopwatch.StartNew();
            if (File.Exists(filePath))
                File.Delete(filePath);
            var lines = File.ReadLines("Assets/2023/Day06.demo.txt").ToList();
            var lineNumber = 0;
            try {
                File.AppendAllText(filePath, $"------\n{sum}\n");
                if(lines.Count == 2)
                {
                    var times = lines[0].Split(':')[1].Trim().Replace("  "," ").Replace("  ", " ").Split(' ');
                    var distances = lines[1].Split(':')[1].Trim().Replace("  ", " ").Replace("  ", " ").Split(' ');
                    for(var race = 0; race < distances.Length; race++)
                    {
                        var raceTime = int.Parse(times[race]);
                        var raceDistance = int.Parse(distances[race]);
                        for(var buttonTime = 1; buttonTime < raceTime; buttonTime++)
                        {
                            var distance = buttonTime * 1
                        }
                    }
                }
                return $"Part1:\n\tTotal: {sum}";
            }
            catch (Exception ex)
            {
                File.AppendAllText(filePath, $"\n[{lineNumber,4}] {ex}\n");
                throw;
            }
        }

        public static string Part2()
        {

            var sum = 0L;
            var filePath = @"F:\logs\20230602.log";
            var sw = Stopwatch.StartNew();
            if (File.Exists(filePath))
                File.Delete(filePath);
            var cards = File.ReadLines("Assets/2023/Day06.demo.txt").ToArray();
            cardTotals = new long[cards.Length];
            var lineNumber = 0;
            try
            {
                File.AppendAllText(filePath, $"-------\n{sum,7}\n");
                File.AppendAllText(filePath, $"[{sw.Elapsed}] DONE\n");
                return $"Part2:\n\tTotal: {sum}";
           }
            catch (Exception ex)
            {
                File.AppendAllText(filePath, $"\n[{lineNumber,4}] {ex}\n");
                throw;
            }
        }
    }
}
