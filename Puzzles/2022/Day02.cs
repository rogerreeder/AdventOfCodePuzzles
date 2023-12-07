using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCodePuzzles.Year2022
{
    static public class Day02
    {
        public static string Part1()
        {
            lines = File.ReadLines("Assets/Day02.txt").ToList();
            return $"Part1:\n\tSum: {lines.Sum(l => score(l.Split(" ")))}";
        }

        public static string Part2() => $"Part2:\n\tSum: {lines?.Sum(l => score2(ToWinLoseDraw(TheirPiece(l.Split(" ")[0]),MustWin_Lose_Draw(l.Split(" ")[1])),TheirPiece(l.Split(" ")[0])))}";
        #region private
        private static List<string>? lines;
        private enum Pieces{
            Rock,
            Paper,
            Scissors
        }

        private enum Win_Lose{
            Lose,
            Draw,
            Win
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
        private static int score2(Pieces myPiece, Pieces theirPiece){
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

        private static int score(string[] parts){
            var myPiece = MyPiece(parts[1]);
            var theirPiece = TheirPiece(parts[0]);
            return score2(myPiece,theirPiece);
        }
        #endregion //private
    }
}
