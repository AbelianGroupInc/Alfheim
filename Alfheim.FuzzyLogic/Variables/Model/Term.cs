namespace Alfheim.FuzzyLogic.Variables.Model
{
    public class Term
    {

        private string name;
        public string Name {
            get
            {
                return name;
            }
            set
            {
                CheckDoesNameEmtpy(value);

                if (Variable != null)
                {
                    if (Variable.DoesTermNameExist(value))
                        throw new TermNameAlreadyExistsException("Term with name : " + value + " already exists");
                }

                this.name = value;
            }
        }

        public IFuzzyFunction FuzzyFunction { get; private set; }
        public LinguisticVariable Variable { get; set; }
        
        
        public Term(string name)
        {
            CheckDoesNameEmtpy(name);
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
        private void CheckDoesNameEmtpy(string value)
        {
            if (value.Equals(""))
                throw new NameIsEmptyException("Name can not be empty");
        }
    }
}
