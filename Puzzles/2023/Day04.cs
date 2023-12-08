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
                    var cardNumber = int.Parse(cardPieces[0].Replace("  ", " ").Replace("  "," ").Split(' ')[1].Trim());
                    var playerAndWinners = cardPieces[1].Split('|');
                    var winningNumber = playerAndWinners[0].Trim().Replace("  ", " ").Split(" ");
                    var playerNumbers = playerAndWinners[1].Trim().Replace("  ", " ").Split(" ");
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
            var lineNumber = 0;
            try
            {
                for(var cardIndex = 0; cardIndex < cards.Length; cardIndex++)
                    sum += ProcessCard(cards, cardIndex, 0, filePath);
                File.AppendAllText(filePath, $"------\n{sum}\n");
                return $"Part1:\n\tTotal: {sum}";
            }
            catch (Exception ex)
            {
                File.AppendAllText(filePath, $"\n[{lineNumber,4}] {ex}\n");
                throw;
            }
        }

        private static int ProcessCard(string[] cards, int cardIndex, int nesting, string filePath)
        {
            var cardPieces = cards[cardIndex].Split(':');
            var cardNumber = int.Parse(cardPieces[0].Replace("  ", " ").Replace("  ", " ").Split(' ')[1].Trim());
            var playerAndWinners = cardPieces[1].Split('|');
            var winningNumber = playerAndWinners[0].Trim().Replace("  ", " ").Split(" ");
            var playerNumbers = playerAndWinners[1].Trim().Replace("  ", " ").Split(" ");
            var matches = 0;
            foreach (var playerNumber in playerNumbers)
                if (winningNumber.Contains(playerNumber))
                    matches++;
            int result = 1;
            File.AppendAllText(filePath, $"{string.Empty.PadLeft(nesting * 4)}Card {cardIndex + 1}: matches:{matches}\n");
            for (var i = 1; i <= matches; i++)
                if(cardIndex + i < cards.Length - 1)
                    result += ProcessCard(cards, cardIndex + i, nesting + 1, filePath);
            return result;
        }
    }
}
