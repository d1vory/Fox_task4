using System.Text.RegularExpressions;

namespace Task4;

class Program
{
    static void Main(string[] args)
    {
        // string expr = "1 * (2 + 3) - 4";
        // var rvp = new ReversePolishNotation(expr);
        //
        // rvp.PrintReversedPolishNotation();
        //
        // Console.WriteLine(rvp.Evaluate());
        
        
        while (true)
        {
            //string expr = "4+18/(9-3)";
        
            string expr = Console.ReadLine();
            var rvp = new ReversePolishNotation(expr);
        
            rvp.PrintReversedPolishNotation();
            Console.WriteLine(rvp.Evaluate());
        }


    }
}