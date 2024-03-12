namespace Task4;

public class CalcItem
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
                try
                {
                    return a / b;
                }
                catch (DivideByZeroException ex)
                {
                    throw new ApplicationException(ex.Message);
                }
                
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