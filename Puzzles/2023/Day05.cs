using AdventOfCodePuzzles.Helpers;
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
        static List<long> seedsToBePlanted = new List<long>();
        static List<AlmanacMap> mappings = new List<AlmanacMap>();
        static Dictionary<long, AlmanacSeedInfo> seeds = new Dictionary<long, AlmanacSeedInfo>();
        public static string Part1()
        {
            var sum = 0L;
            var filePath = @"F:\logs\20230501.log";
            var sw = Stopwatch.StartNew();
            if (File.Exists(filePath))
                File.Delete(filePath);
            var lines = File.ReadLines("Assets/2023/Day05.txt").ToList();
            var lineNumber = 0;
            try {
                var currentSection = almanacSections.SeedLine;
                foreach( var line in lines )
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
                        if(!newSection)
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
                            if(!string.IsNullOrWhiteSpace(mappingName))
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
                ProcessSeedInMapping();
                var lowestLocation = 0L;
                foreach(var seed in seeds)
                    if(lowestLocation == 0 || lowestLocation > seed.Value.Location)
                        lowestLocation = seed.Value.Location;
                File.AppendAllText(filePath, $"------\n{lowestLocation}\n");
                return $"Part1:\n\tTotal: {lowestLocation}";
            }
            catch (Exception ex)
            {
                File.AppendAllText(filePath, $"\n[{lineNumber,4}] {ex}\n");
                throw;
            }
        }
        public static void ProcessSeedInMapping()
        {
            foreach(var seed in seedsToBePlanted)
            {
                var soil = GetDestination(almanacMapNames.SeedToSoil, seed);
                var fertilizer = GetDestination(almanacMapNames.SoilToFertilizer, soil);
                var water = GetDestination(almanacMapNames.FertilizerToWater, fertilizer);
                var light = GetDestination(almanacMapNames.WaterToLight, water);
                var temp = GetDestination(almanacMapNames.LightToTemp, light);
                var humidity = GetDestination(almanacMapNames.TempToHumid, temp);
                var location = GetDestination(almanacMapNames.HumidToLoc, humidity);

                var seedInfo = new AlmanacSeedInfo
                {
                    Soil = soil
                    , Fertilizer = fertilizer
                    , Humidity = humidity
                    , Location = location
                    , Temperature = temp
                    , Water = water
                };
                seeds.Add(seed, seedInfo);
            }
        }
        public static long GetDestination(string mappingName, long source)
        {
            var destination = source;
            var mapping = mappings
            .Where(a => a.Name == mappingName)
                .Where(source => destination >= source.Source && destination < source.Source + source.Range)
                .FirstOrDefault();
            if(mapping != null)
                destination = mapping.Destination + (destination - mapping.Source);
            return destination;
        }
        public static void MapProcessSeedLine(string line)
        {
            foreach (var seed in line.Split(':')[1].Trim().Split(' '))
                seedsToBePlanted.Add(long.Parse(seed));
            return;
        }
        public static void AddAlmanacMap(string name, string line)
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
        public static string Part2()
        {

            var sum = 0L;
            var filePath = @"F:\logs\20230502.log";
            var sw = Stopwatch.StartNew();
            if (File.Exists(filePath))
                File.Delete(filePath);
            var cards = File.ReadLines("Assets/2023/Day05.demo.txt").ToArray();
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
