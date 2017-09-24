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
            mOutputLinguisticVariables.CollectionChanged += OnCollectionChanged;
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

        #endregion

        #region Private methods

        private bool IsNameExist(string name)
        {
            bool isInputVariableExist = (GetInputVariableByName(name) != null);
            bool isOutputVariableExist = (GetOutputVariableByName(name) != null);

            return isInputVariableExist || isOutputVariableExist;
        }

        private LinguisticVariable GetOutputVariableByName(string name)
        {
            return OutputLinguisticVariables
                .FirstOrDefault(variable => variable.Name == name);
        }

        private LinguisticVariable GetInputVariableByName(string name)
        {
            return InputLinguisticVariables
                .FirstOrDefault(variable => variable.Name == name);
        }

        #region Event handlers

        private void OnItemAdding(object sender, ItemAddingEventArgs e)
        {
            var item = (e.NewItem as LinguisticVariable);

            if (IsNameExist(item.Name))
                throw new LinguisticVariableNameAlreadyExistsException("Linguistic variable name already exists;");
        }

        private void OnAdded(FuzzyLogicObservableCollection<LinguisticVariable> collection, 
            IEnumerable<LinguisticVariable> newItems, IEnumerable<LinguisticVariable> oldItems)
        {

        }

        private void OnRemoved(FuzzyLogicObservableCollection<LinguisticVariable> collection,
            IEnumerable<LinguisticVariable> newItems, IEnumerable<LinguisticVariable> oldItems)
        {

        }

        private void OnReplaced(FuzzyLogicObservableCollection<LinguisticVariable> collection,
            IEnumerable<LinguisticVariable> newItems, IEnumerable<LinguisticVariable> oldItems)
        {

        }

        private void OnCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            var collection = (sender as FuzzyLogicObservableCollection<LinguisticVariable>);

            if (collection == null)
                return;

            switch (e.Action)
            {
                case NotifyCollectionChangedAction.Add:
                    OnAdded(collection, 
                        e.NewItems.Cast<LinguisticVariable>(),
                        e.OldItems.Cast<LinguisticVariable>());
                    break;
                case NotifyCollectionChangedAction.Remove:
                    OnRemoved(collection,
                        e.NewItems.Cast<LinguisticVariable>(),
                        e.OldItems.Cast<LinguisticVariable>());
                    break;

                case NotifyCollectionChangedAction.Replace:
                    OnReplaced(collection,
                        e.NewItems.Cast<LinguisticVariable>(),
                        e.OldItems.Cast<LinguisticVariable>());
                    break;
            }
        }

        #endregion
        #endregion
    }
}
