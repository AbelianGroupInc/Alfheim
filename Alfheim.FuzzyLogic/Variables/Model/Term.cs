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

        /// <summary>
        /// The common usage : 
        /// Term term = new Term("term1", variable);
        /// term.SetFunction<TrapezoidalFunction>();
        /// </summary>
        /// <typeparam name="T"></typeparam>
        public void SetFunction<T> ()
            where T : IFuzzyFunction, new ()
        {
            this.FuzzyFunction = new T();

            FuzzyFunction.MaxInputValue = Variable.MaxValue;
            FuzzyFunction.MinInputValue = Variable.MinValue;
        }
    }
}
