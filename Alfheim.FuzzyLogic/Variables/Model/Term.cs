namespace Alfheim.FuzzyLogic.Variables.Model
{
    public class Term
    {
        public string Name { get; set; }
        public IFuzzyFunction FuzzyFunction { get; private set; }
        public LinguisticVariable Variable { get; set; }
        
        public Term(string name, LinguisticVariable variable)
        {
            Name = name;
            Variable = variable;
        }

        public Term(string name)
        {
            Name = name;
        }

        public void SetFunction(IFuzzyFunction fuzzyFunction)
        {
            fuzzyFunction.MaxInputValue = Variable.MaxValue;
            fuzzyFunction.MinInputValue = Variable.MinValue;
        }
    }
}
