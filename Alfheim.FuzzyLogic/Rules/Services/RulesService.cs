using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Alfheim.FuzzyLogic.Rules.Model;

namespace Alfheim.FuzzyLogic.Rules.Services
{
    public class RulesService
    {
        RulesDao rulesDao;

        private static RulesService instance;

        public IEnumerable<Rule> Rules
        {
            get
            {
                return rulesDao.Rules;
            }
        }

        public static RulesService Instance
        {
            get
            {
                if (instance == null)
                    instance = new RulesService();
                return instance;
            }
        }

        private RulesService()
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

        public static void Clear()
        {
            instance = new RulesService(); 
        }
    }
}
