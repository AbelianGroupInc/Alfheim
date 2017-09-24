using System.Linq;

namespace Alfheim.FuzzyLogic.Variables.Model
{
    public class LinguisticVariable
    {
        private FuzzyLogicObservableCollection<Term> mTerms = new FuzzyLogicObservableCollection<Term>();

        #region Properties

        public string Name { get; set; }
        public double MinValue { get; set; }
        public double MaxValue { get; set; }

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
            MinValue = minValue;
            MaxValue = maxValue;

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

        private bool IsTermNameExist(string name)
        {
            return mTerms
                .Where(term => term.Name == name)
                .Any();
        }

        #region Event handlers

        private void OnItemAdding(object sender, ItemAddingEventArgs e)
        {
            Term item = (e.NewItem as Term);

            if (IsTermNameExist(item.Name))
                throw new TermNameAlreadyExistsException("Term name already exists;");
        }

        #endregion
    }
}
