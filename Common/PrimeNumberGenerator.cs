using System;
using System.Collections;
using System.Linq;

namespace Common
{
    public static class PrimeNumberGenerator
    {
 
        public static int GetNextPrime(int number)
        {
            while (true)
            {
                var isPrime = true;
                number = number + 1;

                var squaredNumber = (int)Math.Sqrt(number);

                for (var i = 2; i <= squaredNumber; i++)
                {
                    if (number % i != 0)
                    {
                        continue;
                    }

                    isPrime = false;
                    break;
                }

                if (isPrime)
                {
                    return number;
                }
            }
        }

      
    }
}
