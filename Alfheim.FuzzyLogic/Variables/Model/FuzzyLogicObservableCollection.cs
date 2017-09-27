using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;

namespace Alfheim.FuzzyLogic.Variables.Model
{
    public class FuzzyLogicObservableCollection<T> : Collection<T>, INotifyCollectionChanged, INotifyPropertyChanged
    {
        #region Constructors

        public FuzzyLogicObservableCollection(Collection<T> list) : base(list) { }

        public FuzzyLogicObservableCollection() : base() { }

        #endregion

        #region Evnets

        public event NotifyCollectionChangedEventHandler CollectionChanged;
        public event PropertyChangedEventHandler PropertyChanged;
        public event ItemAddingEventHandler ItemAdding;

        #endregion

        public new void Add(T item)
        {
            ItemAdding(this, new ItemAddingEventArgs(item));

            base.Add(item);
            NotifyChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Add, item, base.Count - 1));
            OnPropertyChanged("Count");
        }

        public new void Remove(T item)
        {
            var index = base.Items.IndexOf(item);
            base.Remove(item);

            NotifyChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Remove, item, index));
            OnPropertyChanged("Count");
        }

        #region Protected methods

        protected void NotifyChanged(NotifyCollectionChangedEventArgs args)
        {
            CollectionChanged?.Invoke(this, args);
        }

        protected void OnPropertyChanged(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        #endregion
    }
}