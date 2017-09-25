namespace Alfheim.FuzzyLogic.Variables.Model
{
    public class Term
    {
        public string Name { get; set; }
        public IFuzzyFunction FuzzyFunction { get; private set; }
        public LinguisticVariable Variable { get; set; }
        
        
        public Term(string name)
        {
            Name = name;
        }

        public void SetFunction(IFuzzyFunction fuzzyFunction)
        {
            if (Variable == null)
                throw new LinguisticVariableIsNotSpecifiedException("Linguistic variable was not specified for term");
            
            fuzzyFunction.MaxInputValue = Variable.MaxValue;
            fuzzyFunction.MinInputValue = Variable.MinValue;

            this.FuzzyFunction = fuzzyFunction;
        }
    }
}
