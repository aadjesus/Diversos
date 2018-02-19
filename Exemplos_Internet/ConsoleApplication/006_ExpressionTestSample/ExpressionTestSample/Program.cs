using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Linq.Expressions;
using System.Collections;
using System.IO;

namespace ExpressionTestSample
{
    class Program
    {
        static void Main(string[] args)
        {



            Func<int, bool> mydelegate = x => x < 5;
            Expression<Func<int, bool>> myexpressiondelegate = x => x < 5;

            ParameterExpression param1 = myexpressiondelegate.Parameters[0];
            BinaryExpression bbody = myexpressiondelegate.Body as BinaryExpression;

            ParameterExpression parambodyleft = bbody.Left as ParameterExpression;
            ConstantExpression constRight = bbody.Right as ConstantExpression;
            ExpressionType type = bbody.NodeType;


            BlockExpression body = Expression.Block(
                Expression.LessThan(parambodyleft, constRight));

            Expression<Func<int, bool>> returnexpression = Expression.Lambda<Func<int, bool>>(body, param1);
            Func<int, bool> returnFunc = returnexpression.Compile();


            bool returnvalue = returnFunc(3);


            //Func<IEnumerable<int>, int, bool> dividesectionmethod = (x, y) =>
            //{
            //    int nos1 = 0;
            //    int nos2 = 0;
            //    foreach (int i in x)
            //    {
            //        if (i <= y)
            //            nos1++;
            //        else
            //            nos2++;
            //    }
            //    return nos1 > nos2;
            //};


            ParameterExpression enumerableExpression = Expression.Parameter(typeof(IEnumerable<int>), "x");
            ParameterExpression intexpression = Expression.Parameter(typeof(int), "y");

            ParameterExpression localvarnos1 = Expression.Variable(typeof(int), "nos1");
            ParameterExpression localvarnos2 = Expression.Variable(typeof(int), "nos2");
            ConstantExpression zeroConstantintval = Expression.Constant(0);
            BinaryExpression bexplocalnos1 = Expression.Assign(localvarnos1, zeroConstantintval);
            BinaryExpression bexplocalnos2 = Expression.Assign(localvarnos2, zeroConstantintval);

            //As Expression does not support Foreach we need to get Enumerator before doing loop

            ParameterExpression enumerator = Expression.Variable(typeof(IEnumerator<int>), "enumerator");
            BinaryExpression assignenumerator = Expression.Assign(enumerator, Expression.Call(enumerableExpression, typeof(IEnumerable<int>).GetMethod("GetEnumerator")));


            var currentelement = Expression.Parameter(typeof(int), "i");
            var callCurrent = Expression.Assign(currentelement, Expression.Property(enumerator, "Current"));

            BinaryExpression firstlessequalsecond = Expression.LessThanOrEqual(currentelement, intexpression);

            MethodCallExpression movenext = Expression.Call(enumerator, typeof(IEnumerator).GetMethod("MoveNext"));

            LabelTarget looplabel = Expression.Label("looplabel");
            LabelTarget returnLabel = Expression.Label(typeof(bool), "retval");



            BlockExpression block = Expression.Block(
                    new ParameterExpression[] { 
                    localvarnos1, localvarnos2, enumerator, currentelement },
                    bexplocalnos1,
                    bexplocalnos2,
                    assignenumerator,
                    Expression.Loop(
                        Expression.IfThenElse(
                            Expression.NotEqual(movenext, Expression.Constant(false)),
                            Expression.Block(
                                callCurrent,
                                Expression.IfThenElse(
                                    firstlessequalsecond,
                                    Expression.Assign(
                                        localvarnos1,
                                        Expression.Increment(localvarnos1)),
                                    Expression.Assign(
                                        localvarnos2,
                                        Expression.Increment(localvarnos2)))),
                            Expression.Break(looplabel)),
                            looplabel),
                            Expression.LessThan(localvarnos1, localvarnos2));

            Expression<Func<IEnumerable<int>, int, bool>> lambda =
                Expression.Lambda<Func<IEnumerable<int>, int, bool>>(
                    block,
                    enumerableExpression,
                    intexpression);


            Func<IEnumerable<int>, int, bool> dividesectionmethod = lambda.Compile();



            List<int> lst = GetList();


            bool lessthan100isgreater = dividesectionmethod(lst, 3);

            if (lessthan100isgreater)
                Console.WriteLine("There are less numbers above hundred than more than hundred.");
            else
                Console.WriteLine("There are more numbers above hundred than less than hundred.");

            Console.ReadLine();






            //ParameterExpression paramExp = Expression.Parameter(typeof(int), "m");


            //Expression<Predicate<int>> expression = Expression.Lambda<Predicate<int>>(Expression.LessThan(Expression.Parameter(typeof(int), "m"), Expression.Constant(10)),
            //    Expression.Parameter(typeof(int), "m"));

            //Predicate<int> predicate = expression.Compile();
            //bool isMatch = predicate(8);

        }


        public static List<int> GetList()
        {
            List<int> mylist = new List<int>();
            do
            {
                try
                {
                    Console.WriteLine("Input any integer and press return");

                    int enterednum = Convert.ToInt32(Console.ReadLine());

                    mylist.Add(enterednum);
                }
                catch (Exception ex) { Console.WriteLine(ex.Message); }

                Console.WriteLine("Do you want to continue? Press n to exit");
                if (Console.ReadLine().Equals("n")) break;
            } while (true);
            return mylist;
        }
    }
}
