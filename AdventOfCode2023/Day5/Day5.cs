using System.Text.RegularExpressions;

namespace AdventOfCode2023.Day5;

[TestClass]
public class Day5 : IDay
{
    [TestMethod]
    public void Part1()
    {
        List<string> input = PuzzleInputReader.ReadInput(5).ToList();
        var data = new Dag5_Data(input);

        long minLocation = data.Seeds.Select(seed => data.ConvertSeedToLocation(seed)).Min();

        Console.WriteLine($"location: {minLocation}");
    }

    [TestMethod]
    public void Part2()
    {
        List<string> input = PuzzleInputReader.ReadInput(5).ToList();
        var data = new Dag5_Data(input);
        var allMin = long.MaxValue;
        var tasks = new List<Task<long>>();
        for (var i = 0; i < data.Seeds.Count - 1; i += 2)
        {
            int index = i;
            tasks.Add(Task.Run(() =>
            {
                long start = data.Seeds[index];
                long length = data.Seeds[index + 1];
                long minLocation = long.MaxValue;


                for (long j = start; j < start + length; j++)
                {
                    long location = data.ConvertSeedToLocation(j);
                    minLocation = Math.Min(minLocation, location);
                }

                return minLocation;
                Console.WriteLine($"{index} is done");
            }));
        }

        foreach (Task<long> task in tasks)
        {
            long result = task.Result;
            Console.WriteLine($"Evaluating result of task");
            allMin = Math.Min(result, allMin);
        }


        Console.WriteLine($"location: {allMin}");
    }

    [TestMethod]
    public void Dag5_Data_ConvertToMap()
    {
        var data = new Dag5_Data(new List<string>());
        data.SeedToSoilMap = new List<Dag5_Range>()
        {
            new Dag5_Range() { DestinationStart = 50, SourceStart = 98, RangeLength = 2 },
            new Dag5_Range() { DestinationStart = 52, SourceStart = 50, RangeLength = 48 },
        };

        Assert.AreNotEqual(49, data.ConvertToMap(97, data.SeedToSoilMap));
        Assert.AreEqual(50, data.ConvertToMap(98, data.SeedToSoilMap));
        Assert.AreEqual(51, data.ConvertToMap(99, data.SeedToSoilMap));
        Assert.AreNotEqual(52, data.ConvertToMap(100, data.SeedToSoilMap));
    }

    class Dag5_Data
    {
        public List<long> Seeds { get; set; }
        public List<Dag5_Range> SeedToSoilMap { get; set; } = new List<Dag5_Range>();
        public List<Dag5_Range> SoilToFertilizerMap { get; set; } = new List<Dag5_Range>();
        public List<Dag5_Range> FertilizerToWaterMap { get; set; } = new List<Dag5_Range>();
        public List<Dag5_Range> WaterToLightMap { get; set; } = new List<Dag5_Range>();
        public List<Dag5_Range> LightToTemperatureMap { get; set; } = new List<Dag5_Range>();
        public List<Dag5_Range> TemperatureToHumidityMap { get; set; } = new List<Dag5_Range>();
        public List<Dag5_Range> HumidityToLocationMap { get; set; } = new List<Dag5_Range>();

        public Dag5_Data(List<string> input)
        {
            List<Dag5_Range>? currentMap = null;
            var numberLine = new Regex("^\\d+ \\d+ \\d+$");
            var seedLine = new Regex("^seeds: .*$");
            foreach (string line in input)
            {
                if (numberLine.IsMatch(line))
                {
                    if (currentMap == null) throw new NullReferenceException("current map is null!");
                    IEnumerable<long> values = line.Split(' ').Select(long.Parse).ToList();
                    var range = new Dag5_Range()
                    {
                        DestinationStart = values.First(),
                        SourceStart = values.Skip(1).First(),
                        RangeLength = values.Last()
                    };
                    currentMap.Add(range);
                    continue;
                }


                switch (line)
                {
                    case "seed-to-soil map:":
                        currentMap = SeedToSoilMap;
                        continue;
                    case "soil-to-fertilizer map:":
                        currentMap = SoilToFertilizerMap;
                        continue;

                    case "fertilizer-to-water map:":
                        currentMap = FertilizerToWaterMap;
                        continue;
                    case "water-to-light map:":
                        currentMap = WaterToLightMap;
                        continue;
                    case "light-to-temperature map:":
                        currentMap = LightToTemperatureMap;
                        continue;
                    case "temperature-to-humidity map:":
                        currentMap = TemperatureToHumidityMap;
                        continue;
                    case "humidity-to-location map:":
                        currentMap = HumidityToLocationMap;
                        continue;
                    case "":
                        continue;
                }

                if (seedLine.IsMatch(line))
                {
                    Seeds = line[(line.IndexOf(':') + 2)..].Split(' ').Select(long.Parse).ToList();
                    continue;
                }
            }
        }

        public long ConvertToMap(long value, List<Dag5_Range> map)
        {
            foreach (Dag5_Range dag5Range in map)
            {
                if (dag5Range.SourceStart > value ||
                    dag5Range.SourceStart + dag5Range.RangeLength <= value) continue;
                long offset = value - dag5Range.SourceStart;
                return dag5Range.DestinationStart + offset;
            }

            return value;
        }

        public long ConvertSeedToLocation(long seed)
        {
            long soil = ConvertToMap(seed, SeedToSoilMap);
            long fertilizer = ConvertToMap(soil, SoilToFertilizerMap);
            long water = ConvertToMap(fertilizer, FertilizerToWaterMap);
            long light = ConvertToMap(water, WaterToLightMap);
            long temperature = ConvertToMap(light, LightToTemperatureMap);
            long humidity = ConvertToMap(temperature, TemperatureToHumidityMap);
            return ConvertToMap(humidity, HumidityToLocationMap);
        }

        public IEnumerable<long> ConvertSeedsToLocation(IEnumerable<long> seeds)
        {
            throw new NotImplementedException();
        }
        
    }

    record Dag5_Range
    {
        public long DestinationStart { get; set; }
        public long SourceStart { get; set; }
        public long RangeLength { get; set; }
    }
}