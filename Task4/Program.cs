using System.Text.RegularExpressions;

namespace Task4;

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("_____Calculator_____");
        while (true)
        {
            try
            {
                Console.WriteLine("Enter an expression: ");
                string expr = Console.ReadLine();
                var rvp = new Calculator(expr);

                rvp.PrintReversedPolishNotation();
         
                var result = rvp.Evaluate();
                Console.WriteLine($"Result is {result}");
            }
            catch (Exception ex) when (ex is ArgumentException or ApplicationException or DivideByZeroException)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(ex.Message);
                Console.ResetColor();
            }

        }


    }
}