using System.Globalization;
using System.Text.RegularExpressions;

namespace Task4;

public class ReversePolishNotation
{
    public class Shunt
    {
        private decimal? _number;
        private string? _operator;

        public Shunt(decimal? number=null, string? operatorStr = null)
        {
            _number = number;
            _operator = operatorStr;
        }

        public override string ToString()
        {
            if (_number != null)
            {
                return _number.ToString();
            }
            if (_operator != null)
            {
                return _operator;
            }

            return "";
        }

    }
    
    
    private string _input { get; set; }
    
    private string[] _tokens { get; set; }
    private Queue<Shunt> _output { get; set; }
    private Stack<string> _operations { get; set; }
    
    
    
    public ReversePolishNotation(string input)
    {
        _input = input;
        
        DivideByTokens();   
        DoShuntingAlgorithm();
    }

    public void PrintReversedPolishNotation()
    {
        foreach (var sh in _output)
        {
            Console.Write(sh + "  ");
        }
        Console.Write("\n");
    }

    private void DoShuntingAlgorithm()
    {
        var culture = new CultureInfo("en-US", false);
        var style = NumberStyles.Number;
        _output = new Queue<Shunt>();
        _operations = new Stack<string>();

        foreach (var token in _tokens)
        {
            // If it's a number add it to queue
            bool isNumber = decimal.TryParse(token, style, culture, out var number);
            if (isNumber)
            {
                _output.Enqueue(new Shunt(number:number));
            }
            else if (token is "+" or "-" or "*" or "/")
            {
                if ((_operations.Count == 0) || (token is "*" or "/"))
                {
                    _operations.Push(token);
                }
                else
                {
                    //While there's an operator on the top of the stack with greater precedence:
                    while ((_operations.Count > 0) && (_operations.Peek() is "*" or "/" ))
                    {
                        //Pop operators from the stack onto the output queue
                        _output.Enqueue(new Shunt(operatorStr:_operations.Pop()));
                    }
                    _operations.Push(token);
                }
            }
            else if (token is "(")
            {
                _operations.Push(token);
            }
            else if (token is ")")
            {
                while (_operations.Peek() != "(")
                {
                    _output.Enqueue(new Shunt(operatorStr:_operations.Pop()));
                }
                // Pop the left bracket from the stack and discard it
                _operations.Pop();
            }
            
        }

        while (_operations.Count > 0)
        {
            _output.Enqueue(new Shunt(operatorStr:_operations.Pop()));
        }
    }

    private void DivideByTokens()
    {
        var regexp = new Regex(@"(\()|(\))|([\d\.]+)|([\+\/\*-]+)");

        _tokens = regexp.Split(_input).Where(match => !string.IsNullOrWhiteSpace(match)).ToArray();
    }


    private void validateInput()
    {
        validateParenthesis();
        validateChars();
    }

    private void validateParenthesis()
    {
        throw new NotImplementedException();
    }

    private void validateChars()
    {
        throw new NotImplementedException();
    }
}