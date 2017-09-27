using System;

namespace Alfheim.FuzzyLogic.Variables.Model
{
    public class ItemAddingEventArgs : EventArgs
    {
        public ItemAddingEventArgs(object newItem)
        {
            NewItem = newItem;
        }

        public object NewItem { get; }
    }
}
