using Task4;

namespace TestTask4;

[TestClass]
public class TestCalculator
{
    [TestMethod]
    public void TestInputExpressionIsValid()
    {
        var validExpressions = new[]
        {
            "2+2*3",
            "2 + (3 -4)",
            "    5    -    2 *     1/10",
            "2.3 - 1.0000000001 * 5",
            "((15 /(7-(1+1)))*37578.67)-(2+(1+1))",
            "2/0",
            "1+2*(3+2)",
            "2+15/3+4*2",
            };
        foreach (var expr in validExpressions)
        {
            var calculator = new Calculator(expr);
        }

        var invalidExpressions = new[]
        {
            "2 + 2a",
            "",
            "           ",
            "((((2-3)",
            "2,3 + 4",
            "2+)3-2(",
            "(4-3+1",
            "2+ 1 -(3+1",
            "4 - 3)"
        };
        foreach (var expr in invalidExpressions)
        {
            Assert.ThrowsException<ApplicationException>(() => new Calculator(expr));
        }
    }

    [TestMethod]
    public void TestEvaluate()
    {
        var expressionAndResult = new (string, decimal)[]
        {
            ("((3 + 7) * 2 - 5) / (4 + 1)", 3),
            ("12 / ((4 * 2) - 3) + 1", 3.4M),
            ("2 * ((6 + 4) - 2) + (8 / 2)", 20),
            ("(15 / (3 + 2)) + (4 * 2)", 11),
            ("7 - ((2 * 3) + 5) * 2", -15),
            ("(9 / (2 + 1)) * (4 + 3)", 21),
            ("((6 + 2) * 3 - 7) / 2", 8.5M),
            ("((3.5 + 7.2) * 2.1 - 5.3) / (4.6 + 1.8)", 2.6828125M),
            ("((6.6 + 2.9) * 3.4 - 7.1) / 2.5", 10.08M),
            ("5.4 * ((3.1 - 1.7) + 7.8) - 2.6", 47.08M),
        };
        foreach (var (expr, expectedResult) in expressionAndResult)
        {
            var calc = new Calculator(expr);
            var calculatedResult = calc.Evaluate();
            Assert.AreEqual(expectedResult, calculatedResult);
        }

        Assert.ThrowsException<DivideByZeroException>(() => new Calculator("2 / 0").Evaluate());

    }
}