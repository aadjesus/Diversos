/*
The MIT License

Copyright (c) 2008 Kevin Moore (http://j832.com)

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in
all copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
THE SOFTWARE.
*/

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Windows.Input;
using J832.Common;

namespace J832.Wpf
{
    /// <remarks>
    ///     The members of this class are only safe to use via one thread (usually the UI thread in a WPF application).
    ///     Any cross-thread changes will cause undesired behavior.
    /// </remarks>
    public class DemoCollection<T> : ReadOnlyObservableCollection<T>
    {
        public static DemoCollection<T> Create(IEnumerable<T> source, int initialCount, int minCount, int maxCount)
        {
            Util.RequireNotNull(source, "source");
            Util.RequireArgumentRange(initialCount >= 0, "initialCount");
            Util.RequireArgumentRange(minCount >= 0, "minCount");
            Util.RequireArgumentRange(minCount <= initialCount, "minCount");
            Util.RequireArgumentRange(initialCount <= maxCount, "maxCount");

            ReadOnlyCollection<T> sourceItems = source.ToReadOnlyCollection();

            Util.RequireArgumentRange(sourceItems.Count > 0, "source", "source must have at least 1 item");

            ObservableCollection<T> observableCollection = new ObservableCollection<T>();

            for (int i = 0; i < initialCount; i++)
            {
                observableCollection.Add(sourceItems[i % sourceItems.Count]);
            }

            return new DemoCollection<T>(sourceItems, observableCollection, minCount, maxCount, initialCount);
        }

        public void Add()
        {
            if (canAdd())
            {
                int sourceIndex = m_random.Next(m_sourceItems.Count);
                int targetIndex = m_random.Next(m_activeItems.Count + 1);

                m_activeItems.Add(m_sourceItems[sourceIndex]);
            }
        }

        public void Remove()
        {
            if (canRemove())
            {
                int targetIndex = m_random.Next(m_activeItems.Count);

                m_activeItems.RemoveAt(targetIndex);
            }
        }

        public void Move()
        {
            if (canMove())
            {
                int startIndex = m_random.Next(m_activeItems.Count);
                int endIndex;
                do
                {
                    endIndex = m_random.Next(m_activeItems.Count);
                }
                while (endIndex == startIndex);

                m_activeItems.Move(startIndex, endIndex);
            }
        }

        public void Change()
        {
            if (canChange())
            {
                int targetIndex = m_random.Next(m_activeItems.Count);
                int sourceIndex = m_random.Next(m_sourceItems.Count);

                m_activeItems[targetIndex] = m_sourceItems[sourceIndex];
            }
        }

        public void Insert()
        {
            if (canAdd())
            {
                int targetIndex = m_random.Next(m_activeItems.Count + 1);
                int soureIndex = m_random.Next(m_sourceItems.Count);

                m_activeItems.Insert(targetIndex, m_sourceItems[soureIndex]);
            }
        }

        public void Reset()
        {
            // This is essential. '==' doesn't work against an arbitrary 'T'
            // And item.Equals will blow up if any item is null
            EqualityComparer<T> comparer = EqualityComparer<T>.Default;

            for (int i = 0; i < m_initialCount; i++)
            {
                int j = i % m_sourceItems.Count;

                if (i < m_activeItems.Count) // just re-shuffle
                {
                    if (comparer.Equals(m_activeItems[i], m_sourceItems[j]))
                    {
                        // This item is already what it should be
                    }
                    else // Go look for an active item that matches
                    {
                        bool found = false;
                        for (int k = i + 1; k < m_activeItems.Count; k++)
                        {
                            if (comparer.Equals(m_activeItems[k], m_sourceItems[j]))
                            {
                                m_activeItems.Move(k, i);
                                found = true;
                                break;
                            }
                        }

                        if (!found)
                        {
                            m_activeItems[i] = m_sourceItems[j];
                        }
                    }
                }
                else // need to add new ones
                {
                    m_activeItems.Add(m_sourceItems[j]);
                }
            }

            while (m_activeItems.Count > m_initialCount) // need to remove extras
            {
                m_activeItems.RemoveAt(m_activeItems.Count - 1);
            }
        }

        public ICommand AddCommand { get { return m_addCommand; } }
        public ICommand RemoveCommand { get { return m_removeCommand; } }
        public ICommand MoveCommand { get { return m_moveCommand; } }
        public ICommand ChangeCommand { get { return m_changeCommand; } }
        public ICommand InsertCommand { get { return m_insertCommand; } }
        public ICommand ResetCommand { get { return m_resetCommand; } }

        #region Implementation

        private DemoCollection(
            ReadOnlyCollection<T> sourceItems,
            ObservableCollection<T> activeItems,
            int minCount,
            int maxCount,
            int initialCount)
            : base(activeItems)
        {
            m_minCount = minCount;
            m_maxCount = maxCount;
            m_initialCount = initialCount;

            m_activeItems = activeItems;
            m_sourceItems = sourceItems;

            m_addCommand = ActionICommand.Create(Add, canAdd, out m_onCanAddChanged);
            m_insertCommand = ActionICommand.Create(Insert, canAdd, out m_onCanInsertChanged);
            m_removeCommand = ActionICommand.Create(Remove, canRemove, out m_onCanRemoveChanged);
            m_moveCommand = ActionICommand.Create(Move, canMove, out m_onCanMoveChanged);
            m_changeCommand = ActionICommand.Create(Change, canChange, out m_onCanChangeChanged);
            m_resetCommand = ActionICommand.Create(Reset);


            activeItems.CollectionChanged += delegate(object sender, NotifyCollectionChangedEventArgs e)
            {
                m_onCanRemoveChanged();
                m_onCanAddChanged();
                m_onCanMoveChanged();
                m_onCanChangeChanged();
                m_onCanInsertChanged();
            };
        }

        private readonly ObservableCollection<T> m_activeItems;
        private readonly ReadOnlyCollection<T> m_sourceItems;

        private bool canAdd()
        {
            return m_activeItems.Count < m_maxCount;
        }

        private bool canRemove()
        {
            return m_activeItems.Count > m_minCount;
        }

        private bool canMove()
        {
            return m_activeItems.Count > 1;
        }

        private bool canChange()
        {
            return m_activeItems.Count > 0;
        }

        private readonly Action m_onCanAddChanged, m_onCanRemoveChanged, m_onCanMoveChanged, m_onCanChangeChanged, m_onCanInsertChanged;
        private readonly ICommand m_addCommand, m_removeCommand, m_moveCommand, m_changeCommand, m_insertCommand, m_resetCommand;

        private readonly int m_minCount, m_maxCount, m_initialCount;

        private readonly Random m_random = new Random();

        #endregion

    }

}