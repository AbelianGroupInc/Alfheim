namespace Alfheim.FuzzyLogic
{
    public interface IFuzzyFunction : IFunction
    {
        #region Properties

        double MinInputValue { get; set; }

        double MaxInputValue { get; set; }

        double MinOutputValue { get; set; }

        double MaxOutputValue { get; set; }

        #endregion
    }
}
