using AdventOfCodePuzzles.Helpers;
using System.Runtime.Caching;
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
        private static MemoryCache memCache = MemoryCache.Default;
        private static readonly SemaphoreLocker _locker = new SemaphoreLocker();

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

        public static async Task<string> Part2Async()
        {

            var sum = 0L;
            var filePath = @"F:\logs\20230402.log";
            if (File.Exists(filePath))
                File.Delete(filePath);
            var cards = File.ReadLines("Assets/2023/Day04.txt").ToArray();
            var lineNumber = 0;
            try
            {
                var sw = Stopwatch.StartNew();
                var processes = new List<Task<int>>();
                File.AppendAllText(filePath, $"[{sw.Elapsed}]\n");
                for (var cardIndex = 0; cardIndex < cards.Length; cardIndex++)
                    processes.Add(ProcessCardAsync(cards, cardIndex, 0));
                await Task.WhenAll(processes);
                File.AppendAllText(filePath, $"[{sw.Elapsed}] Completed\n");
                foreach(var kvPair in memCache)
                {
                    var newCards = new List<int>();
                    var originalCardNumber = int.Parse(kvPair.Key.Substring(1));
                    var newCardNumber = originalCardNumber + (int)kvPair.Value;
                    File.AppendAllText(filePath, $"{kvPair.Key}:");
                    for (var i = originalCardNumber + 1; i <= newCardNumber; i++)
                        File.AppendAllText(filePath, $"Card {i}{(i < newCardNumber ? ", " : "")}");
                    File.AppendAllText(filePath, "\n");
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

        private static async Task<int> ProcessCardAsync(string[] cards, int cardIndex, int nesting)
        {
            var key = $"C{cardIndex + 1}";
            var matches = await GetFromCache(memCache, key);
            if(matches < 0)
            {
                var cardPieces = cards[cardIndex].Split(':');
                var cardNumber = int.Parse(cardPieces[0].Split(' ')[1].Trim());
                var playerAndWinners = cardPieces[1].Split('|');
                var winningNumber = playerAndWinners[0].Trim().Split(" ");
                var playerNumbers = playerAndWinners[1].Trim().Split(" ");
                matches = 0;
                foreach (var playerNumber in playerNumbers)
                    if (winningNumber.Contains(playerNumber))
                        matches++;
                await AddToCacheAsync(cache: memCache, $"C{cardIndex + 1}", matches);
                /*
                var processes = new List<Task>();
                for (var i = 1; i <= matches; i++)
                    if (cardIndex + i < cards.Length - 1)
                        processes.Add(ProcessCardAsync(cards, cardIndex + i, nesting + 1));
                await Task.WhenAll(processes);
                */
            }
            return matches;
        }
        static async Task AddToCacheAsync(MemoryCache cache, string key, int value)
        {
            // Set cache policy (optional)
            CacheItemPolicy policy = new CacheItemPolicy
            {
                AbsoluteExpiration = DateTimeOffset.Now.AddMinutes(10) // Cache item will expire after 10 minutes
            };
            var currentValue = await GetFromCache(cache, key);
            if (currentValue < 0)
                cache.Add(key, value, policy);

        }

        static async Task<int> GetFromCache(MemoryCache cache, string key)
        {
            int? cachedValue = null;
            await _locker.LockAsync(async () =>
            {
                cachedValue = (int) await Task<int>.FromResult(cache.Get(key) ?? -1);
            });
            if (cachedValue != null && cachedValue is int)
                return (int)cachedValue;
            return -1;
        }
    }
}
