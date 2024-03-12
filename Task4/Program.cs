namespace Task4;

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine(
            "This is a simple calculator, which evaluates expressions. \n" + 
            "To start, choose a mode to work: \n" +
            "'1' is for console mode, to calculate in real-time\n" +
            "'2' is for file mode, which calculates expressions from the file\n" +
            "'3' to exit program");
        var shouldExit = false;
        do
        {
            Console.WriteLine("Enter mode: ");
            var userInput = Console.ReadLine();
            int.TryParse(userInput, out int mode);
            switch (mode)
            {
                case 1:
                    GetUserInputCalculations();
                    break;
                case 2:
                    CalculateFromFile();
                    break;
                case 3:
                    shouldExit = true;
                    break;
                default:
                    Console.WriteLine("Given mode is not valid");
                    break;
            }
        } while (!shouldExit);
    }

    private static void CalculateFromFile()
    {
        Console.WriteLine("__________file mode__________");
        Console.WriteLine("To exit, type 'exit'");
        
        Console.WriteLine("Enter a path to file: ");
        string inputPath = Console.ReadLine();
        if (inputPath == "exit") return;
        if (!File.Exists(inputPath))
        {
            Console.WriteLine("Given path to file does not exist");
        }
        
        Console.WriteLine("Enter a directory path to output file: ");
        string outputDirectory = Console.ReadLine();
        if (outputDirectory == "exit") return;
        if (!Directory.Exists(inputPath))
        {
            Console.WriteLine("Given directory path does not exist");
        }
        
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

        var outputFilePath = Path.Combine(outputDirectory, $"computed_{DateTime.Now}.txt");
        using (StreamWriter outputFile = new StreamWriter(outputFilePath))
        {
            foreach (string line in newFileLines)
                outputFile.WriteLine(line);
        }
        Console.WriteLine($"File was created at {outputFilePath}");
    }
    
    private static void GetUserInputCalculations()
    {
        Console.WriteLine("_____Calculator_____");
        Console.WriteLine("To exit, type 'exit'");
        while (true)
        {
            try
            {
                Console.WriteLine("Enter an expression: ");
                string expr = Console.ReadLine();
                if (expr == "exit")
                {
                    break;
                }
                var rvp = new Calculator(expr);
                rvp.PrintReversedPolishNotation();
         
                var result = rvp.Evaluate();
                Console.WriteLine($"Result is {result}");
            }
            catch (Exception ex) when (ex is ApplicationException)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(ex.Message);
                Console.ResetColor();
            }

        }
    }
}