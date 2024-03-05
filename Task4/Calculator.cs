using System.Globalization;
using System.Text.RegularExpressions;

namespace Task4;

public class Calculator
{
    private class CalcItem
    {
        public decimal? Number { get; private set; }
        private string? _operator;

        public string? Operator
        {
            get => _operator;
            private set
            {
                var allowedValues = new[] { "+", "-", "*", "/" };
                if (value != null && !allowedValues.Contains(value))
                {
                    throw new ApplicationException("Expression is not valid.");
                }

                _operator = value;
            }
        }

        public CalcItem(decimal? number = null, string? operatorStr = null)
        {
            Number = number;
            Operator = operatorStr;
        }

        public bool IsNumber()
        {
            return Number != null;
        }

        public decimal Calculate(decimal a, decimal b)
        {
            switch (Operator)
            {
                case "+":
                    return a + b;
                case "-":
                    return a - b;
                case "*":
                    return a * b;
                case "/":
                    return a / b;
                default:
                    throw new ApplicationException("This instance is not operator");
            }
        }

        public override string ToString()
        {
            if (IsNumber())
            {
                return Number.ToString() ?? string.Empty;
            }

            return Operator ?? string.Empty;
        }
    }


    private string Input { get; set; }
    private string[] Tokens { get; set; }
    private Queue<CalcItem> OutputPolishNotation { get; set; }


    public Calculator(string input)
    {
        Input = input;

        ValidateInput();
        DivideByTokens();
        ValidateParentheses();
        DoShuntingAlgorithm();
    }

    public void PrintReversedPolishNotation()
    {
        foreach (var item in OutputPolishNotation)
        {
            Console.Write(item + "  ");
        }
        Console.Write("\n");
    }


    public decimal Evaluate()
    {
        var calculationStack = new Stack<decimal>();
        foreach (var item in OutputPolishNotation)
        {
            if (item.IsNumber())
            {
                calculationStack.Push((decimal)item.Number);
            }
            else
            {
                if (calculationStack.Count < 2)
                {
                    throw new ApplicationException("Error in the expression");
                }

                var operandB = calculationStack.Pop();
                var operandA = calculationStack.Pop();

                var result = item.Calculate(operandA, operandB);
                calculationStack.Push(result);
            }
        }

        return calculationStack.Pop();
    }

    private void DoShuntingAlgorithm()
    {
        var culture = new CultureInfo("en-US", false);
        var style = NumberStyles.Number;
        OutputPolishNotation = new Queue<CalcItem>();
        var operations = new Stack<string>();

        foreach (var token in Tokens)
        {
            // If it's a number add it to queue
            bool isNumber = decimal.TryParse(token, style, culture, out var number);
            if (isNumber)
            {
                OutputPolishNotation.Enqueue(new CalcItem(number: number));
            }
            else if (token is "+" or "-" or "*" or "/")
            {
                if ((operations.Count == 0) || (token is "*" or "/"))
                {
                    operations.Push(token);
                }
                else
                {
                    //While there's an operator on the top of the stack with greater precedence:
                    while ((operations.Count > 0) && (operations.Peek() is "*" or "/"))
                    {
                        //Pop operators from the stack onto the output queue
                        OutputPolishNotation.Enqueue(new CalcItem(operatorStr: operations.Pop()));
                    }

                    operations.Push(token);
                }
            }
            else if (token is "(")
            {
                operations.Push(token);
            }
            else if (token is ")")
            {
                while ((operations.Count > 0) && (operations.Peek() != "("))
                {
                    OutputPolishNotation.Enqueue(new CalcItem(operatorStr: operations.Pop()));
                }

                // Pop the left bracket from the stack and discard it
                operations.Pop();
            }
        }

        while (operations.Count > 0)
        {
            OutputPolishNotation.Enqueue(new CalcItem(operatorStr: operations.Pop()));
        }
    }

    private void DivideByTokens()
    {
        var regexp = new Regex(@"(\()|(\))|([\d\.]+)|([\+\/\*-]+)");

        Tokens = regexp.Split(Input).Where(match => !string.IsNullOrWhiteSpace(match)).ToArray();
    }

    private void ValidateInput()
    {
        var invalidCharsRegex = new Regex(@"([^\d\(\)\s\.\+\-\/\*]+)");
        if (string.IsNullOrWhiteSpace(Input) || invalidCharsRegex.IsMatch(Input))
        {
            throw new ApplicationException("Given input is invalid!");
        }
    }

    private void ValidateParentheses()
    {
        var bracketStack = new Stack<string>();
        foreach (var token in Tokens)
        {
            switch (token)
            {
                case "(":
                    bracketStack.Push(token);
                    break;
                case ")" when bracketStack.Count == 0 || bracketStack.Pop() != "(":
                    throw new ApplicationException("Wrong order of parentheses!");
            }
        }
        
        if (bracketStack.Count != 0)
        {
            throw new ApplicationException("Wrong order of parentheses!");
        }
    }
    
    
}