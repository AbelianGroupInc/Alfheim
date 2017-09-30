using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;

namespace Alfheim.FuzzyLogic.Variables.Model
{
    internal class LinguisticVariableDao
    {
        #region Members

        FuzzyLogicObservableCollection<LinguisticVariable> mInputLinguisticVariables =
            new FuzzyLogicObservableCollection<LinguisticVariable>();
        FuzzyLogicObservableCollection<LinguisticVariable> mOutputLinguisticVariables = 
            new FuzzyLogicObservableCollection<LinguisticVariable>();

        #endregion

        #region Properties

        public FuzzyLogicObservableCollection<LinguisticVariable> InputLinguisticVariables
        {
            get
            {
                return mInputLinguisticVariables;
            }
        }
        public FuzzyLogicObservableCollection<LinguisticVariable> OutputLinguisticVariables
        {
            get
            {
                return mOutputLinguisticVariables;
            }
        }

        #endregion

        public LinguisticVariableDao()
        {
            mInputLinguisticVariables.ItemAdding += OnItemAdding;
            mOutputLinguisticVariables.ItemAdding += OnItemAdding;
        }

        #region Public methods

        public LinguisticVariable GetLinguisticVariable(String name)
        {
            LinguisticVariable inputVariableByName = GetInputVariableByName(name);
            LinguisticVariable outputVariableByName = GetOutputVariableByName(name);

            if (inputVariableByName != null)
                return inputVariableByName;
            else if (outputVariableByName != null)
                return outputVariableByName;
            else
                throw new LinguisticVariableNotFoundException("Linguistic variable not found");
        }

        public bool IsNameExist(string name)
        {
            bool isInputVariableExist = (GetInputVariableByName(name) != null);
            bool isOutputVariableExist = (GetOutputVariableByName(name) != null);

            return isInputVariableExist || isOutputVariableExist;
        }

        public LinguisticVariable GetOutputVariableByName(string name)
        {
            return OutputLinguisticVariables
                .FirstOrDefault(variable => variable.Name == name);
        }

        public LinguisticVariable GetInputVariableByName(string name)
        {
            return InputLinguisticVariables
                .FirstOrDefault(variable => variable.Name == name);
        } 

        public void Remove(LinguisticVariable variable)
        {
            if (variable.Type == LinguisticVariableType.Input)
            {
                InputLinguisticVariables.Remove(variable);
            }
            else if (variable.Type == LinguisticVariableType.Output)
            {
                OutputLinguisticVariables.Remove(variable);
            }
        }
        #endregion

        #region Private methods


        #region Event handlers

        private void OnItemAdding(object sender, ItemAddingEventArgs e)
        {
            var item = (e.NewItem as LinguisticVariable);

            if (IsNameExist(item.Name))
                throw new LinguisticVariableNameAlreadyExistsException("Linguistic variable name already exists;");

            var collection = (sender as FuzzyLogicObservableCollection<LinguisticVariable>);

            if (collection == mInputLinguisticVariables && item.Type == LinguisticVariableType.Undefined)
                item.Type = LinguisticVariableType.Input;
            else if (collection == mOutputLinguisticVariables && item.Type == LinguisticVariableType.Undefined)
                item.Type = LinguisticVariableType.Output;
        }

        #endregion
        #endregion
    }
}
