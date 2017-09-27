using Alfheim.FuzzyLogic.Rules.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alfheim.FuzzyLogic.Rules.Services
{
    public interface IRulesService
    {
        IEnumerable<Rule> LinguisticVariables { get; }
        void AddRule(Rule rule);
        void RemoveRule(Rule rule);
    }
}
