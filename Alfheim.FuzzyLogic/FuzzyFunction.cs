using System;

namespace Alfheim.FuzzyLogic
{
    public abstract class FuzzyFunction : IFuzzyFunction
    {

        #region Constructors

        public FuzzyFunction() : this(FuzzyFunctionConstants.DefaultMinInputValue,
                FuzzyFunctionConstants.DefaultMaxInputValue) { }

        public FuzzyFunction(double maxInputValue) :
            this(FuzzyFunctionConstants.DefaultMinInputValue, maxInputValue) { }

        public FuzzyFunction(double minInputValue, double maxInputValue) : this(minInputValue, maxInputValue,
            FuzzyFunctionConstants.DefaultMinOutputValue, FuzzyFunctionConstants.DefaultMaxOutputValue) { }

        public FuzzyFunction(double minInputValue, double maxInputValue, 
            double minOutputValue, double maxOutputValue)
        {
            MinInputValue = minInputValue;
            MaxInputValue = maxInputValue;

            MinOutputValue = minOutputValue;
            MaxOutputValue = maxOutputValue;
        }

        #endregion

        #region Properties

        public double MinInputValue { get; set; }

        public double MaxInputValue { get; set; }

        public double MinOutputValue { get; set; }

        public double MaxOutputValue { get; set; }

        #endregion

        #region Public methods
        public virtual double GetValue(double x)
        {
            if (x < MinInputValue || x > MaxInputValue)
                throw new ArgumentException(FuzzyFunctionStringConstants.InputError);

            return Normalize(Function(x));
        }

        #endregion

        #region Protected methods

        protected abstract double Function(double x);

        protected virtual double Normalize(double y)
        {
            if (y < MinOutputValue)
                y = MinOutputValue;

            if (y > MaxOutputValue)
                y = MaxOutputValue;

            return y;
        }

        #endregion  
    }
}
