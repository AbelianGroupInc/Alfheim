using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alfheim.FuzzyLogic
{
    public class FuzzyProject
    {
        private static FuzzyProject instance;
         

        public static FuzzyProject Instance
        {
            get
            {
                if (instance == null)
                    instance = new FuzzyProject();
                return instance;
            }
        }

        public string Name { get; set; }
        public double? Version { get; set; } = null;
    }
}
