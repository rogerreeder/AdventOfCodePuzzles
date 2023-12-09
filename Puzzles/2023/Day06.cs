using AdventOfCodePuzzles.Helpers;
using System.Diagnostics;

namespace AdventOfCodePuzzles.Year2023
{
    static public class Day06
    {
        public static string Part1()
        {
            var sum = 0L;
            var filePath = @"F:\logs\20230601.log";
            var sw = Stopwatch.StartNew();
            if (File.Exists(filePath))
                File.Delete(filePath);
            var lines = File.ReadLines("Assets/2023/Day06.txt").ToArray();
            var lineNumber = 0;
            try {
                File.AppendAllText(filePath, $"------\n{sum}\n");
                var waysToWinPerRace = new List<int>();
                if (lines.Length == 2)
                {
                    var times = lines[0].Split(':')[1].Trim().Replace("  "," ").Replace("  ", " ").Replace("  ", " ").Replace("  ", " ").Replace("  ", " ").Split(' ');
                    var distances = lines[1].Split(':')[1].Trim().Replace("  ", " ").Replace("  ", " ").Replace("  ", " ").Replace("  ", " ").Replace("  ", " ").Split(' ');
                    for(var race = 0; race < distances.Length; race++)
                    {
                        var raceTime = int.Parse(times[race]);
                        var raceDistance = int.Parse(distances[race]);
                        var wins = 0;
                        for(var buttonTime = 1; buttonTime < raceTime; buttonTime++)
                        {
                            var distanceTraveled = DistanceTraveled(buttonTime, raceTime);
                            if(distanceTraveled >  raceDistance)
                                wins++;
                        }
                        if(wins > 0)
                            waysToWinPerRace.Add(wins);
                    }
                }
                sum = waysToWinPerRace.Aggregate((a, n) => a * n);
                File.AppendAllText(filePath, $"------\n{sum}\n");
                File.AppendAllText(filePath, $"[{sw.Elapsed}] DONE\n");
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
            var lines = File.ReadLines("Assets/2023/Day06.demo.txt").ToArray();
            var lineNumber = 0;
            try
            {
                if (lines.Length == 2)
                {
                    var time = lines[0].Split(':')[1].Trim().Replace(" ", "");
                    var distance = lines[1].Split(':')[1].Replace(" ","").Trim();
                    var raceTime = double.Parse(time);
                    var raceDistance = double.Parse(distance);
                    double chargingTime = CalculateChargingTime(raceDistance, raceTime);

                    if (chargingTime >= 0)
                    {
                        double remainingTime = raceTime - chargingTime;
                        Console.WriteLine($"Charging time: {chargingTime} milliseconds");
                        Console.WriteLine($"Remaining time to travel: {remainingTime} milliseconds");
                    }
                    else
                    {
                        Console.WriteLine("It's not possible to cover the distance in the given time.");
                    }
                }
                File.AppendAllText(filePath, $"------\n{sum}\n");
                File.AppendAllText(filePath, $"[{sw.Elapsed}] DONE\n");
                return $"Part2:\n\tTotal: {sum}";
           }
            catch (Exception ex)
            {
                File.AppendAllText(filePath, $"\n[{lineNumber,4}] {ex}\n");
                throw;
            }
        }

        static double CalculateChargingTime(double distanceToTravel, double totalTimeToTravel)
        {
            // Assuming linear speed increase: 1 millimeter per millisecond per millisecond
            double initialSpeed = 0; // Initial speed in millimeters per millisecond


            // The formula for the distance covered during charging is: distance = initialSpeed * time + 0.5 * acceleration * time^2
            // We rearrange this to solve for time: time = (-initialSpeed + sqrt(initialSpeed^2 + 2 * acceleration * distance)) / acceleration
            double chargingTime = (-initialSpeed + Math.Sqrt(Math.Pow(initialSpeed, 2) + 2 * distanceToTravel));

            // If the calculated charging time is less than or equal to the total time, return it
            // Otherwise, return -1 to indicate that it's not possible to cover the distance in the given time
            return (chargingTime <= totalTimeToTravel) ? chargingTime : -1;
        }
        private static int DistanceTraveled(int timeButtonPressed, int raceTime) =>
            (timeButtonPressed < raceTime) ? timeButtonPressed * (raceTime - timeButtonPressed) : 0;

    }
}
