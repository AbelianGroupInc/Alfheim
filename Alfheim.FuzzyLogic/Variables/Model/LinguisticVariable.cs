using Alfheim.FuzzyLogic.Variables.Services;
using System.Linq;

namespace Alfheim.FuzzyLogic.Variables.Model
{
    public class LinguisticVariable
    {
        // TODO : input/output
        private FuzzyLogicObservableCollection<Term> mTerms = new FuzzyLogicObservableCollection<Term>();
        private string name;
        private double minValue;
        private double maxValue;
        

        public string Name {
            get
            {
                return name;
            }
            set
            {
                CheckDoesNameEmtpy(value);

                if (LinguisticVariableService.Instance.IsNameExist(value))
                    throw new LinguisticVariableNameAlreadyExistsException("Variable with name : " + value + " already exists");

                this.name = value;
            }
        }

        public double MinValue {
            get
            {
                return minValue;
            }
            set
            {
                CheckDomainRestriction(value, MaxValue);

                minValue = value;
            }
        }
        public double MaxValue {
            get
            {
                return maxValue;
            }
            set
            {
                CheckDomainRestriction(MinValue, value);
                maxValue = value;
            }
        }

        public FuzzyLogicObservableCollection<Term> Terms
        {
            get
            {
                return mTerms;
            }
        }

        public LinguisticVariableType Type {
            get
            {
                return LinguisticVariableService.Instance.GetLinguisticVariableType(this);
            }
            set
            {
                LinguisticVariableType currentVarType = this.Type;

                if (currentVarType == LinguisticVariableType.Undefined)
                    LinguisticVariableService.Instance.Add(this, value);
                else if (value != currentVarType)
                {
                    LinguisticVariableService.Instance.Remove(this);
                    LinguisticVariableService.Instance.Add(this, value);
                    
                }
                else if(value == LinguisticVariableType.Undefined)
                {
                    LinguisticVariableService.Instance.Remove(this);
                }
            }
        }
        

        #region Constructors

        public LinguisticVariable(string name, double minValue, double maxValue)
        {
            this.name = name;
            CheckDoesNameEmtpy(name);
            CheckDomainRestriction(minValue, maxValue);

            this.minValue = minValue;
            this.maxValue = maxValue;

            mTerms.ItemAdding += OnItemAdding; ;
        }


        #endregion

        public Term GetTermByName(string name)
        {
            var termByName = mTerms.FirstOrDefault(term => term.Name == name);

            if (termByName == null)
                throw new TermNotFoundException("Term not found");

            return termByName;
        }


        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;

            if (obj == this)
                return true;

            LinguisticVariable variable = obj as LinguisticVariable;
            if ((System.Object) variable == null)
            {
                return false;
            }

            
            return this.MaxValue == variable.MaxValue &&
                this.MinValue == variable.MinValue &&
                this.Name.Equals(variable.Name) &&
                this.Terms.SequenceEqual(variable.Terms);
        }
        #region Private methods
        private void CheckDomainRestriction(double minValue, double maxValue)
        {
            if (minValue > maxValue)
            {
                throw new LinguisticVariableDomainRestrictionException("Max value of a function is less then min value");
            }
        }

        public bool DoesTermNameExist(string name)
        {
            return mTerms
                .Where(term => term.Name == name)
                .Any();
        }

        #region Event handlers

        private void OnItemAdding(object sender, ItemAddingEventArgs e)
        {
            Term item = (e.NewItem as Term);

            if (DoesTermNameExist(item.Name))
                throw new TermNameAlreadyExistsException("Term name already exists;");

            item.Variable = this;
        }
        private static void CheckDoesNameEmtpy(string value)
        {
            if (value.Equals(""))
                throw new NameIsEmptyException("Name can not be empty");
        }

        #endregion
        #endregion
    }
}
