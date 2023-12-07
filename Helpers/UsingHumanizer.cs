using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCodePuzzles.Helpers
{
    public static class UsingHumanizer
    {
        public static string ConvertWordsToNumbers(string line)
        {
            var parsedLine = line;
            var atEnd = parsedLine.Substring(parsedLine.Length - 1);
            var atStart = parsedLine[0];
            if (char.IsDigit(atStart) && char.IsDigit(atEnd[0]))
                return parsedLine;

                Dictionary<string, string> wordToNumberMap = new Dictionary<string, string>
            {
                {"one", "1"},
                {"two", "2"},
                {"three", "3"},
                {"four", "4"},
                {"five", "5"},
                {"six", "6"},
                {"seven", "7"},
                {"eight", "8"},
                {"nine", "9"}
                /*
                {"ten", "10"},
                {"eleven", "11"},
                {"twelve", "12"},
                {"thirteen", "13"},
                {"fourteen", "14"},
                {"fifteen", "15"},
                {"sixteen", "16"},
                {"seventeen", "17"},
                {"eighteen", "18"},
                {"nineteen", "19"},
                {"twenty", "20"},
                {"thirty", "30"},
                {"forty", "40"},
                {"fifty", "50"},
                {"sixty", "60"},
                {"seventy", "70"},
                {"eighty", "80"},
                {"ninety", "90"},
                */
                // Add more mappings as needed
            };
            for (var i = 3; i <= parsedLine.Length; i++)
            {
                var word = parsedLine.Substring(0, i);
                foreach (var kv in wordToNumberMap)
                    if (word.Contains(kv.Key))
                    {
                        parsedLine = parsedLine.Replace(kv.Key, kv.Value);
                        break;
                    }
            }
            /*
            var atEnd = parsedLine.Substring(parsedLine.Length - 1);
            if(!char.IsDigit(atEnd[0])) {
                var exit = false;
                for (var i = parsedLine.Length - 3; i > 0 ; i--)
                {
                    var word = parsedLine.Substring(i);
                    foreach (var kv in wordToNumberMap)
                        if (word.Contains(kv.Key))
                        {
                            parsedLine = parsedLine.Replace(kv.Key, kv.Value);
                            exit = true;
                            break;
                        }
                    if (exit) break;
                }
            }
            */
            return parsedLine;
        }
    }
}
