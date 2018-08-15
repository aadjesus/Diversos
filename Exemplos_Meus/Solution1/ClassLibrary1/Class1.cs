using System;

namespace ClassLibrary1
{
    public class Class1 : IClass1
    {
        public event EventHandler CountdownCompleted;

        public MyClassVM xxxx(int iDExterno)
        {
            throw new NotImplementedException();
        }
    }

    public interface IClass1
    {
        MyClassVM xxxx(int iDExterno);
    }

    public class MyClassVM
    {

    }
}
