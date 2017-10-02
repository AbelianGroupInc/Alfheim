﻿using Alfheim.FuzzyLogic.Variables.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alfheim.FuzzyLogic.Rules.Model
{
    public class RulesDao
    {
        private FuzzyLogicObservableCollection<Rule> rules;

        public FuzzyLogicObservableCollection<Rule> Rules
        {
            get
            {
                return rules;
            }
        }

        public RulesDao()
        {
            rules = new FuzzyLogicObservableCollection<Rule>();
            rules.ItemAdding += OnItemAdding;
        }
         

        private void OnItemAdding(object sender, ItemAddingEventArgs e)
        {
            var item = (e.NewItem as LinguisticVariable);
        }
    }
}
