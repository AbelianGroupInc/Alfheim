using Alfheim.FuzzyLogic.Variables.Model;
using System.Collections.ObjectModel;

namespace Alfheim.FuzzyLogic.Variables.Services
{
    public interface ILinguisticVariableService
    {
        FuzzyLogicObservableCollection<LinguisticVariable> InputLinguisticVariables { get; }
        FuzzyLogicObservableCollection<LinguisticVariable> OutputLinguisticVariables { get; }

        LinguisticVariable GetLinguisticVariable(string name);
    }
}
