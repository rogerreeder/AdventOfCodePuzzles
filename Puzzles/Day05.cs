using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCodePuzzles
{
    static public class Day05
    {
        const string FILENAME = "Assets/Day05.txt";
        private static List<string>? lines;
        private static string[]? stacks;
        public static string Part1()
        {
            if(lines == null)
                lines = File.ReadLines(FILENAME).ToList();
            var numberOfStacks = (lines[0].Length+1)/4;
            stacks = new string[numberOfStacks];
            foreach(var line in lines){
                if(line != "") {
                    var parts = line.Split(' ');
                    if(parts[0] != "1" && parts[0] != "move")
                        parseLoad(line);
                    else if(line[0] == 'm')
                        parseMove(line);
                }
            }
            return $"Part1:\n\tCode: {Code()}";
        }

        public static string Part2()
        {
            if(lines == null)
                lines = File.ReadLines(FILENAME).ToList();
            var numberOfStacks = (lines[0].Length+1)/4;
            stacks = new string[numberOfStacks];
            foreach(var line in lines){
                if(line != "") {
                    var parts = line.Split(' ');
                    if(parts[0] != "1" && parts[0] != "move")
                        parseLoad(line);
                    else if(line[0] == 'm'){
                        try
                        {
                            MoveFromTo(int.Parse(parts[3]), int.Parse(parts[5]), int.Parse(parts[1]));
                        }
                        catch (System.Exception ex)
                        {
                            Console.WriteLine($"[ERROR] {line}");
                            Console.WriteLine($"[ERROR][MoveFromTo] - {ex.Message}\n{ex.StackTrace}");
                            throw;
                        }
                    }
                }
            }
            return $"Part2:\n\tCode: {Code()}";
        }


        public static void parseLoad(string line){
            if(stacks == null) return;
            for(var i = 1; i < line.Length; i+=4){
                if(line[i] != ' ') stacks[i/4] = line[i] + stacks[i/4];
            }
        }
        public static void parseMove(string line){
            var parts = line.Split(' ');
            for(var i = 1; i <= int.Parse(parts[1]); i++)
                MoveFromTo(int.Parse(parts[3]), int.Parse(parts[5]), 1);
            return;
        }

        public static void MoveFromTo(int From, int To){
            if(stacks == null) return;
            stacks[To - 1] += stacks[From - 1].Substring(stacks[From - 1].Length - 1);
            stacks[From - 1] = stacks[From - 1].Substring(0,stacks[From - 1].Length - 1);
        }
        public static void MoveFromTo(int From, int To, int Move){
            if(stacks == null) return;
            stacks[To - 1] += stacks[From - 1].Substring(stacks[From - 1].Length - Move);
            stacks[From - 1] = stacks[From -1].Substring(0,stacks[From - 1].Length - Move);
        }

        public static string Code(){
            var code = "";
            if(stacks != null)
                foreach(var stack in stacks)
                    code += stack.Substring(stack.Length -1);
            return code;
        }

        public static void PrintStacks(){
            if(stacks == null) return;
            foreach(var stack in stacks)
                Console.WriteLine($"{stack}");
        }
    }
}
