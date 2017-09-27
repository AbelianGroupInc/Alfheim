using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alfheim.FuzzyLogic.Rules.Model
{
    class RulesDao
    {
        private List<Rule> rules;

        public IEnumerable<Rule> Rules
        {
            get
            {
                return rules;
            }
        }

        public void AddRule(Rule rule)
        {
            rules.Add(rule);
        } 

        public void RemoveRule(Rule rule)
        {
            rules.Remove(rule);
        }
    }
}
