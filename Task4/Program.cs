using System.Text.RegularExpressions;

namespace Task4;

class Program
{
    static void Main(string[] args)
    {
        var inputPath = Path.Combine(new[]{"..", "..", "..", "files", "expressions.txt"});
        var outputPath = Path.Combine(new[]{"..", "..", "..", "files", "expressionsOutput.txt"});
        CalculateFromFile(inputPath, outputPath);

        GetUserInputCalculations();
    }

    private static void CalculateFromFile(string inputPath, string outputPath)
    {
        var newFileLines = new List<string>(); 
        foreach (var line in File.ReadLines(inputPath))
        {
            string result;
            try
            {
                var calculator = new Calculator(line);
                result = calculator.Evaluate().ToString();
            }
            catch (Exception ex) when (ex is ArgumentException or ApplicationException or DivideByZeroException)
            {
                result = ex.Message;
            }

            var newLine = line + " = " + result;
            newFileLines.Add(newLine);
        }
        
        using (StreamWriter outputFile = new StreamWriter(outputPath))
        {
            foreach (string line in newFileLines)
                outputFile.WriteLine(line);
        }
    }
    
    private static void GetUserInputCalculations()
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