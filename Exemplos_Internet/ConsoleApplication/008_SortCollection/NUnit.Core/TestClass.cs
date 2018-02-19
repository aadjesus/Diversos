using System;
using NUnit.Framework;
using DealCore;

namespace NUnit.Core
{
    [TestFixture]
    public class TestClass
    {
    #region Setup
            //setup deals
            Deal dealA = new Deal(1001, 12450, "USD");
            Deal dealB = new Deal(1002, 1180, "GPB");
            Deal dealC = new Deal(1003, 15000, "AUD");
            //create a new deal for MinMax method
            Deal dealD = new Deal();
            //setup sorted collections
            DealCollection sortedCollA = new DealCollection();
            DealCollection sortedCollB = new DealCollection();
    #endregion
    //===========================================================================================
    #region Tests
            [Test]
            public void NonSortedCollectionTest()
            {
                //setup colections
                DealCollection dealCollectionA = new DealCollection();
                DealCollection dealCollectionB = new DealCollection();
                //
                dealCollectionA.Add(dealB);
                dealCollectionA.Add(dealC);
                dealCollectionA.Add(dealA);
                //
                dealCollectionB.Add(dealC);
                dealCollectionB.Add(dealA);
                dealCollectionB.Add(dealB);
                Console.WriteLine("Non Sorted Collection Test");
                //print on console items of first collection
                foreach (Deal itemA in dealCollectionA)
                {
                    Console.WriteLine("--- First collection ---");
                    Console.WriteLine("DealID = " + itemA.DealID);
                    Console.WriteLine("Amount = " + itemA.Amount);
                    Console.WriteLine("Currency = " + itemA.CurrencyCode);
                }
                //print on console items of second collection
                foreach (Deal itemB in dealCollectionB)
                {
                    Console.WriteLine("--- Second collection ---");
                    Console.WriteLine("DealID = " + itemB.DealID);
                    Console.WriteLine("Amount = " + itemB.Amount);
                    Console.WriteLine("Currency = " + itemB.CurrencyCode);
                }
                Console.WriteLine("");
                //first compare collections without sorting them
                NUnit.Framework.Assert.AreEqual(dealCollectionA, dealCollectionB);
            }
            //
            [Test]
            public void SortedCollectionTest()
            {
                //setup colections
                DealCollection dealCollectionA = new DealCollection();
                DealCollection dealCollectionB = new DealCollection();
                //
                dealCollectionA.Add(dealB);
                dealCollectionA.Add(dealC);
                dealCollectionA.Add(dealA);
                //
                dealCollectionB.Add(dealC);
                dealCollectionB.Add(dealA);
                dealCollectionB.Add(dealB);
                //sort collections based on Amount ascending
                sortedCollA = dealCollectionA.SortAscending<Deal, double, DealCollection>(deal => deal.Amount);
                sortedCollB = dealCollectionB.SortAscending<Deal, double, DealCollection>(deal => deal.Amount);
                Console.WriteLine("Sorted Collection Test");
                //print on console items of first collection
                foreach (Deal itemA in sortedCollA)
                {
                    Console.WriteLine("--- First collection ---");
                    Console.WriteLine("DealID = " + itemA.DealID);
                    Console.WriteLine("Amount = " + itemA.Amount);
                    Console.WriteLine("Currency = " + itemA.CurrencyCode);
                }
                //print on console items of second collection
                foreach (Deal itemB in sortedCollB)
                {
                    Console.WriteLine("--- Second collection ---");
                    Console.WriteLine("DealID = " + itemB.DealID);
                    Console.WriteLine("Amount = " + itemB.Amount);
                    Console.WriteLine("Currency = " + itemB.CurrencyCode);
                }
                Console.WriteLine("");
                //use NUnit Assert to determine if the collections are identical
                NUnit.Framework.Assert.AreEqual(sortedCollA, sortedCollB);
            }
            //
            [Test]
            public void MaxItemTest()
            {
                //setup colection
                DealCollection dealCollectionA = new DealCollection();
                //
                dealCollectionA.Add(dealB);
                dealCollectionA.Add(dealC);
                dealCollectionA.Add(dealA);
                //the standard Max method retuns in our case a double which is the maximum Amount in our collection
                //double amount = dealCollectionA.Max(deal => deal.Amount);
                //MaxItem returns the object which has the maximum Amount in our collection
                dealD = dealCollectionA.MaxItem(deal => deal.Amount);
                Console.WriteLine("MaxItemTest");
                //Write to the console the amount value
                //Console.WriteLine("Amount = " + amount);
                //write to console deal properties
                Console.WriteLine("DealID = " + dealD.DealID);
                Console.WriteLine("Amount = " + dealD.Amount);
                Console.WriteLine("Currency = " + dealD.CurrencyCode);
                Console.WriteLine("");
            }
    #endregion
    }
}
