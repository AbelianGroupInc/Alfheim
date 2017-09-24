namespace Alfheim.FuzzyLogic.Variables.Model
{
    public class Term
    {
        public string Name { get; set; }
        public IFuzzyFunction FuzzyFunction { get; private set; }
        public LinguisticVariable Variable { get;}
        
        public Term(string name, LinguisticVariable variable)
        {
            Name = name;
            Variable = variable;
        }
         
        public void SetFunction(IFuzzyFunction fuzzyFunction)
        {
            fuzzyFunction.MaxInputValue = Variable.MaxValue;
            fuzzyFunction.MinInputValue = Variable.MinValue;
        }
    }
}
