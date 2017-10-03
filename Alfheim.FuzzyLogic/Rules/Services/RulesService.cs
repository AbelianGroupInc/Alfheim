using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Alfheim.FuzzyLogic.Rules.Model;

namespace Alfheim.FuzzyLogic.Rules.Services
{
    public class RulesService : IRulesService
    {
        RulesDao rulesDao;

        public IEnumerable<Rule> LinguisticVariables
        {
            get
            {
                return rulesDao.Rules;
            }
        }

        public RulesService()
        {
            rulesDao = new RulesDao();
        }

        public void AddRule(Rule rule)
        {
            rulesDao.Add(rule);
        }

        public void RemoveRule(Rule rule)
        {
            rulesDao.Remove(rule);
        }
    }
}
