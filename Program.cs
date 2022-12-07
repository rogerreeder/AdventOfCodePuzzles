// See https://aka.ms/new-console-template for more information
using AdventOfCodePuzzles;

const int NEXTDAY = 8;
var notExit = true;
showMenu();
Console.WriteLine("Tap Key to run, ? for menu or X to exit");
while (notExit)
{
    var selected = Console.ReadKey();
    Console.WriteLine("");
    try
    {
        switch (selected.KeyChar)
        {
            case '?':
                showMenu();
                break;
            case '1':
                Console.WriteLine(Day01.Part1());
                Console.WriteLine(Day01.Part2());
                break;
            case '2':
                Console.WriteLine(Day02.Part1());
                Console.WriteLine(Day02.Part2());
                break;
            case '3':
                Console.WriteLine(Day03.Part1());
                Console.WriteLine(Day03.Part2());
                break;
            case '4':
                Console.WriteLine(Day04.Part1());
                Console.WriteLine(Day04.Part2());
                break;
            case '5':
                Console.WriteLine(Day05.Part1());
                Console.WriteLine(Day05.Part2());
                break;
            case '6':
                Console.WriteLine(Day06.Part1());
                Console.WriteLine(Day06.Part2());
                break;
            case '7':
                Console.WriteLine(Day07.Part1());
                Console.WriteLine(Day07.Part2());
                break;
            case '8':
                Console.WriteLine(Day08.Part1());
                Console.WriteLine(Day08.Part2());
                break;
            default:
                notExit = false;
                break;
        }
    }
    catch (System.Exception ex)
    {
        Console.WriteLine($"[ERROR] {ex.Message}");
    }
    finally{
        Console.WriteLine("Tap Key to run, ? for menu or X to exit");
    }
}


void showMenu()
{
    Console.WriteLine("************ MENU **********");
    for (var key = 1; key <= NEXTDAY; key++)
    {
        Console.WriteLine($"{(char)(48 + (key < 10 ? key : key + 7))} - Advent of Code 2022 ({key})");
    }

}