using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AdventOfCodePuzzles.Helpers
{
    public static class UsingHumanizer
    {
        public static string ConvertWordsToNumbers(string line)
        {
            var parsedLine = line;
            if (char.IsDigit(parsedLine[0]) && char.IsDigit(parsedLine[parsedLine.Length - 1]))
                return parsedLine;

                Dictionary<string, string> mapping = new Dictionary<string, string>
            {
                {"oneight", "oneeight"},
                {"twone", "twoone"},
                {"threeight", "threeeight"},
                {"fiveight", "fiveeight"},
                {"sevenine", "sevennine"},
                {"eightwo", "eighttwo"},
                {"nineight", "nineeight"},
                {"one", "1"},
                {"two", "2"},
                {"three", "3"},
                {"four", "4"},
                {"five", "5"},
                {"six", "6"},
                {"seven", "7"},
                {"eight", "8"},
                {"nine", "9"}
            };
            foreach(var kv in  mapping)
                parsedLine = parsedLine.Replace(kv.Key, kv.Value);
            return parsedLine;
        }
        public static string ReplaceNonNumericWithAstrick(string input)
        {
            // Use regular expression to replace non-numeric characters with a period
            string result = Regex.Replace(input, "[^0-9]", "*");

            return result;
        }
    }
}
