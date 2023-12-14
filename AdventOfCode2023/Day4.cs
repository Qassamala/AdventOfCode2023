using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2023
{
    internal class Day4
    {
        public Day4() {
            string filelocDay4 = "Day4Input.txt";
            Day4Part1And2(filelocDay4);

        }

        private static void Day4Part1And2(string filelocDay4)
        {
            double points = 0;
            string line;

            // For Part2
            Dictionary<int, int> instances = new Dictionary<int, int>();
            int cardNumber = 0;
            int copiesWonUntilIncluding = 0;
            instances.Add(1, 1);
            Dictionary<int, int> instancesWithMatchesFound = new Dictionary<int, int>();

            using (StreamReader read = new StreamReader(filelocDay4))
            {
                while ((line = read.ReadLine()) != null)
                {
                    cardNumber++;   //Part 2

                    double matchesFound = 0;
                    var card = line.Split('|');
                    var chosenNumbers = card[1].Split(' ')
                        .Select(x => x.Trim())
                        .Where(x => !string.IsNullOrWhiteSpace(x))
                        .ToArray();

                    var winningNumbers = card[0].Split(' ').Skip(2)
                        .Select(x => x.Trim())
                        .Where(x => !string.IsNullOrWhiteSpace(x))
                        .ToArray();

                    foreach (var number in winningNumbers)
                    {

                        foreach (var item in chosenNumbers)
                        {
                            if (item.Equals(number))
                            {
                                matchesFound++;
                                break;
                            }
                        }

                    }

                    // Part 2
                    // Create a map with how many copies are generated from each original
                    copiesWonUntilIncluding = cardNumber + (int)matchesFound;   // For part 1 & 2

                    if (!instancesWithMatchesFound.ContainsKey(cardNumber))
                    {
                        instancesWithMatchesFound.Add(cardNumber, copiesWonUntilIncluding);
                    }

                    // For part 1
                    if (matchesFound > 0)
                    {
                        matchesFound -= 1;

                        points += Math.Pow(2, matchesFound);
                    }

                }

            }

            Console.WriteLine("Day4Part1 Sum is: " + points);

            // Part 2
            int[] list = new int[instancesWithMatchesFound.Count + 1];

            // Principle is: store how many copies for each card in an array.
            // As you iterate, check how many copies plus 1 original and add that nr
            // to the succedding matching

            foreach (var item in instancesWithMatchesFound)
            {
                instancesWithMatchesFound.TryGetValue(item.Key, out var copiesuntil);

                int increment = 1 + list[item.Key]; // check hom many copies exist and the original
                for (int i = item.Key + 1; i <= copiesuntil; i++)
                {
                    list[i] = list[i] + increment;
                }
            }

            Console.WriteLine("Day4Part2 Sum is: " + (list.Sum() + (list.Length) - 1));
        }


    }
}
