using System;
using System.Collections.Generic;
using System.Text;

namespace CTIServerFlexClientAPI.VStream
{
    public interface IStream
    {
        int Port {
            get;
            set;
        }
        String HostAddress
        {
            get;
            set;
        }
        void Open();
        void Close();
        void AddListener(IStreamListener listener);
        void RemoveListener(IStreamListener listener);
    }
}
