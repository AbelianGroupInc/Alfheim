using Alfheim.FuzzyLogic.Variables.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alfheim.FuzzyLogic.Variables.Services
{
    public interface ILinguisticVariableService
    {
        IEnumerable<LinguisticVariable> InputLinguisticVariables { get; }
        IEnumerable<LinguisticVariable> OutputLinguisticVariables { get; }

        LinguisticVariable GetLinguisticVariable(string name);

        void AddLinguisticVariable(LinguisticVariable variable, LinguisticVariableType type);
        void RemoveLinguisticVariable(LinguisticVariable variable, LinguisticVariableType type); 
    }
}
