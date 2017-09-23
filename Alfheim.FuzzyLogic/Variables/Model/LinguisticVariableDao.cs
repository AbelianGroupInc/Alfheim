using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alfheim.FuzzyLogic.Variables.Model
{
    class LinguisticVariableDao
    {
        private IList<LinguisticVariable> linguisticVariables;
        public IEnumerable<LinguisticVariable> LinguisticVariables
        {
            get
            {
                return linguisticVariables;
            }
        }

        public LinguisticVariableDao()
        {
            this.linguisticVariables = new List<LinguisticVariable>();
        }

        public void AddLinguisticVariable(LinguisticVariable variable)
        {
            bool isExist = linguisticVariables
                .Where(curVariable => curVariable.Name == variable.Name)
                .Any();

            if (isExist)
                throw new LinguisticVariableNameAlreadyExistsException("Linguistic variable name already exists;");

            linguisticVariables.Add(variable);
            

        }
         
        public LinguisticVariable GetLinguisticVariable(String name)
        {
            var variableByName = linguisticVariables.FirstOrDefault(variable => variable.Name == name);

            if (variableByName == null)
                throw new LinguisticVariableNotFoundException("Linguistic variable not found");

            return variableByName;
        }

        public void RemoveLinguisticVariable(LinguisticVariable variable)
        {
            linguisticVariables.Remove(variable);
        }
    }
}
