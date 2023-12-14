using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AdventOfCode2023
{
    internal class Day1
    {
        public Day1() {
            string fileloc = "Day1Input.txt";
            Day1Part1(fileloc);
            Day1Part2(fileloc);

        }
        
        private static void Day1Part1(string fileloc)
        {
            int sum = 0, calibration = 0;
            string combined = null;

            using (StreamReader read = new StreamReader(fileloc))
            {
                string line;
                while ((line = read.ReadLine()) != null)
                {
                    var numbers = Regex.Matches(line, @"\d");
                    combined = numbers.First().Value + numbers.Last().Value;
                    calibration = ushort.Parse(combined);
                    sum += calibration;
                }
            }

            Console.WriteLine("Day 1 Part1 sum is: " + sum);
        }

        private static void Day1Part2(string fileloc)
        {
            string regex = @"\d|one|two|three|four|five|six|seven|eight|nine";
            int sum = 0, calibration = 0;
            //int counter = 1;

            using (StreamReader read = new StreamReader(fileloc))
            {
                string line;
                while ((line = read.ReadLine()) != null)
                {
                    var first = Regex.Match(line, regex);
                    var last = Regex.Match(line, regex, RegexOptions.RightToLeft);

                    calibration = ushort.Parse(extractInt(first.Value) + extractInt(last.Value));
                    sum += calibration;
                }
            }

            Console.WriteLine("Day 1 Part2 sum is: " + sum);
        }

        private static string extractInt(string number)
        {
            switch (number)
            {
                case "one": return "1";
                case "two": return "2";
                case "three": return "3";
                case "four": return "4";
                case "five": return "5";
                case "six": return "6";
                case "seven": return "7";
                case "eight": return "8";
                case "nine": return "9";
                default:
                    return number;
            }

        }
    }
}
