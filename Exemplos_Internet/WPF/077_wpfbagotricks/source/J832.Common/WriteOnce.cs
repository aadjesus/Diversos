using System;
using System.Threading;

namespace J832.Common
{
    /// <summary>
    ///     A wrapper to ensure strong mutability behavior.
    /// </summary>
    public class WriteOnce<T>
    {
        /// <summary>
        ///     Creates a new <see cref="WriteOnce{T}"/> that
        ///     will throw an exception if the value is accesss before Set.
        /// </summary>
        public WriteOnce() : this(true) { }

        /// <summary>
        ///     Creates a new <see cref="WriteOnce{T}"/>.
        /// </summary>
        /// <param name="throwIfNotSet">
        ///     true to throw an exception if the value is accessed before set.
        ///     false will cause value access when unset to return the default
        ///     value of T.
        /// </param>
        public WriteOnce(bool throwIfNotSet)
        {
            m_throwsIfNotSet = throwIfNotSet;
        }

        /// <summary>
        ///     Gets whether the value has been set for this instance.
        /// </summary>
        public bool IsSet { get { return m_interlock != 0; } }

        /// <summary>
        ///     The value that has been set.
        /// </summary>
        /// <remarks>
        ///     Throw an exception if <see cref="IsSet"/> is false 
        ///     and <see cref="ThrowsIfNotSet"/> is true.
        /// </remarks>
        public T Value
        {
            get
            {
                if (trySetLock()) // not set yet
                {
                    if (m_throwsIfNotSet)
                    {
                        throw new InvalidOperationException("Value has not been set.");
                    }
                    else
                    {
                        return default(T);
                    }
                }
                else // already set
                {
                    return m_value;
                }
            }
            set
            {
                if (trySetLock())
                {
                    m_value = value;
                }
                else
                {
                    throw new InvalidOperationException("Already set or get already attempted.");
                }
            }
        }

        /// <summary>
        ///     Gets whether the implementation will throw
        ///     an exception if the value is accessed before set is called.
        /// </summary>
        public bool ThrowsIfNotSet { get { return m_throwsIfNotSet; } }

        /// <summary>
        ///     Creates a WriteOnce from a provided value.
        /// </summary>
        public static implicit operator T(WriteOnce<T> writeOnce)
        {
            return writeOnce.Value;
        }

        /// <summary>
        ///     The value that has been set.
        /// </summary>
        /// <remarks>
        ///     Throw an exception if <see cref="IsSet"/> is false 
        ///     and <see cref="ThrowsIfNotSet"/> is true.
        /// </remarks>
        public static implicit operator WriteOnce<T>(T value)
        {
            WriteOnce<T> writeOnce = new WriteOnce<T>();
            writeOnce.Value = value;
            return writeOnce;
        }

        #region Implementation

        private bool trySetLock()
        {
            return Interlocked.Exchange(ref m_interlock, 1) == 0;
        }

        private T m_value;
        private int m_interlock;

        private readonly bool m_throwsIfNotSet;

        #endregion

    }
}