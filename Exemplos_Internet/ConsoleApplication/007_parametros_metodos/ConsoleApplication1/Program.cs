using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConsoleApplication1
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine(CalcularSalario());
            Console.WriteLine(CalcularSalario(20));
            Console.WriteLine(CalcularSalario(20, 15));
            Console.WriteLine(CalcularSalario(20, 15, 8));
            Console.WriteLine(CalcularSalario(20, valorHora:5,  horasPorDia: 11));
        }

        static int CalcularSalario(int diasDetrabalho = 20, int valorHora = 15, int horasPorDia = 8)
        {
            return diasDetrabalho * (horasPorDia * valorHora);
        }
    }


}
