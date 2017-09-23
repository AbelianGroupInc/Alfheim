using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Alfheim.FuzzyLogic.Variables.Model;

namespace Alfheim.FuzzyLogic.Variables.Services
{
    public class LinguisticVariableService : ILinguisticVariableService
    {
        private LinguisticVariableDao linguisticVariableDao;

        public LinguisticVariableService()
        {
            linguisticVariableDao = new LinguisticVariableDao();
        }

        public IEnumerable<LinguisticVariable> InputLinguisticVariables {
            get
            {
                return linguisticVariableDao.InputLinguisticVariables;
            }
        }
        public IEnumerable<LinguisticVariable> OutputLinguisticVariables
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

        public void RemoveLinguisticVariable(LinguisticVariable variable, LinguisticVariableType type)
        {
            linguisticVariableDao.RemoveLinguisticVariable(variable, type);
        }

        public void AddLinguisticVariable(LinguisticVariable variable, LinguisticVariableType type)
        {
            linguisticVariableDao.AddLinguisticVariable(variable, type);
        }
    }
}
