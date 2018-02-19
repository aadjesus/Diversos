using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ConsoleApplication1
{
    class Program
    {

        static void Main(string[] args)
        {
            if (args.FirstOrDefault() == "")
            //if (String.IsNullOrEmpty(args.FirstOrDefault()))
            {
                
            }
            int _a = 0;
            args
                .ToList()
                .ForEach(f => Console.Write(String.Concat(_a++, "\n", f)));
        }
    }
}
