using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace PLINQ.PrimeDemo
{
    public class PrimeNumberDemo
    {
        public void ShowDemo()
        {
            bool _firstTimeFaster = false;

            //Set how many tests we want to do
            List<int> upperLimits = new List<int> { 10, 100, 500, 1000, 5000, 7500, 10000, 20000, 50000, 100000, 150000 };

            foreach (int upperLimit in upperLimits)
            {
                //Create the numbers to test for this upper limit
                List<PotentialPrime> numbersToCheck = CreateNumberList(upperLimit);

                //Run the demos for both parallel and sequential runs
                long sequentialTime = RunSequentialTest(numbersToCheck);
                long parallelTime = RunParallelTest(numbersToCheck);


                Console.WriteLine("Testing Prime Numbers Up To {0}", upperLimit);
                Console.WriteLine("     Sequential Time {0}ms", sequentialTime);
                Console.WriteLine("     Parallel Time {0}ms", parallelTime);
                Console.WriteLine();

                //If this is the first time the parallel test is faster
                //show some eye candy
                if (!_firstTimeFaster && (parallelTime < sequentialTime))
                {
                    Console.WriteLine("The Parallel Execution time is now faster than the sequential execution time.");
                    Console.WriteLine();

                    _firstTimeFaster = true;
                }

                Console.WriteLine();
            }
            Console.WriteLine("Finished!");
            Console.ReadLine();
        }

        /// <summary>
        /// Runs a prime check on a list of numbers using PLINQ
        /// </summary>
        /// <param name="numbersToCheck"></param>
        /// <returns></returns>
        private long RunParallelTest(List<PotentialPrime> numbersToCheck)
        {
            Stopwatch watch = new Stopwatch();
            watch.Start();

            var query = from num in numbersToCheck.AsQueryable()
                        select new { Num = num, IsPrime = IsPrime(num.Value) };
            
            Parallel.ForEach(numbersToCheck, num =>
            {
                var q = IsPrime(num.Value);
                num.IsPrime = q;

            });

            return watch.ElapsedMilliseconds;
        }

        /// <summary>
        /// Runs a prime check on a list of numbers using standard looping
        /// </summary>
        /// <param name="numbersToCheck"></param>
        /// <returns></returns>
        private long RunSequentialTest(List<PotentialPrime> numbersToCheck)
        {
            Stopwatch watch = new Stopwatch();
            watch.Start();

            foreach (PotentialPrime num in numbersToCheck)
            {
                var q = IsPrime(num.Value);
                num.IsPrime = q;
                
            }
            return watch.ElapsedMilliseconds;
        }

        private List<PotentialPrime> CreateNumberList(int max)
        {
            List<PotentialPrime> numbersToCheck = new List<PotentialPrime>(max);
            for (int i = 3; i < max; i++)
            {
                PotentialPrime pot = new PotentialPrime();
                pot.Value = i + 1;
                pot.IsPrime = false;

                numbersToCheck.Add(pot);
                
            }
            return numbersToCheck;
        }

        /// <summary>
        /// Determines if a specific number is prime
        /// </summary>
        /// <param name="number"></param>
        /// <returns></returns>
        private bool IsPrime(int number)
        {
            bool found = false;
            for (int i = 2; i < number && !found; i++)
            {
                if (number % i == 0)
                {
                    found = true;
                    break;
                }
            }

            return !found;
        }
    }
}
