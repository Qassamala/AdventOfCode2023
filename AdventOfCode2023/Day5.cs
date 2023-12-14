using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AdventOfCode2023
{
    internal class Day5
    {
        public Day5() {
            string filelocDay5 = "Day5Input.txt";
            Day5Part1(filelocDay5);

        }

        private void Day5Part1(string fileloc)
        {
            int lineCount = 0;
            List<string> seeds = new List<string>();

            // Alt approach
            string readText = File.ReadAllText(fileloc);
            var rows = readText.Split("\n");

            var firstRow = rows[0].Split(' ');
            foreach (var item in firstRow.Skip(1))
            {
                seeds.Add(item);
            }

            var mapData = readText.Split("\r\n").Skip(2);




            var maps = CreateArrayOfMaps();

            using (StreamReader read = new StreamReader(fileloc))
            {
                string line;

                string destinationRangeStart = null;
                string sourceRangeStart = null;
                string rangeLength = null;

                while ((line = read.ReadLine()) != null)
                {
                    if (lineCount++ < 1)
                    {
                        var subsets = line.Split(' ');
                        foreach (var item in subsets.Skip(1))
                        {
                            seeds.Add(item);
                        }
                    }
                }
            }

            Console.WriteLine(seeds[1]);
        }

        private static Dictionary<string, string>[] CreateArrayOfMaps()
        {
            Dictionary<string, string>[] maps = new Dictionary<string, string>[7];

            //Maps
            Dictionary<string, string> seedToSoilMap = new Dictionary<string, string>();
            Dictionary<string, string> soilToFertilizerMap = new Dictionary<string, string>();
            Dictionary<string, string> fertilizerToWaterMap = new Dictionary<string, string>();
            Dictionary<string, string> waterToLightMap = new Dictionary<string, string>();
            Dictionary<string, string> lightToTemperatureMap = new Dictionary<string, string>();
            Dictionary<string, string> temperatureToHumidity = new Dictionary<string, string>();
            Dictionary<string, string> humidityToLocation = new Dictionary<string, string>();

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
