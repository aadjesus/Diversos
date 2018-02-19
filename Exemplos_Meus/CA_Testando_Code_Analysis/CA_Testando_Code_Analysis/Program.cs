using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WindowsFormsApplication1;

namespace Testando
{
    class Program
    {
        static void Main(string[] args)
        {
            //Class1 x1 = new Class1();
            //if (x1 == null)
            //{

            //}

            //if (args.FirstOrDefault() == "")
            //{ 
            //}

            //Warning	1	The variable '_aaa' is assigned but its value is never used	c:\Users\alessandro.augusto\Documents\Visual Studio\Exemplos_Meus\CA_Testando_Code_Analysis\CA_Testando_Code_Analysis\Program.cs	17	20	CA_Testando_Code_Analysis


            if (String.IsNullOrEmpty(args.FirstOrDefault()))
            {
                Form1 xx1 = new Form1();
                xx1.Show();
            }
        }
    }

    //public class Testando_Class_Teste
    //{

    //}
}
