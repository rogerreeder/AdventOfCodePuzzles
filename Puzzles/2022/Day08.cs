using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCodePuzzles.Year2022
{
    public class  GridPoint{
        public int Row {get;set;}
        public int Col{get;set;}
    }
    static public class Day08
    {
        const string FILENAME = "Assets/Day08.txt";
        private static List<string>? lines;
        private static int[,]? TreeGrid;
        private static Dictionary<string,int> ScenicGrid = new Dictionary<string, int>();
        private static int maxRow = 0;
        private static int maxCol = 0;
        public static string Part1()
        {
            if(lines == null)
                lines = File.ReadLines(FILENAME).ToList();
            var treeRow = 0;
            var edgeTrees = 0;
            var innerVisibleTrees = 0;
            try
            {
                foreach(var line in lines){
                    if(TreeGrid == null){
                        TreeGrid = new int[lines.Count,line.Length];
                        maxRow = lines.Count;
                        maxCol = line.Length;
                    }
                    for(var treeIndex = 0; treeIndex < line.Length; treeIndex++)
                        TreeGrid[treeRow,treeIndex] = int.Parse($"{line[treeIndex]}");
                    treeRow++;
                }
                edgeTrees = maxRow * 2 + (2 * (maxCol - 2));
                for(treeRow = 1; treeRow < maxRow -1; treeRow++)
                    for(var treeCol = 1; treeCol < maxCol -1; treeCol++)
                        innerVisibleTrees += IsVisibleTree(new GridPoint{Row=treeRow, Col=treeCol}) ? 1 : 0;
            }
            catch (System.Exception ex)
            {
                Console.WriteLine($"\t[ERROR] {ex.Message}\n{ex.StackTrace}");
            }
            return $"Part1:\n\tOuter Edge Trees: {edgeTrees}\n\tInner Visible Trees: {innerVisibleTrees}\n\tVisible Trees: {innerVisibleTrees + edgeTrees}";
        }

        public static string Part2()
        {
            try
            {
                for(var treeRow = 1; treeRow < maxRow -1; treeRow++)
                    for(var treeCol = 1; treeCol < maxCol -1; treeCol++)
                        ScenicGrid.Add($"{treeRow},{treeCol}", ScenicScore(new GridPoint{Row=treeRow, Col=treeCol}));

                //foreach(var scenicItem in ScenicGrid)
                //    Console.WriteLine($"ScenicGrid[\"{scenicItem.Key}\"] {scenicItem.Value}");
            }
            catch (System.Exception ex)
            {
                Console.WriteLine($"\t[ERROR] {ex.Message}\n{ex.StackTrace}");
            }

            return $"Part2:\n\tMax Scenic Score: {ScenicGrid.Max(score => score.Value)}";
        }

        private static int ScenicScore(GridPoint treePoint){
            //Console.Write($"TreePoint({treePoint.Row},{treePoint.Col})");
            var total = ScenicScoreLeft(treePoint)
                   * ScenicScoreTop(treePoint)
                   * ScenicScoreRight(treePoint)
                   * ScenicScoreBottom(treePoint);
            //Console.WriteLine($" {total}");
            return total;
        }
        private static int ScenicScoreTop(GridPoint treePoint){
            var score = 0;
            for(var row = treePoint.Row - 1; row >= 0; row--){
                score++;
                if(TreeGrid?[row,treePoint.Col] >= TreeGrid?[treePoint.Row, treePoint.Col])
                    break;
            }
            //Console.Write($" {score}");
            return score;
        }
        private static int ScenicScoreBottom(GridPoint treePoint){
            var score = 0;
            for(var row = treePoint.Row + 1; row < maxRow; row++){
                score++;
                if(TreeGrid?[row,treePoint.Col] >= TreeGrid?[treePoint.Row, treePoint.Col])
                    break;
            }
            //Console.Write($" {score}");
            return score;
        }
        private static int ScenicScoreLeft(GridPoint treePoint){
            var score = 0;
            for(var col = treePoint.Col - 1; col >= 0; col--){
                score++;
                if(TreeGrid?[treePoint.Row,col] >= TreeGrid?[treePoint.Row, treePoint.Col])
                    break;
            }
            //Console.Write($" {score}");
            return score;
        }
        private static int ScenicScoreRight(GridPoint treePoint){
            var score = 0;
            for(var col = treePoint.Col + 1; col < maxCol; col++){
                score++;
                if(TreeGrid?[treePoint.Row, col] >= TreeGrid?[treePoint.Row, treePoint.Col])
                    break;
            }
            //Console.Write($" {score}");
            return score;
        }

        private static bool IsVisibleTree(GridPoint point){
            return VisibleTop(point)
                || VisibleBottom(point)
                || VisibleLeft(point)
                || VisibleRight(point);
        }
        private static bool VisibleLeft(GridPoint point){
            for(var col = 0;col < point.Col;col++)
                if(TreeGrid?[point.Row,col] >= TreeGrid?[point.Row,point.Col])
                    return false;
            return true;
        }
        private static bool VisibleRight(GridPoint point){
            for(var col = point.Col + 1;col < maxCol;col++)
                if(TreeGrid?[point.Row,col] >= TreeGrid?[point.Row,point.Col])
                    return false;
            return true;
        }
        private static bool VisibleTop(GridPoint point){
            for(var row = 0;row < point.Row;row++)
                if(TreeGrid?[row, point.Col] >= TreeGrid?[point.Row,point.Col])
                    return false;
            return true;
        }
        private static bool VisibleBottom(GridPoint point){
            for(var row = point.Row + 1;row < maxRow;row++)
                if(TreeGrid?[row, point.Col] >= TreeGrid?[point.Row,point.Col])
                    return false;
            return true;
        }
    }
}
