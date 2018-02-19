using System.Collections.Generic;
using System.Collections;

namespace DealCore
{
    //==================================================================
    public class Deal
    {
        #region Fields
        //==================================================================
            private int dealID;
            private double amount;
            private string currencyCode;
        //==================================================================
        #endregion

        #region Properties
        //==================================================================
            public int DealID
            {
                get { return dealID; }
                set { dealID = value; }
            }
            //
            public double Amount
            {
                get { return amount; }
                set { amount = value; }
            }
            //
            public string CurrencyCode
            {
                get { return currencyCode; }
                set { currencyCode = value; }
            }
        //==================================================================
        #endregion
        //
        public Deal() {}
        //
        public Deal(int _dealID, double _amount,string _currencyCode)
        {
            dealID = _dealID;
            amount = _amount;
            currencyCode = _currencyCode;
        }
    }
    //==================================================================
    public class DealCollection : CollectionBase, IList<DealCore.Deal>
    {
        public int Add(Deal item)
        {
            return List.Add(item);
        }
        //
        public void Insert(int index, Deal item)
        {
            List.Insert(index, item);
        }
        //
        public void Remove(Deal item)
        {
            List.Remove(item);
        }
        //
        public bool Contains(Deal item)
        {
            return List.Contains(item);
        }
        //
        public int IndexOf(Deal item)
        {
            return List.IndexOf(item);
        }
        //
        public void CopyTo(Deal[] array, int index)
        {
            List.CopyTo(array, index);
        }
        //
        public Deal this[int index]
        {
            get { return (Deal)List[index]; }
            set { List[index] = value; }
        }

        #region ICollection<Deal> Members

        void ICollection<Deal>.Add(Deal item)
        {
            List.Add(item);
        }

        public bool IsReadOnly
        {
            get { return false; }
        }

        bool ICollection<Deal>.Remove(Deal item)
        {
            bool removed = false;
            if(InnerList.Contains(item))
            {
                InnerList.Remove(item);
                removed = true;
            }
            return removed;
        }

        #endregion

        #region IEnumerable<Deal> Members

        public new IEnumerator<Deal> GetEnumerator()
        {
            foreach (Deal deal in InnerList)
            {
                yield return deal;
            }
        }

        #endregion
    }
    //==================================================================
}
