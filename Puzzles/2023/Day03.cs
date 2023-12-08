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
    static public class Day03
    {
        public static string Part1()
        {
            var sum = 0L;
            var filePath = @"F:\logs\20230301.log";
            if (File.Exists(filePath))
                File.Delete(filePath);
            var lines = File.ReadLines("Assets/2023/Day03.txt").ToList().ToArray();
            
            int numRows = lines.Length;
            int numCols = lines[0].Length;

            // Define the directions to check (left, up-left,up, up-right, right, down-right, down, down-left)
            var leftDirection = new Point(-1, 0);
            var upLeftDirection = new Point(-1, -1);
            var upDirection = new Point(0, -1);
            var upRightDirection = new Point(1, -1);
            var rightDirection = new Point(1, 0);
            var downRightDirection = new Point(1, 1);
            var downDirection = new Point(0, 1);
            var downLeftDirection = new Point(-1, 1);

            var rowIndex = 0;
            try
            {
                var engineSymbols = new Dictionary<string, string>();
                foreach (var line in lines)
                {
                    for(var charIndex = 0; charIndex < line.Length; charIndex++)
                    {
                        var possibleSymbol = line[charIndex];
                        if (possibleSymbol != '.' && !char.IsDigit(possibleSymbol))
                        {
                            var previousPartNumber = string.Empty;
                            for (int i = 0; i < 8; i++)
                            {
                                Point direction = new Point(0,0);
                                switch (i)
                                {
                                    case 0:direction = leftDirection; break;
                                    case 1:direction = upLeftDirection; break;
                                    case 2:direction = upDirection; break;
                                    case 3:direction = upRightDirection; break; 
                                    case 4:direction = rightDirection; break;
                                    case 5:direction = downRightDirection; break;
                                    case 6:direction = downDirection; break;
                                    case 7:direction = downLeftDirection; break;
                                }
                                int colDirection = direction.X;
                                int newCol = charIndex + colDirection;
                                int rowDirection = direction.Y;
                                int newRow = rowIndex + rowDirection;

                                // Check if the new position is within bounds
                                if (newRow >= 0 && newRow < numRows && newCol >= 0 && newCol < numCols)
                                {
                                    // Check if the neighboring cell contains a number
                                    if (char.IsDigit(lines[newRow][newCol]))
                                    {
                                        var partNumber = $"{lines[newRow][newCol]}";
                                        var x1 = Math.Max(newCol - 2, 0);
                                        var x2 = Math.Min(newCol + 2, lines[newRow].Length - 1);
                                        var PartKey = $"{possibleSymbol}{newCol}_{newRow}";
                                        for (var pos = newCol - 1; pos >= x1; pos--)
                                        {
                                            var nextChar = lines[newRow][pos];
                                            if (char.IsDigit(nextChar))
                                            {
                                                partNumber = $"{nextChar}{partNumber}";
                                                PartKey = $"{possibleSymbol}{pos}_{newRow}";
                                            }
                                            else
                                                break;
                                        }
                                        for (var pos = newCol + 1; pos <= x2; pos++)
                                        {
                                            var nextChar = lines[newRow][pos];
                                            if (char.IsDigit(nextChar))
                                                partNumber = $"{partNumber}{nextChar}";
                                            else
                                                break;
                                        }

                                        if (engineSymbols.ContainsKey(PartKey))
                                        {
                                            File.AppendAllText(filePath, $"[{sum}][{PartKey}][{possibleSymbol}][{charIndex}, {rowIndex}][{partNumber}] ALREADY EXISTS\n");
                                        }
                                        else
                                        {
                                            engineSymbols.Add(PartKey, partNumber);
                                            previousPartNumber = partNumber;
                                            File.AppendAllText(filePath, $"[{sum}][{PartKey}][{possibleSymbol}][{charIndex}, {rowIndex}][{partNumber}]\n");
                                            sum += int.Parse(partNumber);
                                        }
                                    }
                                }
                            }

                        }
                    }
                    /*
                    foreach (Match match in matches)
                    {
                        var x1 = Math.Max(match.Index - 1, 0);
                        var y1 = Math.Max(rowIndex - 1, 0);
                        var x2 = Math.Min(match.Index + match.Value.Length, line.Length - 1);
                        var y2 = Math.Min(rowIndex + 1, lines.Length - 1);
                        var isPart = false;
                        for (var x = x1; x <= x2; x++)
                        {
                            for (var y = y1; y <= y2; y++)
                            {
                                var checkChar = lines[y][x];
                                if (!isPart && checkChar != '.' && !char.IsLetter(checkChar) && !char.IsNumber(checkChar))
                                    isPart = true;
                            }
                        }
                        if (isPart)
                            sum += int.Parse(match.Value);
                        File.AppendAllText(filePath, $"{match.Value} {(isPart ? "+" : "!")}\n");
                    }*/
                    rowIndex++;
                }
                File.AppendAllText(filePath, $"------\n{sum}\n");
                return $"Part1:\n\tTotal: {sum}";

            }
            catch (Exception ex)
            {
                File.AppendAllText(filePath, $"\n[{rowIndex,4}] {ex}\n");
                throw;
            }
        }

        public static string Part2()
        {
            var sum = 0L;
            var filePath = @"F:\logs\20230302.log";
            if (File.Exists(filePath))
                File.Delete(filePath);
            var lines = File.ReadLines("Assets/2023/Day03.txt").ToList().ToArray();

            int numRows = lines.Length;
            int numCols = lines[0].Length;

            // Define the directions to check (left, up-left,up, up-right, right, down-right, down, down-left)
            var leftDirection = new Point(-1, 0);
            var upLeftDirection = new Point(-1, -1);
            var upDirection = new Point(0, -1);
            var upRightDirection = new Point(1, -1);
            var rightDirection = new Point(1, 0);
            var downRightDirection = new Point(1, 1);
            var downDirection = new Point(0, 1);
            var downLeftDirection = new Point(-1, 1);

            var rowIndex = 0;
            try
            {
                foreach (var line in lines)
                {
                    for (var charIndex = 0; charIndex < line.Length; charIndex++)
                    {
                        var possibleSymbol = line[charIndex];
                        if (possibleSymbol == '*')
                        {
                            var engineSymbols = new Dictionary<string, string>();
                            var previousPartNumber = string.Empty;
                            for (int i = 0; i < 8; i++)
                            {
                                Point direction = new Point(0, 0);
                                switch (i)
                                {
                                    case 0: direction = leftDirection; break;
                                    case 1: direction = upLeftDirection; break;
                                    case 2: direction = upDirection; break;
                                    case 3: direction = upRightDirection; break;
                                    case 4: direction = rightDirection; break;
                                    case 5: direction = downRightDirection; break;
                                    case 6: direction = downDirection; break;
                                    case 7: direction = downLeftDirection; break;
                                }
                                int colDirection = direction.X;
                                int newCol = charIndex + colDirection;
                                int rowDirection = direction.Y;
                                int newRow = rowIndex + rowDirection;

                                // Check if the new position is within bounds
                                if (newRow >= 0 && newRow < numRows && newCol >= 0 && newCol < numCols)
                                {
                                    // Check if the neighboring cell contains a number
                                    if (char.IsDigit(lines[newRow][newCol]))
                                    {
                                        var partNumber = $"{lines[newRow][newCol]}";
                                        var x1 = Math.Max(newCol - 2, 0);
                                        var x2 = Math.Min(newCol + 2, lines[newRow].Length - 1);
                                        var PartKey = $"{possibleSymbol}{newCol}_{newRow}";
                                        for (var pos = newCol - 1; pos >= x1; pos--)
                                        {
                                            var nextChar = lines[newRow][pos];
                                            if (char.IsDigit(nextChar))
                                            {
                                                partNumber = $"{nextChar}{partNumber}";
                                                PartKey = $"{possibleSymbol}{pos}_{newRow}";
                                            }
                                            else
                                                break;
                                        }
                                        for (var pos = newCol + 1; pos <= x2; pos++)
                                        {
                                            var nextChar = lines[newRow][pos];
                                            if (char.IsDigit(nextChar))
                                                partNumber = $"{partNumber}{nextChar}";
                                            else
                                                break;
                                        }

                                        if (!engineSymbols.ContainsKey(PartKey))
                                        {
                                            engineSymbols.Add(PartKey, partNumber);
                                        }
                                    }
                                }
                            }
                            if(engineSymbols.Count == 2)
                            {
                                var firstGear = 0L;
                                var secondGear = 0L;
                                foreach (var kvPair in engineSymbols)
                                {
                                    if (firstGear == 0L)
                                        firstGear = long.Parse(kvPair.Value);
                                    else
                                        secondGear = long.Parse(kvPair.Value);
                                }
                                sum += firstGear * secondGear;
                            }
                        }
                    }
                    rowIndex++;
                }
                File.AppendAllText(filePath, $"------\n{sum}\n");
                return $"Part2:\n\tTotal: {sum}";

            }
            catch (Exception ex)
            {
                File.AppendAllText(filePath, $"\n[{rowIndex,4}] {ex}\n");
                throw;
            }

        }
    }
}
