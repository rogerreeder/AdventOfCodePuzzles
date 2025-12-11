using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCodePuzzles.Year2022
{
    class Point {
        public Point(int x, int y){
            X = x;
            Y = y;
        }
        public int X {get;set;}
        public int Y {get;set;}
    }
    static public class Day09
    {
        const string FILENAME = "Assets/Day09.demo.txt";
        private static List<string>? lines;
        private static Point? startPoint;
        private static Point? headPoint;
        private static Point? tailPoint;
        private static Dictionary<string,int> tailPoints = new Dictionary<string, int>();
        private static string[] plot = new string[5];
        private static Point maxPoint = new Point(0,0);
        private static Point minPoint = new Point(0,0);
        public static string Part1()
        {
            if(lines == null)
                lines = File.ReadLines(FILENAME).ToList();
            startPoint = new Point(0,0);
            headPoint = new Point(0,0);
            foreach(var line in lines)
                figureStart(line.Split(' '));
            headPoint = new Point(startPoint.X, startPoint.Y);
            tailPoint = new Point(startPoint.X, startPoint.Y);
            tailPoints.Add("0_0",1);
            showPlot();

            foreach(var line in lines)
                moveRope(line.Split(' '));
            foreach(var tailPointItem in tailPoints)
                Console.WriteLine($"key: {tailPointItem.Key} {tailPointItem.Value}");
            //if(!FILENAME.Contains("demo"))
                //showPlot();
            return $"Part1:"
                + $"\n\tpoints:{tailPoints.Count} {tailPoints.Where(p => p.Value > 0).Count()}";
        }

        public static string Part2()
        {
            return $"Part2:"
                + $"\n\tNot Implemented";
        }

        private static void figureStart(string[] command){
            var direction = command[0][0];
            var distance = int.Parse($"{command[1]}");
            var x = 0;
            var y = 0;
            switch(direction){
                case 'U':
                    y = 1;
                    break;
                case 'R':
                    x = 1;
                    break;
                case 'D':
                    y = -1;
                    break;
                case 'L':
                    x = -1;
                    break;
            }
            headPoint.X += x * distance;
            headPoint.Y += y * distance;
            if(maxPoint.X < headPoint.X)
                maxPoint.X = headPoint.X;
            if(maxPoint.Y < headPoint.Y)
                maxPoint.Y = headPoint.Y;
            if(minPoint.X > headPoint.X)
                minPoint.X = headPoint.X;
            if(minPoint.Y > headPoint.Y)
                minPoint.Y = headPoint.Y;

            startPoint.X = -minPoint.X;
            startPoint.Y = -minPoint.Y;
            maxPoint.X = maxPoint.X - minPoint.X;
            maxPoint.Y = maxPoint.Y - minPoint.Y;

        }

        private static void moveRope(string[] command){
            var direction = command[0][0];
            var distance = int.Parse($"{command[1]}");
            var x = 0;
            var y = 0;
            switch(direction){
                case 'U':
                    y = 1;
                    break;
                case 'R':
                    x = 1;
                    break;
                case 'D':
                    y = -1;
                    break;
                case 'L':
                    x = -1;
                    break;
            }
            for(var i = 0; i < distance;i++){
                eachStep(x,y);
                showPlot();
                System.Threading.Thread.Sleep(100);
            }
        }

        private static void eachStep(int x, int y){
            if(headPoint == null || tailPoint == null) throw new Exception("no head or tail");
            headPoint.X += x;
            headPoint.Y += y;
            if(Math.Abs(tailPoint.X - headPoint.X) > 1 || Math.Abs(tailPoint.Y - headPoint.Y) > 1)
                if(tailPoint.Y == headPoint.Y)
                    tailPoint.X += (tailPoint.X < headPoint.X) ? 1 : -1;
                else if(tailPoint.X == headPoint.X)
                    tailPoint.Y += (tailPoint.Y < headPoint.Y) ? 1 : -1;
                else {
                    tailPoint.X += (tailPoint.X < headPoint.X) ? 1 : -1;
                    tailPoint.Y += (tailPoint.Y < headPoint.Y) ? 1 : -1;
                }
            var tailKey = $"{tailPoint.X}_{tailPoint.Y}";
            if(tailPoints.ContainsKey(tailKey))
                tailPoints[tailKey]++;
            else
                tailPoints.Add(tailKey, 1);

        }

        private static void showPlot(){
            var width = 40;
            var height = 40;
            var leftEdge = headPoint.X - width/2;
            var rightEdge = headPoint.X + width/2;
            var topEdge = headPoint.Y + height/2;
            var bottomEdge = headPoint.Y - height/2; 
            try {
                var xOffset = rightEdge - leftEdge;
                var yOffset = topEdge - bottomEdge;

                var plot = new string[height];
                for(var i = height; i >= 0;i--){
                    var row = GetRowVisual(i + yOffset, xOffset, width);
                    /*
                    if(i + yOffset == startPoint?.Y)
                        row = SwapCharacterInString(row, startPoint.X, 's');
                    if(i == tailPoint?.Y)
                        row = SwapCharacterInString(row, tailPoint.X, 'T');
                    if(i == headPoint?.Y)
                        row = SwapCharacterInString(row, headPoint.X, 'H');
                        */
                    Console.WriteLine(row);
                }
            }
            catch (System.Exception ex)
            {
                Console.WriteLine($"\t[ERROR] {ex.Message}\n{ex.StackTrace}");
                throw;
            }
            Console.WriteLine("");
        }
        private static string GetRowVisual(int yOffset, int xOffSet, int width){
            var visualString = "";
            var gridMarks = 5;
            var gridMark = "+";
            var gridField = ".";
            for(var i = xOffSet; i < xOffSet + width; i++)
                visualString += (yOffset % gridMarks == 0) ? gridMark : ( i % gridMarks == 0) ? gridMark : gridField;
            return visualString;
        }
        private static string SwapCharacterInString(string text, int index, char newCharacter){
            //Console.WriteLine($"\t[SwapCharacterInString] {text} {index} {newCharacter}");
            try
            {
                System.Text.StringBuilder strBuilder = new System.Text.StringBuilder(text);
                strBuilder[index] = newCharacter;
                return strBuilder.ToString();
            }
            catch (System.Exception ex)
            {
                Console.WriteLine($"\t[ERROR][SwapCharacterInString] {text} {index} {newCharacter}");
                Console.WriteLine($"\t[ERROR][SwapCharacterInString] {ex.Message}\n{ex.StackTrace}");
                throw;
            }
        }
    }
}
