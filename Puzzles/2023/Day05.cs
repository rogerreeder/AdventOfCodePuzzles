using AdventOfCodePuzzles.Helpers;
using System.Data;
using System.Data.Common;
using System.Diagnostics;

namespace AdventOfCodePuzzles.Year2023
{
    struct almanacSections
    {
        public const string NoSection = "no-section";
        public const string SeedLine = "seed-line";
        public const string SeedToSoilMap = "seed-to-soil map:";
        public const string SoilToFertilizerMap = "soil-to-fertilizer map:";
        public const string FertilizerToWaterMap = "fertilizer-to-water map:";
        public const string WaterToLightMap = "water-to-light map:";
        public const string LightToTempMap = "light-to-temperature map:";
        public const string TempToHumidMap = "temperature-to-humidity map:";
        public const string HumidToLocationMap = "humidity-to-location map:";
    }
    struct almanacMapNames
    {
        public const string SeedToSoil = "SD2SL";
        public const string SoilToFertilizer = "SL2FR";
        public const string FertilizerToWater = "FR2WT";
        public const string WaterToLight = "WT2LT";
        public const string LightToTemp = "LT2TP";
        public const string TempToHumid = "TP2HD";
        public const string HumidToLoc = "HD2LC";
    }
    class SeedRange
    {
        public long Start { get; set; }
        public long End { get; set; }
    }
    class AlmanacMap
    {
        public string Name { get; set; } = string.Empty;
        public long Source { get; set; }
        public long Destination { get; set; }
        public long Range { get; set; }
    }
    class AlmanacSeedInfo
    {
        public long Soil { get; set; } = 0;
        public long Fertilizer { get; set; } = 0;
        public long Water { get; set; } = 0;
        public long Temperature { get; set; } = 0;
        public long Humidity { get; set; } = 0;
        public long Location { get; set; } = 0;
    }
    static public class Day05
    {
        static long[] cardTotals = new long[0];
        static long sectionRow = 0;
        static List<long> seedsToBePlanted = new List<long>();//start and range
        static List<SeedRange> seedsToBePlantedPart2 = new List<SeedRange>();//start and range
        static List<AlmanacMap> mappings = new List<AlmanacMap>();
        static Dictionary<long, AlmanacSeedInfo> seeds = new Dictionary<long, AlmanacSeedInfo>();
        public static string Part1()
        {
            var filePath = @"F:\logs\20230501.log";
            var sw = Stopwatch.StartNew();
            if (File.Exists(filePath))
                File.Delete(filePath);
            File.AppendAllText(filePath, $"[{sw.Elapsed}] START\n");
            var lines = File.ReadLines("Assets/2023/Day05.txt").ToList();
            var lineNumber = 0;
            try {
                var currentSection = almanacSections.SeedLine;
                foreach (var line in lines)
                {
                    if (line.Length > 0)
                    {
                        var newSection = false;
                        if (currentSection == almanacSections.NoSection)
                        {
                            switch (line)
                            {
                                case almanacSections.SeedToSoilMap:
                                    currentSection = almanacSections.SeedToSoilMap;
                                    newSection = true;
                                    break;
                                case almanacSections.SoilToFertilizerMap:
                                    currentSection = almanacSections.SoilToFertilizerMap;
                                    newSection = true;
                                    break;
                                case almanacSections.FertilizerToWaterMap:
                                    currentSection = almanacSections.FertilizerToWaterMap;
                                    newSection = true;
                                    break;
                                case almanacSections.WaterToLightMap:
                                    currentSection = almanacSections.WaterToLightMap;
                                    newSection = true;
                                    break;
                                case almanacSections.LightToTempMap:
                                    currentSection = almanacSections.LightToTempMap;
                                    newSection = true;
                                    break;
                                case almanacSections.TempToHumidMap:
                                    currentSection = almanacSections.TempToHumidMap;
                                    newSection = true;
                                    break;
                                case almanacSections.HumidToLocationMap:
                                    currentSection = almanacSections.HumidToLocationMap;
                                    newSection = true;
                                    break;
                            }
                        }
                        if (!newSection)
                        {
                            var mappingName = string.Empty;
                            switch (currentSection)
                            {
                                case almanacSections.SeedLine:
                                    MapProcessSeedLine(line);
                                    break;
                                case almanacSections.SeedToSoilMap:
                                    mappingName = almanacMapNames.SeedToSoil;
                                    break;
                                case almanacSections.SoilToFertilizerMap:
                                    mappingName = almanacMapNames.SoilToFertilizer;
                                    break;
                                case almanacSections.FertilizerToWaterMap:
                                    mappingName = almanacMapNames.FertilizerToWater;
                                    break;
                                case almanacSections.WaterToLightMap:
                                    mappingName = almanacMapNames.WaterToLight;
                                    break;
                                case almanacSections.LightToTempMap:
                                    mappingName = almanacMapNames.LightToTemp;
                                    break;
                                case almanacSections.TempToHumidMap:
                                    mappingName = almanacMapNames.TempToHumid;
                                    break;
                                case almanacSections.HumidToLocationMap:
                                    mappingName = almanacMapNames.HumidToLoc;
                                    break;
                            }
                            if (!string.IsNullOrWhiteSpace(mappingName))
                                AddAlmanacMap(mappingName, line);
                        } else
                        {
                            sectionRow = 0L;
                        }
                    } else
                    {
                        currentSection = almanacSections.NoSection;
                    }

                }

                var lowestLocation = ProcessSeedInMapping();
                File.AppendAllText(filePath, $"------\n{lowestLocation}\n");
                File.AppendAllText(filePath, $"[{sw.Elapsed}] DONE\n");
                return $"Part1:\n\tTotal: {lowestLocation}";
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
            var filePath = @"F:\logs\20230502.log";
            var sw = Stopwatch.StartNew();
            if (File.Exists(filePath))
                File.Delete(filePath);
            File.AppendAllText(filePath, $"[{sw.Elapsed}] START\n");
            var cards = File.ReadLines("Assets/2023/Day05.txt").ToArray();
            cardTotals = new long[cards.Length];
            var lineNumber = 0;
            try
            {
                var currentSection = almanacSections.SeedLine;
                foreach (var line in cards)
                {
                    if (line.Length > 0)
                    {
                        var newSection = false;
                        if (currentSection == almanacSections.NoSection)
                        {
                            switch (line)
                            {
                                case almanacSections.SeedToSoilMap:
                                    currentSection = almanacSections.SeedToSoilMap;
                                    newSection = true;
                                    break;
                                case almanacSections.SoilToFertilizerMap:
                                    currentSection = almanacSections.SoilToFertilizerMap;
                                    newSection = true;
                                    break;
                                case almanacSections.FertilizerToWaterMap:
                                    currentSection = almanacSections.FertilizerToWaterMap;
                                    newSection = true;
                                    break;
                                case almanacSections.WaterToLightMap:
                                    currentSection = almanacSections.WaterToLightMap;
                                    newSection = true;
                                    break;
                                case almanacSections.LightToTempMap:
                                    currentSection = almanacSections.LightToTempMap;
                                    newSection = true;
                                    break;
                                case almanacSections.TempToHumidMap:
                                    currentSection = almanacSections.TempToHumidMap;
                                    newSection = true;
                                    break;
                                case almanacSections.HumidToLocationMap:
                                    currentSection = almanacSections.HumidToLocationMap;
                                    newSection = true;
                                    break;
                            }
                        }
                        if (!newSection)
                        {
                            var mappingName = string.Empty;
                            switch (currentSection)
                            {
                                case almanacSections.SeedLine:
                                    MapProcessSeedLine2(line);
                                    break;
                                case almanacSections.SeedToSoilMap:
                                    mappingName = almanacMapNames.SeedToSoil;
                                    break;
                                case almanacSections.SoilToFertilizerMap:
                                    mappingName = almanacMapNames.SoilToFertilizer;
                                    break;
                                case almanacSections.FertilizerToWaterMap:
                                    mappingName = almanacMapNames.FertilizerToWater;
                                    break;
                                case almanacSections.WaterToLightMap:
                                    mappingName = almanacMapNames.WaterToLight;
                                    break;
                                case almanacSections.LightToTempMap:
                                    mappingName = almanacMapNames.LightToTemp;
                                    break;
                                case almanacSections.TempToHumidMap:
                                    mappingName = almanacMapNames.TempToHumid;
                                    break;
                                case almanacSections.HumidToLocationMap:
                                    mappingName = almanacMapNames.HumidToLoc;
                                    break;
                            }
                            if (!string.IsNullOrWhiteSpace(mappingName))
                                AddAlmanacMap(mappingName, line);
                        }
                        else
                        {
                            sectionRow = 0L;
                        }
                    }
                    else
                    {
                        currentSection = almanacSections.NoSection;
                    }

                }
                var lowestLocation = ProcessSeedInMappingUseParallel();
                File.AppendAllText(filePath, $"------\n{lowestLocation}\n");
                File.AppendAllText(filePath, $"[{sw.Elapsed}] DONE\n");
                return $"Part2:\n\tTotal: {lowestLocation}";
            }
            catch (Exception ex)
            {
                File.AppendAllText(filePath, $"\n[{lineNumber,4}] {ex}\n");
                throw;
            }
        }
        /// <summary>
        /// returns lowest location
        /// </summary>
        /// <returns></returns>
        private static long ProcessSeedInMapping()
        {
            var lowestLlocation = 0L;
            foreach (var seed in seedsToBePlanted)
            {
                var soil = GetDestination(almanacMapNames.SeedToSoil, seed);
                var fertilizer = GetDestination(almanacMapNames.SoilToFertilizer, soil);
                var water = GetDestination(almanacMapNames.FertilizerToWater, fertilizer);
                var light = GetDestination(almanacMapNames.WaterToLight, water);
                var temp = GetDestination(almanacMapNames.LightToTemp, light);
                var humidity = GetDestination(almanacMapNames.TempToHumid, temp);
                var location = GetDestination(almanacMapNames.HumidToLoc, humidity);
                if (lowestLlocation == 0 || location < lowestLlocation)
                    lowestLlocation = location;
            }
            return lowestLlocation;
        }
        private static long ProcessSeedInMapping2()
        {
            var possibleLocations = new List<(long, long)>();
            int nested = 2;
            long startValue = 0L;
            long endValue = 3000000000L;
            var lowestLocation = 0L;
            var seed = 0L;
            for (long location = startValue; location <= endValue; location++)
            {
                if (location % 1000 == 0)
                    Debug.WriteLine($"location: {location}");
                seed = GetSeedFromLocation(location);
                if (seedsToBePlantedPart2.Where(s => s.Start < seed && s.End > seed).FirstOrDefault() != null)
                {
                    possibleLocations.Add((location, seed));
                    lowestLocation = location;
                    break;
                }
            }
            /*
            Parallel.For(
                startValue,
                endValue,
                new ParallelOptions { MaxDegreeOfParallelism = Environment.ProcessorCount },
                (i, loopState) =>
                {
                    long location = i;
                    long seed = GetSeedFromLocation(location);
                    if(seedsToBePlantedPart2.Where(s => s.Start < seed && s.End > seed).FirstOrDefault() != null)
                    {
                        possibleLocations.Add((location, seed));
                        lowestLocation = location;
                        loopState.Stop();
                    }
                    
                });
            */
            return lowestLocation;
        }
        private static long ProcessSeedInMappingUseParallel()
        {
            var possibleLocations = new List<(long, long)>();
            int nested = 2;
            var lowestLocation = 0L;
            foreach (var seedRange in seedsToBePlantedPart2)
            {
                var start = seedRange.Start;
                var end = seedRange.End;
                var span = end - start;
                if (span < 0)
                    Debug.Print($"we have a problem.");
                var interval =  1024 * 32;
                for(var i = 0; i <= interval; i++)
                {
                    var seed = start + ((span / interval) * i);
                    var possibleLoc = GetLocationFromSeed(seed);
                    if (lowestLocation == 0L || possibleLoc < lowestLocation)
                        lowestLocation = possibleLoc;
                }
            }
            var endValue = lowestLocation;
            long startValue = 1000000L;
            //var endValue = GetLocationFromSeed(seedsToBePlantedPart2.First().Start);//just get a legit location to use as an upper. Will never get that high.
            Parallel.For(
                startValue,
                endValue,
                new ParallelOptions { 
                      MaxDegreeOfParallelism = Environment.ProcessorCount },
                (i, loopState) =>
                {
                    var lowest = 0L;
                    if(!loopState.ShouldExitCurrentIteration)
                    {
                        long location = i;
                        long seed = GetSeedFromLocation(location);
                        var inRange = seedsToBePlantedPart2.Where(s => s.Start < seed && s.End > seed).FirstOrDefault();
                        if (inRange != null)
                        {
                            if(lowest == 0 || location < lowest)
                            {
                                lowestLocation = location;
                                Debug.WriteLine("{0}", lowestLocation.ToString());
                                loopState.Stop();

                            }
                        }
                    }
                });
            return lowestLocation;
        }

        private static long GetSeedFromLocation(long location)
        {
            var humidity = GetSource(almanacMapNames.HumidToLoc, location);
            var temp = GetSource(almanacMapNames.TempToHumid, humidity);
            var light = GetSource(almanacMapNames.LightToTemp, temp);
            var water = GetSource(almanacMapNames.WaterToLight, light);
            var fertilizer = GetSource(almanacMapNames.FertilizerToWater, water);
            var soil = GetSource(almanacMapNames.SoilToFertilizer, fertilizer);
            var seed = GetSource(almanacMapNames.SeedToSoil, soil);
            return seed;
        }

        private static long GetLocationFromSeed(long seed)
        {
            var soil = GetDestination(almanacMapNames.SeedToSoil, seed);
            var fertilizer = GetDestination(almanacMapNames.SoilToFertilizer, soil);
            var water = GetDestination(almanacMapNames.FertilizerToWater, fertilizer);
            var light = GetDestination(almanacMapNames.WaterToLight, water);
            var temp = GetDestination(almanacMapNames.LightToTemp, light);
            var humidity = GetDestination(almanacMapNames.TempToHumid, temp);
            var location = GetDestination(almanacMapNames.HumidToLoc, humidity);
            return location;
        }
        private static long GetDestination(string mappingName, long source)
        {
            var destination = source;
            var mapping = mappings
                .Where(a => a.Name == mappingName)
                .Where(b => destination >= b.Source && destination < b.Source + b.Range)
                .FirstOrDefault();
            if (mapping != null)
                destination = mapping.Destination + (destination - mapping.Source);
            return destination;
        }
        private static long GetSource(string mappingName, long destination)
        {
            var source = destination;
            var mapping = mappings
                .Where(a => a.Name == mappingName)
                .Where(b => source >= b.Destination && source < b.Destination + b.Range)
                .FirstOrDefault();
            if (mapping != null)
                source = mapping.Source + (source - mapping.Destination);
            return source;
        }

        private static void MapProcessSeedLine(string line)
        {
            foreach (var seed in line.Split(':')[1].Trim().Split(' '))
                seedsToBePlanted.Add(long.Parse(seed));
            return;
        }
        private static void MapProcessSeedLine2(string line)
        {
            var isStart = true;
            var start = 0L;
            foreach (var seed in line.Split(':')[1].Trim().Split(' '))
            {
                if (isStart)
                    start = long.Parse(seed);
                else
                    seedsToBePlantedPart2.Add(new SeedRange { Start = start, End = start + long.Parse(seed) - 1});
                isStart = !isStart;
            }
            return;
        }
        private static void AddAlmanacMap(string name, string line)
        {
            var fields = line.Split(' ');// 0 is destination, 1 is source, 2 is range
            var destination = long.Parse(fields[0]);//soil
            var source = long.Parse(fields[1]);//seed
            var range = long.Parse(fields[2]);
            mappings.Add(new AlmanacMap
            {
                Name = name,
                Source = source,
                Destination = destination,
                Range = range
            });

        }

    }
}
