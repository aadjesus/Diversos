using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using System.Collections;
using DynamicComparer;
using System.Diagnostics;
using System.Linq.Expressions;

namespace ConsoleApplication3
{
    public class Person
    {
        public Person(string firstName, string lastName, int age)
        {
            FirstName = firstName;
            LastName = lastName;
            Age = age;
        }

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Age { get; set; }
        public object Uncomparable { get; set; }

        public override string ToString()
        {
            return FirstName + " " + LastName + ", " + Age;
        }
    }

    class TestComparer : IComparer<Person>
    {
        #region IComparer<Person> Members

        public int Compare(Person x, Person y)
        {
            int c;
            c = x.FirstName.CompareTo(y.FirstName);
            if (c != 0) return c;
            return x.Age.CompareTo(y.Age);
        }

        #endregion
    }

    class Program
    {
        static void Main(string[] args)
        {
            TestComparer hardComparer = new TestComparer();

            var safeComparer = new DynamicComparer<Person>();
            safeComparer.SortOrder(x => x.FirstName, x => x.Age);

            var unsafeComparer = new DynamicComparer<Person>();
            unsafeComparer.SortOrder("FirstName", "Age");

            Person[] ppl = new Person[] {
                new Person("Aviad","P.",35),
                new Person("Goerge","Smith",33),
                new Person("Harry","Burke",30),
                new Person("Harry","Smith",20),
                new Person("George","Harrison",19)
            };

            Benchmark(1000000, "hard", x =>
            {
                Array.Sort(x, hardComparer);
                return x;
            });

            Benchmark(1000000, "unsafe", x =>
            {
                Array.Sort(x, unsafeComparer);
                return x;
            });

            Benchmark(1000000, "safe", x =>
            {
                Array.Sort(x, safeComparer);
                return x;
            });

            Benchmark(1000000, "LINQ", x =>
            {
                var rc = (from o in x
                          orderby o.FirstName, o.Age
                          select o).ToArray();
                return rc;
            });

            safeComparer.SortOrder(x => x.FirstName, x => x.LastName, x => x.Age);
            Demo(safeComparer, ppl);

            safeComparer.SortOrder(x => x.FirstName, x => x.Age, x => x.LastName);
            Demo(safeComparer, ppl);

            safeComparer.SortOrder(x => x.Age);
            Demo(safeComparer, ppl);
        }

        private static Func<Person, IComparable> Test(Expression<Func<Person, IComparable>> exp)
        {
            return exp.Compile();
        }

        static void Benchmark(int n, string test, Func<Person[], Person[]> deleg)
        {
            Console.WriteLine("Testing '{0}' {1} iterations.", test, n);
            var comp = new TestComparer();
            Stopwatch sw = new Stopwatch();
            for (int i = 0; i < n; i++)
            {
                Person[] xppl = new Person[] {
                    new Person("Aviad","P.",35),
                    new Person("Goerge","Smith",33),
                    new Person("Harry","Burke",30),
                    new Person("Harry","Smith",20),
                    new Person("George","Harrison",19)
                };
                deleg(xppl);
                if (i == 0) sw.Start();
            }
            sw.Stop();
            Console.WriteLine("{0,-10} {1} sorts took {2}", test, n, sw.Elapsed);
        }

        static void Demo(DynamicComparer<Person> comp, Person[] ppl)
        {
            Console.WriteLine(comp);
            Console.WriteLine("------------------------");
            Array.Sort(ppl, comp);
            PrintPeople(ppl);
        }

        static void PrintPeople(Person[] ppl)
        {
            foreach (var p in ppl)
                Console.WriteLine(p);
        }
    }
}
