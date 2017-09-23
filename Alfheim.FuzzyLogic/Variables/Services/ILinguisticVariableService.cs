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
        IEnumerable<LinguisticVariable> LinguisticVariables { get; }
         
        LinguisticVariable GetLinguisticVariable(string name);

        void AddLinguisticVariable(LinguisticVariable variable);
        void RemoveLinguisticVariable(LinguisticVariable variable);
    }
}
