using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;
namespace AdventOfCodePuzzles.Year2022
{
    class FileInfo {
        public string Directory {get;set;} = string.Empty;
        public string FileName  {get;set;} = string.Empty;
        public long FileSize {get;set;}
    }
    static public class Day07
    {
        private static Dictionary<string,long> Directories = new Dictionary<string, long>();
        private static Dictionary<string,FileInfo> FileInfos = new Dictionary<string, FileInfo>();
        const string FILENAME = "Assets/Day07.txt";
        private static string currentDirectory = "";
        private static List<string>? lines;
        public static string Part1()
        {
            if(lines == null)
                lines = File.ReadLines(FILENAME).ToList();
            foreach(var line in lines)
                parseLine(line);

            foreach(var fileInfoItem in FileInfos){
                var fileInfo = fileInfoItem.Value;
                //Console.WriteLine($"fileInfo.Directory: {fileInfo.Directory} {fileInfo.FileName}");
                var directoryName = fileInfo.Directory;
                if(!Directories!.ContainsKey(directoryName))
                    Directories!.Add(directoryName, FileInfos.Where(f => f.Value.Directory.Contains(directoryName)).Sum(f => f.Value.FileSize));
            }
            foreach(var dirItem in Directories)
                Directories[dirItem.Key] = FileInfos.Where(f => f.Value.Directory.Contains(dirItem.Key)).Sum(f => f.Value.FileSize);
            return $"Part1:\n\tSum: {Directories.Where(d => d.Value <= 100000).Sum(d => d.Value)}";
        }

        public static string Part2()
        {
            var used = FileInfos.Sum(f => f.Value.FileSize);
            var free = 70000000 - used;
            var needed = 30000000 - free;
            var bestDirectory = "/";
            foreach(var directory in Directories){
                if(directory.Value > needed)
                    if(directory.Value < Directories[bestDirectory])
                        bestDirectory = directory.Key;
            }
            return $"Part2: \n\tUsed Space: {used}\n\tFree Space: {free}\n\tMore Needed: {needed}\n\tBest Directory: {bestDirectory} {Directories[bestDirectory]}";
        }

        private static void parseLine(string line){
            //Console.WriteLine($"{line}");
            var parts = line.Split(' ');
            switch(parts[0]){
                case "$"://command
                    ParseCommand(parts);
                    break;
                case "dir"://dir
                    //Console.WriteLine($"\tDIRECTORY - {currentDirectory}{(currentDirectory == "/" ? "" : "/")}{parts[1]}");
                    break;
                default://file
                    try
                    {
                        var newFileInfo = new FileInfo
                            {
                                Directory = currentDirectory
                                , FileName = parts[1]
                                , FileSize = long.Parse(parts[0])
                            };
                        FileInfos!.Add($"{currentDirectory}{(currentDirectory == "/" ? "" : "/")}{parts[1]}"
                            , newFileInfo);
                    }
                    catch (System.Exception ex)
                    {
                        Console.WriteLine($"\t[ERROR] {ex.Message}\n{ex.StackTrace}");
                    }
                    break;
            }
        }

        private static void ParseCommand(string[] parts){
            switch(parts[1]){
                case "cd":
                    if(parts[2] == "..")
                        try
                        {
                            currentDirectory = currentDirectory.Substring(0, currentDirectory.LastIndexOf("/"));
                            if(currentDirectory == "")
                                currentDirectory = "/";
                            if(!Directories!.ContainsKey(currentDirectory))
                                Directories.Add(currentDirectory,0L);
                        }
                        catch (System.Exception ex)
                        {
                            Console.WriteLine($"\tcurrentDirectory:{currentDirectory}");
                            Console.WriteLine($"\t[ERROR] {ex.Message}");
                            throw;
                        }
                    else if(parts[2] == "/")
                        currentDirectory = "/";
                    else
                        currentDirectory += (currentDirectory == "/" ? "" : "/") + $"{parts[2]}";
                    //Console.WriteLine($"\tCurrent Directory: {currentDirectory}");
                    break;
                case "ls":
                    break;
            }
        }

    }
}
