using Alfheim.FuzzyLogic.Variables.Model;
using System.Collections.ObjectModel;

namespace Alfheim.FuzzyLogic.Variables.Services
{
    public class LinguisticVariableService : ILinguisticVariableService
    {
        private LinguisticVariableDao linguisticVariableDao;

        public LinguisticVariableService()
        {
            linguisticVariableDao = new LinguisticVariableDao();
        }

        public FuzzyLogicObservableCollection<LinguisticVariable> InputLinguisticVariables {
            get
            {
                return linguisticVariableDao.InputLinguisticVariables;
            }
        }
        public FuzzyLogicObservableCollection<LinguisticVariable> OutputLinguisticVariables
        {
            get
            {
                return linguisticVariableDao.OutputLinguisticVariables;
            }
        }

        public LinguisticVariable GetLinguisticVariable(string name)
        {
            return linguisticVariableDao.GetLinguisticVariable(name);
        }
    }
}
