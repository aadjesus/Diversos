using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataSynchronizer
{
    public interface ILogger
    {
        void Write(string message);
    }
}
