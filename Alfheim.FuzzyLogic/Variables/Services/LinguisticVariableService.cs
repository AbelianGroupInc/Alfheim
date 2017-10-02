using Alfheim.FuzzyLogic.Variables.Model;
using System.Collections.ObjectModel;

namespace Alfheim.FuzzyLogic.Variables.Services
{
    public class LinguisticVariableService : ILinguisticVariableService
    {
        private LinguisticVariableDao linguisticVariableDao;

        private static LinguisticVariableService instance;

        public static LinguisticVariableService Instance
        {
            get
            {
                if (instance == null)
                    instance = new LinguisticVariableService();
                return instance;
            }
        }

        private LinguisticVariableService()
        {
            linguisticVariableDao = new LinguisticVariableDao();
        }

        public FuzzyLogicObservableCollection<LinguisticVariable> InputLinguisticVariables
        {
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

        public bool IsNameExist(string name)
        {
            return linguisticVariableDao.IsNameExist(name);
        }

        public LinguisticVariable GetInputVariableByName(string name)
        {
            return linguisticVariableDao.GetInputVariableByName(name);
        }

        public LinguisticVariable GetOutputVariableByName(string name)
        {
            return linguisticVariableDao.GetOutputVariableByName(name);
        }

        public void Remove(LinguisticVariable variable)
        {
            linguisticVariableDao.Remove(variable);
            if(variable.Type != LinguisticVariableType.Undefined)
                variable.Type = LinguisticVariableType.Undefined;
        }

        public void Add(LinguisticVariable variable, LinguisticVariableType type)
        {
            if (type == LinguisticVariableType.Input && variable.Type != LinguisticVariableType.Input)
                InputLinguisticVariables.Add(variable);
            else if (type == LinguisticVariableType.Output && variable.Type != LinguisticVariableType.Output)
                OutputLinguisticVariables.Add(variable);

        }
        
        public static void Clear()
        {
            instance = new LinguisticVariableService();
        }
    }
}
