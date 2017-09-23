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
        Dictionary<LinguisticVariableType, IList<LinguisticVariable>> linguisticVariables;
        public IEnumerable<LinguisticVariable> InputLinguisticVariables
        {
            get
            {
                return linguisticVariables[LinguisticVariableType.Input];
            }
        }
        public IEnumerable<LinguisticVariable> OutputLinguisticVariables
        {
            get
            {
                return linguisticVariables[LinguisticVariableType.Output];
            }
        }

        public object outputLinguisticVariables { get; private set; }

        public LinguisticVariableDao()
        {
            linguisticVariables = new Dictionary<LinguisticVariableType, IList<LinguisticVariable>>();

            this.linguisticVariables[LinguisticVariableType.Input] = new List<LinguisticVariable>();
            this.linguisticVariables[LinguisticVariableType.Output] = new List<LinguisticVariable>();
        }
        public LinguisticVariable GetLinguisticVariable(String name)
        {
            LinguisticVariable inputVariableByName = InputVariableByName(name);
            LinguisticVariable outputVariableByName = OutputVariableByName(name);

            if (inputVariableByName != null)
                return inputVariableByName;
            else if (outputVariableByName != null)
                return outputVariableByName;
            else
                throw new LinguisticVariableNotFoundException("Linguistic variable not found");
        }

        private LinguisticVariable OutputVariableByName(string name)
        {
            return OutputLinguisticVariables
                            .FirstOrDefault(variable => variable.Name == name);
        }

        private LinguisticVariable InputVariableByName(string name)
        {
            return InputLinguisticVariables
                            .FirstOrDefault(variable => variable.Name == name);
        }

        public void AddLinguisticVariable(LinguisticVariable variable, LinguisticVariableType type)
        {
            IList<LinguisticVariable> variables = linguisticVariables[type];

            LinguisticVariable inputVariableByName = InputVariableByName(variable.Name);
            LinguisticVariable outputVariableByName = OutputVariableByName(variable.Name);

            if (inputVariableByName != null || outputVariableByName != null)
                throw new LinguisticVariableNameAlreadyExistsException("Linguistic variable name already exists;");

            variables.Add(variable);
        }

        public void RemoveLinguisticVariable(LinguisticVariable variable, LinguisticVariableType type)
        {
            IList<LinguisticVariable> variables = linguisticVariables[type];
            variables.Remove(variable);
        }
    }
}
