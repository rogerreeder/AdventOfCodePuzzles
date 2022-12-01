// See https://aka.ms/new-console-template for more information
using AdventOfCodePuzzles;

const int CURRENTDAY = 2;
var notExit = true;
showMenu();
Console.WriteLine("Tap Key to run, ? for menu or X to exit");
while (notExit)
{
    var selected = Console.ReadKey();
    Console.WriteLine("");
    switch (selected.KeyChar)
    {
        case '?':
            showMenu();
            break;
        case '1':
            Console.WriteLine(Day01.MaxCalories());
            Console.WriteLine(Day01.Top3GroupOfCalories());
            break;
        case '2':
            Console.WriteLine(Day02.Part1());
            Console.WriteLine(Day02.Part2());
            break;
        default:
            notExit = false;
            break;
    }
    Console.WriteLine("Tap Key to run, ? for menu or X to exit");
}


void showMenu()
{
    Console.WriteLine("************ MENU **********");
    for (var key = 1; key < CURRENTDAY; key++)
    {
        Console.WriteLine($"{(char)(48 + (key < 10 ? key : key + 7))} - Advent of Code 2022 ({key})");
    }

}