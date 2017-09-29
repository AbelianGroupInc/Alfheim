using System.Linq;

namespace Alfheim.FuzzyLogic.Variables.Model
{
    public class LinguisticVariable
    {
        // TODO : input/output
        private FuzzyLogicObservableCollection<Term> mTerms = new FuzzyLogicObservableCollection<Term>();
        private double minValue;
        private double maxValue;

        #region Properties

        public string Name { get; set; }
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

        #endregion

        #region Constructors

        public LinguisticVariable(string name, double minValue, double maxValue)
        {
            Name = name;
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
