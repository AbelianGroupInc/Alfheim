using Alfheim.FuzzyLogic.Rules.Model;
using Alfheim.FuzzyLogic.Rules.Services;
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

        public LinguisticVariableType GetLinguisticVariableType(LinguisticVariable variable)
        {
            return linguisticVariableDao.GetLinguisticVariableType(variable);
        }

        public void Remove(LinguisticVariable variable)
        {
            foreach(Rule rule in RulesService.Instance.Rules)
            {
                foreach(Term term in variable.Terms)
                {
                    if ((variable.Type == LinguisticVariableType.Input && rule.RuleConditions.Contains(term)) ||
                        (variable.Type == LinguisticVariableType.Output && rule.OutputTerm.Equals(term)))
                        throw new TermIsDefinedInRuleException(
                                "Term with name : " +
                                term.Name +
                                " cannot be moved until it is defined in rule: " +
                                rule.Stringify()
                            );
                }
            }

            linguisticVariableDao.Remove(variable);
        }

        public void Add(LinguisticVariable variable, LinguisticVariableType type)
        {
            if (type == LinguisticVariableType.Input)
                InputLinguisticVariables.Add(variable);
            else if (type == LinguisticVariableType.Output)
                OutputLinguisticVariables.Add(variable);

        }
        
        public static void Clear()
        {
            instance = new LinguisticVariableService();
        }
    }
}
