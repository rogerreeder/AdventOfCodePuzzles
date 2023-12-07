// See https://aka.ms/new-console-template for more information
using AdventOfCodePuzzles;
using AdventOfCodePuzzles.Year2023;

const int NEXTDAY = 10;
showMenu();
var notExit = true;
while (notExit)
{
    try
    {
        switch (showPrompt().KeyChar)
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
                //Console.WriteLine(Day02.Part2());
                break;
            /*
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
        case '9':
            Console.WriteLine(Day09.Part1());
            Console.WriteLine(Day09.Part2());
            break;
        case 'a':
            Console.WriteLine(Day10.Part1());
            Console.WriteLine(Day10.Part2());
            break;
            */
            default:
                notExit = false;
                break;
        }
    }
    catch (System.Exception ex)
    {
        Console.WriteLine($"[ERROR] {ex.Message}");
    }
}

void showMenu()
{
    Console.WriteLine("".PadLeft(36,'*') + " M E N U " + "".PadLeft(35,'*'));
    for (var key = 1; key <= NEXTDAY; key++)
        Console.WriteLine($"{(char)(48 + (key < 10 ? key : key + 7))} - Advent of Code 2023 ({key})");
}

ConsoleKeyInfo showPrompt(){
    Console.Write($"{("".PadLeft(80,'*'))}\n>>Tap Key to run, ? for menu or X to exit:");
    var key = Console.ReadKey();
    Console.WriteLine("\n".PadRight(80,'*'));
    return key;
}