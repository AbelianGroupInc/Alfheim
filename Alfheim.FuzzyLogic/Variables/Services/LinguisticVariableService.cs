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

        public IEnumerable<LinguisticVariable> LinguisticVariables {
            get
            {
                return linguisticVariableDao.LinguisticVariables;
            }
        }

        public void AddLinguisticVariable(LinguisticVariable variable)
        {
            linguisticVariableDao.AddLinguisticVariable(variable);
        }

        public LinguisticVariable GetLinguisticVariable(string name)
        {
            return linguisticVariableDao.GetLinguisticVariable(name);
        }

        public void RemoveLinguisticVariable(LinguisticVariable variable)
        {
            linguisticVariableDao.RemoveLinguisticVariable(variable);
        }
    }
}
