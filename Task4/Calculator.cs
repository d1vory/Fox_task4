using System.Globalization;
using System.Text.RegularExpressions;

namespace Task4;

public class Calculator
{
    public readonly string Input;
    private string[] _tokens;
    private Queue<CalcItem> _outputPolishNotation;


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
        foreach (var item in _outputPolishNotation)
        {
            Console.Write(item + "  ");
        }
        Console.Write("\n");
    }


    public decimal Evaluate()
    {
        var calculationStack = new Stack<decimal>();
        foreach (var item in _outputPolishNotation)
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
        _outputPolishNotation = new Queue<CalcItem>();
        var operations = new Stack<string>();

        foreach (var token in _tokens)
        {
            // If it's a number add it to queue
            bool isNumber = decimal.TryParse(token, style, culture, out var number);
            if (isNumber)
            {
                _outputPolishNotation.Enqueue(new CalcItem(number: number));
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
                        _outputPolishNotation.Enqueue(new CalcItem(operatorStr: operations.Pop()));
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
                    _outputPolishNotation.Enqueue(new CalcItem(operatorStr: operations.Pop()));
                }

                // Pop the left bracket from the stack and discard it
                operations.Pop();
            }
        }

        while (operations.Count > 0)
        {
            _outputPolishNotation.Enqueue(new CalcItem(operatorStr: operations.Pop()));
        }
    }

    private void DivideByTokens()
    {
        var regexp = new Regex(@"(\()|(\))|([\d\.]+)|([\+\/\*-]+)");

        _tokens = regexp.Split(Input).Where(match => !string.IsNullOrWhiteSpace(match)).ToArray();
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
        foreach (var token in _tokens)
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