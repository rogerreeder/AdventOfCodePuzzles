using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCodePuzzles
{
    static public class Day02
    {
        enum Pieces{
            Rock,
            Paper,
            Scissors
        }

        enum Win_Lose{
            Lose,
            Draw,
            Win
        }
        public static string Part1()
        {
            var lines = File.ReadLines("Assets/Day02.txt").ToList();
            var sum = 0;
            foreach(var line in lines){
                var parts = line.Split(" ");
                sum += score(MyPiece(parts[1]), TheirPiece(parts[0]));
            }
            return $"Score Part 1 {sum}";
        }

        public static string Part2()
        {
            var lines = File.ReadLines("Assets/Day02.txt").ToList();
            var sum = 0;
            foreach(var line in lines){
                var parts = line.Split(" ");
                var theirPiece = TheirPiece(parts[0]);
                sum += score(ToWinLoseDraw(theirPiece, MustWin_Lose_Draw(parts[1])), theirPiece);
            }
            return $"Score Part 2 {sum}";
        }

        private static Pieces TheirPiece(string strPiece){
            var charNumber = (int)strPiece[0];
            var charOfA = (int)'A';
            return (Pieces) (charNumber - charOfA);
        }
        private static Pieces MyPiece(string strPiece){
            var charNumber = (int)strPiece[0];
            var charOfX = (int)'X';
            return (Pieces) (charNumber - charOfX);
        }
        private static Win_Lose MustWin_Lose_Draw(string strPiece){
            var charNumber = (int)strPiece[0];
            var charOfX = (int)'X';
            return (Win_Lose) (charNumber - charOfX);
        }

        private static  Pieces ToWinLoseDraw(Pieces theirPiece, Win_Lose winLoseDraw){
            if(winLoseDraw == Win_Lose.Draw) return theirPiece;
            switch(theirPiece){
                case Pieces.Rock:
                    if(winLoseDraw == Win_Lose.Lose)
                        return Pieces.Scissors;
                    else
                        return Pieces.Paper;
                case Pieces.Paper:
                    if(winLoseDraw == Win_Lose.Lose)
                        return Pieces.Rock;
                    else
                        return Pieces.Scissors;
                default:
                    if(winLoseDraw == Win_Lose.Lose)
                        return Pieces.Paper;
                    else
                        return Pieces.Rock;
            }
        }
        private static int score(Pieces myPiece, Pieces theirPiece){
            var result = (int)myPiece + 1;
            if(theirPiece == myPiece)
                result += 3;
            else
                switch (theirPiece){
                    case Pieces.Scissors:
                        if(myPiece == Pieces.Rock)
                            result += 6;
                        break;
                    case Pieces.Rock:
                        if(myPiece == Pieces.Paper)
                            result += 6;
                        break;
                    case Pieces.Paper:
                        if(myPiece == Pieces.Scissors)
                            result += 6;
                        break;
                }
            //Console.WriteLine($"{theirPiece} vs {myPiece} result: {result}");
            return result;
        }
    }
}
