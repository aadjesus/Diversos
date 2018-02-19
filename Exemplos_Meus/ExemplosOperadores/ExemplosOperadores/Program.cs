using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ExemplosOperadores
{
    public class XistoDTO
    {
        public XistoDTO()
        {
            _campo1 = 0;
            _campo2 = 0;
            _diferente = null;
        }

        private bool _isChanged;
        private bool _isDeleted;
        private string _diferente;

        public string Diferente
        {
            get { return _diferente; }            
        }

        private int _campo1;
        private int _campo2;

        public int Campo2
        {
            get
            {
                return _campo2;
            }
            set
            {
                _isChanged |= (_campo2 != value);
                if (_isChanged && _campo2 != 0)
                    _diferente += String.Concat("Campo2=",_campo2," para,",  value);
                
                _campo2 = value;
            }
        }

        public int Campo1
        {
            get
            {
                return _campo1;
            }
            set
            {
                _isChanged |= (_campo1 != value);
                if (_isChanged && _campo1 != 0)
                    _diferente += String.Concat(" Campo1=", _campo1, " para,", value);
                _campo1 = value;
            }
        }

        public virtual bool IsChanged
        {
            get
            {
                return _isChanged;
            }
        }

        public virtual bool IsDeleted
        {
            get
            {
                return _isDeleted;
            }
        }


    }

    class Program
    {
        static void Main(string[] args)
        {
            XistoDTO xx = new XistoDTO();
            xx.Campo1 = 1;
            xx.Campo2 = 1;

            xx.Campo1 = 2;
            xx.Campo2 = 1;            
            if (xx == null)
            {

            }

            int a = 0x0c;
            a &= 0x06;
            Console.WriteLine("0x{0:x8}", a);
            bool b = true;
            b |= false;
            b |= true;
            Console.WriteLine(b);
            return;


            Console.WriteLine("# Unary___________________________");
            Unary();
            Console.WriteLine("#");

            Console.WriteLine("# Multiplicative__________________");
            Multiplicative();
            Console.WriteLine("#");

            Console.WriteLine("# Bitwise_________________________");
            Bitwise();
            Console.WriteLine("#");

            Console.WriteLine("# Assignment______________________");
            Assignment();
            Console.WriteLine("#");

            Console.WriteLine("# Relational______________________");
            Relational();
            Console.WriteLine("#");

            Console.WriteLine("# Conditionals____________________");
            Conditionals();
            Console.WriteLine("#");

            Console.WriteLine("# Unary___________________________");
            Switch();
            Console.WriteLine("#");
        }

        private static void Switch()
        {
            int i;
            string s;

            Console.Write("int i=? ");
            i = int.Parse(Console.ReadLine());
            Console.Write("name s=? ");
            s = Console.ReadLine();

            switch (i)
            {
                case 1:
                    Console.WriteLine("i = 1");
                    break;
                case 2:
                case 3:
                    Console.WriteLine("i = 2 or 3");
                    break;
                default:
                    Console.WriteLine("hmm... i = " + i);
                    break;
            }

            switch (s)
            {
                case "Andy":
                    Console.WriteLine("Hello Andy!");
                    break;
                case "Ralph":
                case "Bill":
                    Console.WriteLine("Ah, one of the other guys...");
                    break;
                default:
                    Console.WriteLine("Hello... umm... " + s);
                    break;
            }
        }

        private static void Conditionals()
        {
            int i;
            int j;

            Console.Write("int i=? ");
            i = int.Parse(Console.ReadLine());
            Console.Write("int j=? ");
            j = int.Parse(Console.ReadLine());

            Console.WriteLine("i={0} j={1}", i, j);

            if (i <= j)
                Console.WriteLine("i <= j");

            if (j <= i)
                Console.WriteLine("j <= i");
            else
            {
                Console.WriteLine("The number j({0})", j);
                Console.WriteLine("is greater than i({0})", i);
            }

            if (i >= 5 && j <= 10)
                Console.WriteLine("i >= 5 AND j <= 10");
            else if (i >= 5 || j <= 10)
                Console.WriteLine("i >= 5 OR j <= 10");
            else
                Console.WriteLine("i = {0} j = {1}", i, j);
        }

        private static void Relational()
        {
            int i, j;

            Console.Write("int i=? ");
            i = int.Parse(Console.ReadLine());
            Console.Write("int j=? ");
            j = int.Parse(Console.ReadLine());

            Console.WriteLine("i<j = " + (i < j));
            Console.WriteLine("i>j = " + (i > j));
            Console.WriteLine("i==j = " + (i == j));
            Console.WriteLine("i!=j = " + (i != j));
        }

        private static void Assignment()
        {
            int i = 5;
            int a, b;

            Console.WriteLine("i=" + i);
            i += 4;
            Console.WriteLine("i+=4 = " + i);
            i -= 8;
            Console.WriteLine("i-=8 = " + i);
            i *= 7;
            Console.WriteLine("i*=7 = " + i);
            i /= 2;
            Console.WriteLine("i/=2 = " + i);
            Console.WriteLine("");

            a = b = 4;
            Console.WriteLine("a=b=" + a);
            a *= 5 + 5;
            Console.WriteLine("a*=5+5 = " + a);
            b = b * 5 + 5;
            Console.WriteLine("b=b*5+5 = " + b);
        }

        private static void Bitwise()
        {
            int not;
            int rShift;
            int lShift;
            int and;
            int xor;
            int or;
            int a = 30, b = 2;

            not = ~a;
            rShift = a >> b;
            lShift = a << b;
            and = a & b;
            xor = a ^ b;
            or = a | b;

            Console.WriteLine("a={0} b={1}", a, b);
            Console.WriteLine("~a=" + not);
            Console.WriteLine("a>>b=" + rShift);
            Console.WriteLine("a<<b=" + lShift);
            Console.WriteLine("a&b=" + and);
            Console.WriteLine("a^b=" + xor);
            Console.WriteLine("a|b=" + or);
        }

        private static void Multiplicative()
        {
            int add;
            int sub;
            int mult;
            int div;
            int mod;
            int x = 17, y = 4;

            add = x + y;
            sub = x - y;
            mult = x * y;
            div = x / y;
            mod = x % y;

            Console.WriteLine("x={0} y={1}", x, y);
            Console.WriteLine("x+y = " + add);
            Console.WriteLine("x-y = " + sub);
            Console.WriteLine("x*y = " + mult);
            Console.WriteLine("x/y = " + div);
            Console.WriteLine("x%y = " + mod);
        }

        private static void Unary()
        {
            int x;
            bool a;

            x = 4;
            Console.WriteLine("x = " + x);
            Console.WriteLine("x++ = " + x++); // post increment
            Console.WriteLine("x = " + x);
            Console.WriteLine("++x = " + ++x); // pre increment
            Console.WriteLine("x = " + x);
            Console.WriteLine("x-- = " + x--); // post decrement
            Console.WriteLine("x = " + x);
            Console.WriteLine("--x = " + --x); // pre decrement
            Console.WriteLine("x = " + x);
            Console.WriteLine("");

            x = -2;
            Console.WriteLine("x = " + x);
            Console.WriteLine("+x = " + +x);
            Console.WriteLine("-x = " + -x);
            Console.WriteLine("");

            x = 2;
            Console.WriteLine("x = " + x);
            Console.WriteLine("+x = " + +x);
            Console.WriteLine("-x = " + -x);
            Console.WriteLine("");

            a = false;
            Console.WriteLine("a = " + !a);

            a = true;
            Console.WriteLine("a = " + !a);
        }
    }


}
