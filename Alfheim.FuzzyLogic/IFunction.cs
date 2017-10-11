using Alfheim.FuzzyLogic.Functions;

namespace Alfheim.FuzzyLogic
{
    public interface IFunction
    {
        FuzzyFunctionType Type { get; }
        double GetValue(double x);
    }
}
