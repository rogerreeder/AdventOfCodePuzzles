using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCodePuzzles
{
    static public class Day03
    {
        private static List<string>? lines;
        public static string Part1()
        {
            var priorityList = new List<int>();
            lines = File.ReadLines("Assets/Day03.txt").ToList();
            foreach(var line in lines){
                var itemsList = new List<int>();
                for(var i = 0; i < line.Length;i++)
                    itemsList.Add(GetItemPriority(line[i]));
                var items = itemsList.ToArray();
                var firstHalf = items.Take(items.Length/2).ToArray();
                var secondHalf = items.Skip(items.Length/2).ToArray();
                var priorityItemsInSackList = new List<int>();
                for(var i = 0; i < firstHalf.Length; i++)
                    if(secondHalf.Contains(firstHalf[i]) && !priorityItemsInSackList.ToArray().Contains(items[i])){
                        priorityItemsInSackList.Add(items[i]);
                        priorityList.Add(items[i]);
                    }
            }
            return $"Part2:\n\tSum: {priorityList.ToArray().Sum()}";
        }

        public static string Part2()
        {
            if(lines == null)
                return $"No Data...";
            var priorityList = new List<int>();
            var lineNumber = 0;
            var sack1 = "";
            var sack2 = "";
            var sack3 = "";
            var badgesList = new List<int>();
            foreach(var line in lines){
                switch(lineNumber %3){
                    case 0://first sack of group
                        sack1 = line;
                        break;
                    case 1: //second sack of group
                        sack2 = line;
                        break;
                    case 2: //and third sack of group
                        sack3 = line;
                        // Now compare them...
                        var badge = GetBadgePriority(sack1, sack2, sack3);
                        if(badge > 0)
                            badgesList.Add(badge);
                        break;
                }
                lineNumber++;
            }
            return $"Part2:\n\tSum: {badgesList.ToArray().Sum()}";
        }

        private static int GetBadgePriority(string sack1, string sack2, string sack3){
            var items1 = GetPriorityItemArray(sack1);
            var items2 = GetPriorityItemArray(sack2);
            var items3 = GetPriorityItemArray(sack3);
            foreach(var item in items1){
                if(items2.Contains(item) && items3.Contains(item))
                    return item;
            }
            return 0;
        }

        private static int[] GetPriorityItemArray(string items){
            var itemsList = new List<int>();
            for(var i = 0; i < items.Length;i++)
                itemsList.Add(GetItemPriority(items[i]));
            return itemsList.ToArray();
        }

        private static int GetItemPriority(char item){
            var itemPriority = 0;
            if(item < 'a')
                itemPriority = (int)item - 'A' + 27;
            else
                itemPriority = (int)item - 'a' + 1;
            return itemPriority;
        }

        private static char GetCharFromPriority(int priority){
            if(priority>26)
                return (char)(priority + 'A' -27);
            else
                return (char)(priority + 'a' - 1);

        }
    }
}
