using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using static AdventOfCode2023.Day5;

namespace AdventOfCode2023
{
    internal class Day5
    {
        internal class MapData
        {
            public MapData(string mapName, long destinationRangeStart, long sourceRangeStart, long rangeLength)
            {
                this.mapName = mapName;
                this.destinationRangeStart = destinationRangeStart;
                this.sourceRangeStart = sourceRangeStart;
                this.rangeLength = rangeLength;
            }

            public string mapName { get; set; }
            public long destinationRangeStart { get; set; }
            public long sourceRangeStart { get; set; }
            public long rangeLength { get; set; }
        }
        public Day5() {
            string filelocDay5 = "Day5Input.txt";
            Day5Part1(filelocDay5);

        }

        private void Day5Part1(string fileloc)
        {
            int lineCount = 0;
            List<string> seeds = new List<string>();

            long destinationRangeStart = 0;
            long sourceRangeStart = 0;
            long rangeLength = 0;

            // Alt approach
            string readText = File.ReadAllText(fileloc);
            var rows = readText.Split("\n");

            var firstRow = rows[0].Split(' ');
            foreach (var item in firstRow.Skip(1))
            {
                seeds.Add(item);
            }

            var inputData = readText.Split("\r\n").Skip(2);

            var maps = CreateArrayOfLists();

            int index = 0;

            string mapName = null;




            foreach (var line in inputData)
            {
                if (line.Contains(':'))
                {
                    mapName = line;
                    continue;
                }

                if (line.Equals(""))
                {
                    index++;
                    continue;
                }

                // should only be mapdata here

                var lineData = line.Split(" ");

                MapData mapData = new MapData(mapName, long.Parse(lineData[0]), long.Parse(lineData[1]), long.Parse(lineData[2]));

                maps[index].Add(mapData);

            }


            //must loop through each seed and check the mapdatas

            long lowest = long.MaxValue;

            //Part 1
            Part1And2(seeds, maps, lowest);

            // Part 2
            List<string> seedsPart2= new List<string>();

            /*
            for (int i = 0; i < seeds.Count; i=i+2)
            {
                Console.WriteLine((seeds[i+1]));
                
                for (long j = long.Parse(seeds[i]); j < (long.Parse(seeds[i]) + long.Parse(seeds[i+1])); j++)
                {
                    seedsPart2.Add(j.ToString());
                }
                
            }
            */
            /*
            lowest = long.MaxValue;
            for (int i = 0; i < seeds.Count; i = i + 2)
            {
                for (long j = long.Parse(seeds[i]); j < (long.Parse(seeds[i]) + long.Parse(seeds[i + 1])); j++)
                {
                    long calc = Part2(j, maps);
                    if (calc < lowest)
                    {
                        lowest = calc;
                    }
                }

            }
            */



            lowest = Part2Again(maps, seeds);
            Console.WriteLine("Part2: " + lowest);
        }

        private long Part2Again(List<MapData>[] maps, List<string> seeds)
        {
            long destination = 0;
            long target = 0;
            bool found = false;
            string mapName = null;
            

            maps.Reverse();
            while (!found)
            {
                target = destination;
                int run = 0;
                foreach (var map in maps.Reverse())
                {
                    
                    
                    foreach (var mapData in map)
                    {
                        mapName = mapData.mapName;
                        run++;
                        Console.WriteLine("Checking run : " + (run == map.Count));
                        if (run == map.Count)
                        {
                            Console.WriteLine("Breaking");
                            break;
                        }

                        Console.WriteLine("Run: " + run + " mapcount: " + map.Count);

                        Console.WriteLine("Target: " + target);
                        Console.WriteLine(target >= mapData.sourceRangeStart);
                        Console.WriteLine(target <= mapData.sourceRangeStart + mapData.rangeLength);
                        

                        if (target >= mapData.destinationRangeStart && target <= mapData.destinationRangeStart + mapData.rangeLength)
                        {
                            target = mapData.sourceRangeStart + (target - mapData.destinationRangeStart);
                            Console.WriteLine("Source Changed to: " + target);
                            break;
                        }

                    }

                    
                }

                Console.WriteLine("Mapname: " + mapName);
                if (seeds.Contains(target.ToString()) && mapName.Equals("seed-to-soil map:"))
                {
                    found = true;
                    return destination;
                }
                destination++;
            }

            return 0;




        }

        private static long Part1And2(List<string> seeds, List<MapData>[] maps, long lowest)
        {
            foreach (var item in seeds)
            {
                long source = long.Parse(item);

                foreach (var map in maps)
                {
                    foreach (var mapData in map)
                    {
                        /*
                        Console.WriteLine("Source: " + source);
                        Console.WriteLine(source >= mapData.sourceRangeStart);
                        Console.WriteLine(source <= mapData.sourceRangeStart + mapData.rangeLength);
                        */

                        if (source >= mapData.sourceRangeStart && source <= mapData.sourceRangeStart + mapData.rangeLength)
                        {
                            source = mapData.destinationRangeStart + (source - mapData.sourceRangeStart);
                            //Console.WriteLine("Source Changed to: " + source);
                            break;
                        }

                    }
                }

                if (source < lowest)
                {
                    lowest = source;
                }

            }

            Console.WriteLine("Lowest: " + lowest);
            return lowest;
        }

        private static long Part2(long source, List<MapData>[] maps)
        {

            foreach (var map in maps)
            {
                foreach (var mapData in map)
                {
                    /*
                    Console.WriteLine("Source: " + source);
                    Console.WriteLine(source >= mapData.sourceRangeStart);
                    Console.WriteLine(source <= mapData.sourceRangeStart + mapData.rangeLength);
                    */

                    if (source >= mapData.sourceRangeStart && source <= mapData.sourceRangeStart + mapData.rangeLength)
                    {
                        source = mapData.destinationRangeStart + (source - mapData.sourceRangeStart);
                        //Console.WriteLine("Source Changed to: " + source);
                        break;
                    }

                }
            }

            return source;
        }

        private static List<MapData>[] CreateArrayOfLists()
        {
            List<MapData>[] maps = new List<MapData>[7];

            //Maps
            List<MapData> seedToSoilMap = new List<MapData>();
            List<MapData> soilToFertilizerMap = new List<MapData>();
            List<MapData> fertilizerToWaterMap = new List<MapData>();
            List<MapData> waterToLightMap = new List<MapData>();
            List<MapData> lightToTemperatureMap = new List<MapData>();
            List<MapData> temperatureToHumidity = new List<MapData>();
            List<MapData> humidityToLocation = new List<MapData>();

            maps[0] = seedToSoilMap;
            maps[1] = soilToFertilizerMap;
            maps[2] = fertilizerToWaterMap;
            maps[3] = waterToLightMap;
            maps[4] = lightToTemperatureMap;
            maps[5] = temperatureToHumidity;
            maps[6] = humidityToLocation;

            return maps;
        }
    }
}
