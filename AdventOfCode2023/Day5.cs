using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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
                if (line.Contains(':')){
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

                /*
                 * if source(seed) >= sourcerangestart and <= source + rangelength
                 *  then source = destination + ( source - sourcerangstart)
                 *  
                 *  
                 * 
                 */
                /*
                    if (source >= mapData.sourceRangeStart && source <= sourceRangeStart + rangeLength)
                    {
                        source = mapData.destinationRangeStart + (source - sourceRangeStart);
                    }
                    */

            }
            long source = long.Parse(seeds[index]);   //First seed
            index = 0;

            //must loop through each seed and check the mapdatas

            foreach (var item in seeds)
            {
                foreach (var mapData in maps[index])
                {
                    if (source >= mapData.sourceRangeStart && source <= sourceRangeStart + rangeLength)
                    {
                        source = mapData.destinationRangeStart + (source - sourceRangeStart);
                        index++;
                    }
                    
                }
               
                
            }

            Console.WriteLine(seeds[1]);
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
