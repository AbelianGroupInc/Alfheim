using Alfheim.FuzzyLogic.Variables.Services;
using System.Linq;

namespace Alfheim.FuzzyLogic.Variables.Model
{
    public class LinguisticVariable
    {
        private FuzzyLogicObservableCollection<Term> mTerms = new FuzzyLogicObservableCollection<Term>();
        private string name;
        private double minValue;
        private double maxValue;
        private LinguisticVariableType type;

        #region Properties

        public string Name {
            get
            {
                return name;
            }
            set
            {
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
                return type;
            }
            set
            {
                LinguisticVariableType initType = this.type;
                if(value == LinguisticVariableType.Undefined)
                {
                    this.type = value;
                    LinguisticVariableService.Instance.Remove(this);
                }
                else if (type != LinguisticVariableType.Undefined && type != value)
                {
                    LinguisticVariableService.Instance.Remove(this);
                    this.type = initType;
                    LinguisticVariableService.Instance.Add(this, value);
                    this.type = value;
                }
                else
                {
                    this.type = value;
                    LinguisticVariableService.Instance.Add(this, this.type);
                }
            }
        }

        #endregion

        #region Constructors

        public LinguisticVariable(string name, double minValue, double maxValue)
        {
            this.name = name;
            CheckDomainRestriction(minValue, maxValue);

            this.minValue = minValue;
            this.maxValue = maxValue;
            this.type = LinguisticVariableType.Undefined;

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

        #endregion
        #endregion
    }
}
