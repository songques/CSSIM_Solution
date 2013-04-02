using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using System.ComponentModel;

namespace CSS.IM.UI.Control.Graphics.ListBoxEx
{

    [ListBindable(false)]
    public class ListBoxExItemCollection 
        : IList,ICollection,IEnumerable
    {
        #region Fields

        private ListBoxEx _owner;

        #endregion

        #region Constructors

        public ListBoxExItemCollection(ListBoxEx owner)
        {
            _owner = owner;
        }

        #endregion

        #region Properties

        internal ListBoxEx Owner
        {
            get { return _owner; }
        }

        public ListBoxExItem this[int index]
        {
            get { return Owner.OldItems[index] as ListBoxExItem; }
            set { Owner.OldItems[index] = value; }
        }

        public int Count
        {
            get { return Owner.OldItems.Count; }
        }

        public bool IsReadOnly 
        {
            get { return Owner.OldItems.IsReadOnly; }
        }

        #endregion

        #region Public Methods

        public int Add(ListBoxExItem item)
        {
            if (item == null)
            {
                throw new ArgumentNullException("item");
            }
            return Owner.OldItems.Add(item);
        }

        public void AddRange(ListBoxExItemCollection value)
        {
            foreach (ListBoxExItem item in value)
            {
                Add(item);
            }
        }

        public void AddRange(ListBoxExItem[] items)
        {
            Owner.OldItems.AddRange(items);
        }

        public void Clear()
        {
            Owner.OldItems.Clear();
        }

        public bool Contains(ListBoxExItem item)
        {
            return Owner.OldItems.Contains(item);
        }

        public void CopyTo(
            ListBoxExItem[] destination, 
            int arrayIndex)
        {
            Owner.OldItems.CopyTo(destination, arrayIndex);
        }

        public int IndexOf(ListBoxExItem item)
        {
            return Owner.OldItems.IndexOf(item);
        }

        public void Insert(int index, ListBoxExItem item)
        {
            if (item == null)
            {
                throw new ArgumentNullException("item");
            }
            Owner.OldItems.Insert(index, item);
        }

        public void Remove(ListBoxExItem item)
        {
            Owner.OldItems.Remove(item);
        }

        public void RemoveAt(int index)
        {
            Owner.OldItems.RemoveAt(index);
        }

        public IEnumerator GetEnumerator()
        {
            return Owner.OldItems.GetEnumerator();
        }

        #endregion

        #region IList 成员

        int IList.Add(object value)
        {
            if (!(value is ListBoxExItem))
            {
                throw new ArgumentException();
            }
            return Add(value as ListBoxExItem);
        }

        void IList.Clear()
        {
            Clear();
        }

        bool IList.Contains(object value)
        {
            return Contains(value as ListBoxExItem);
        }

        int IList.IndexOf(object value)
        {
            return IndexOf(value as ListBoxExItem);
        }

        void IList.Insert(int index, object value)
        {
            if (!(value is ListBoxExItem))
            {
                throw new ArgumentException();
            }
            Insert(index, value as ListBoxExItem);
        }

        bool IList.IsFixedSize
        {
            get { return false; }
        }

        bool IList.IsReadOnly
        {
            get { return IsReadOnly; }
        }

        void IList.Remove(object value)
        {
            Remove(value as ListBoxExItem);
        }

        void IList.RemoveAt(int index)
        {
            RemoveAt(index);
        }

        object IList.this[int index]
        {
            get
            {
                return this[index];
            }
            set
            {
                if (!(value is ListBoxExItem))
                {
                    throw new ArgumentException();
                }
                this[index] = value as ListBoxExItem;
            }
        }

        #endregion

        #region ICollection 成员

        void ICollection.CopyTo(Array array, int index)
        {
            CopyTo((ListBoxExItem[])array, index);
        }

        int ICollection.Count
        {
            get { return Count; }
        }

        bool ICollection.IsSynchronized
        {
            get { return false; }
        }

        object ICollection.SyncRoot
        {
            get { return this; }
        }

        #endregion

        #region IEnumerable 成员

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        #endregion
    }
}
