using System;

namespace Proxx
{
    class Program
    {
        static void Main(string[] args)
        {
            int boardSize = 20;
            int blackHolesCount = 64;
            var proxxGame = new ProxxModel(boardSize, blackHolesCount);

            var printer = new ProxxPrinter();
            printer.Print(proxxGame);

            // example of incoming call from UI
            Console.ReadLine();
            proxxGame.OpenCell(0, 0);
            printer.Print(proxxGame);
            Console.ReadLine();
            proxxGame.OpenCell(1, 1);
            printer.Print(proxxGame);
            Console.ReadLine();
            proxxGame.OpenCell(2, 1);
            printer.Print(proxxGame);
            Console.ReadLine();
            proxxGame.OpenCell(1, 2);
            printer.Print(proxxGame);

            Console.WriteLine("Game is finished");
        }
    }
}
