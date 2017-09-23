using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alfheim.FuzzyLogic.Variables.Model
{
    public class LinguisticVariable
    {
        private IList<Term> terms;

        public string Name { get; set; }
        public double MinValue { get; set; }
        public double MaxValue { get; set; }
        public IEnumerable<Term> Terms {
            get {
                return terms;
            }
        }

        public LinguisticVariable(string name, double minValue, double maxValue)
        {
            Name = name;
            MinValue = minValue;
            MaxValue = maxValue;
            terms = new List<Term>();
        }

        public LinguisticVariable(string name, double minValue, double maxValue, IList<Term> terms)
        {
            Name = name;
            MinValue = minValue;
            MaxValue = maxValue;
            this.terms = terms;
        }

        public void AddTerm(Term term)
        {
            bool isExist = terms
                .Where(curTerm => curTerm.Name == term.Name)
                .Any();

            if (isExist)
                throw new TermNameAlreadyExistsException("Term name already exists;");

            terms.Add(term);
        }

        public void RemoveTerm(Term term)
        {
            terms.Remove(term);
        }

        public Term GetTerm(String name)
        {
            var termByName = terms.FirstOrDefault(term => term.Name == name);

            if (termByName == null)
                throw new TermNotFoundException("Term not found");

            return termByName;
        }
    }
}
