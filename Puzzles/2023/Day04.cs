using AdventOfCodePuzzles.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace AdventOfCodePuzzles.Year2023
{
    static public class Day04
    {
        static long[]? cardTotals;

        public static string Part1()
        {
            var sum = 0L;
            var filePath = @"F:\logs\20230401.log";
            if (File.Exists(filePath))
                File.Delete(filePath);
            var lines = File.ReadLines("Assets/2023/Day04.txt").ToList();
            var lineNumber = 0;
            try {
                foreach (var line in lines)
                {
                    var cardPieces = line.Split(':');
                    var cardNumber = int.Parse(cardPieces[0].Split(' ')[1].Trim());
                    var playerAndWinners = cardPieces[1].Split('|');
                    var winningNumber = playerAndWinners[0].Trim().Split(" ");
                    var playerNumbers = playerAndWinners[1].Trim().Split(" ");
                    var matches = 0;
                    foreach (var playerNumber in playerNumbers)
                    {
                        if (winningNumber.Contains(playerNumber))
                            matches++;
                    }
                    var value = 0L;
                    if (matches > 0)
                        value = (long)Math.Pow(2, matches - 1);
                    if (value > 0)
                        sum += value;
                    File.AppendAllText(filePath, $"Card {cardNumber}: {matches} matches {value}\n");

                    lineNumber++;
                }
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
            var filePath = @"F:\logs\20230402.log";
            if (File.Exists(filePath))
                File.Delete(filePath);
            var cards = File.ReadLines("Assets/2023/Day04.txt").ToArray();
            cardTotals = new long[cards.Length];
            var lineNumber = 0;
            try
            {
                var sw = Stopwatch.StartNew();
                var processes = new List<Task<int>>();
                File.AppendAllText(filePath, $"[{sw.Elapsed}] START\n");
                for (var cardIndex = cardTotals.Length - 1; cardIndex >= 0; cardIndex--)
                {
                    sum += ProcessCard(cards, cardIndex);
                    File.AppendAllText(filePath, $"{cardTotals[cardIndex],7} Card {cardIndex + 1,3}\n");
                }
                File.AppendAllText(filePath, $"-------\n{sum,7}\n");
                File.AppendAllText(filePath, $"[{sw.Elapsed}] DONE\n");
                return $"Part1:\n\tTotal: {sum}";
           }
            catch (Exception ex)
            {
                File.AppendAllText(filePath, $"\n[{lineNumber,4}] {ex}\n");
                throw;
            }
        }

        private static long ProcessCard(string[] cards, int cardIndex)
        {
            var result = 1L;
            var key = $"Card {cardIndex + 1}";
            var cardPieces = cards[cardIndex].Split(':');
            var cardNumber = int.Parse(cardPieces[0].Split(' ')[1].Trim());
            var playerAndWinners = cardPieces[1].Split('|');
            var winningNumber = playerAndWinners[0].Trim().Split(" ");
            var playerNumbers = playerAndWinners[1].Trim().Split(" ");
            var cardPosition = cardIndex + 1;
            foreach (var playerNumber in playerNumbers)
                if (winningNumber.Contains(playerNumber))
                    if(cardPosition < cards.Length) {
                        result += cardTotals[cardPosition];
                        cardPosition++;
                    }
            cardTotals[cardIndex] = result;
            return result;
        }
    }
}
