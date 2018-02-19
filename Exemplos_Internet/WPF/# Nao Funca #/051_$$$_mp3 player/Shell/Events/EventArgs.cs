using System;

namespace Framework.Shell.Events
{
    public class EventArgs<T> : EventArgs
    {
        private T _data;

        public EventArgs(T data)
        {
            _data = data;
        }

        public T Data
        {
            get { return _data; }
        }
    }
}