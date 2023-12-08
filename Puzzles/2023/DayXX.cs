using AdventOfCodePuzzles.Helpers;
using System.Diagnostics;

namespace AdventOfCodePuzzles.Year2023
{
    static public class DayXX
    {
        static long[]? cardTotals;

        public static string Part1()
        {
            var sum = 0L;
            var filePath = @"F:\logs\2023xx01.log";
            var sw = Stopwatch.StartNew();
            if (File.Exists(filePath))
                File.Delete(filePath);
            var lines = File.ReadLines("Assets/2023/Dayxx.demo.txt").ToList();
            var lineNumber = 0;
            try {
                File.AppendAllText(filePath, $"------\n{sum}\n");
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
            var filePath = @"F:\logs\2023xx02.log";
            var sw = Stopwatch.StartNew();
            if (File.Exists(filePath))
                File.Delete(filePath);
            var cards = File.ReadLines("Assets/2023/Dayxx.demo.txt").ToArray();
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
