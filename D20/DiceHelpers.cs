using System;
using System.Linq;
using System.Collections.Generic;

namespace D20
{
    internal static class DiceHelpers
	{
        public static IEnumerable<T> Times<T>(this int count, Func<T> f)
        {
            return Enumerable.Range(0, count).Select(i => f());
        }
        public static IEnumerable<T> Times<T>(this int count, Func<int, T> f)
        {
            return Enumerable.Range(0, count).Select(f);
        }

        public static IEnumerable<T> Yield<T>(this T value)
        {
        	yield return value;
        }

		public static IEnumerable<TResult> Join<TKey, TResult>(
			this IEnumerable<TKey> outer,
			IEnumerable<TKey> inner,
			Func<TKey,TKey, TResult> resultSelector)
		{
			return outer.Join(inner, x => true, x => true, resultSelector);
		}

		public static int BinomialCoefficient(int n, int k)
		{
			throw new Exception("Check implementation of BinomialCoefficient for correctness");

			// if (k < 0 || k > n)
			// 	return 0;
			// if (k == 0 || k == n)
			// 	return 1;
			// k = Math.Min(k, n - k);

			// var num = 1L;
			// var denom = 1L;

			// for (int i = 1; i <= k; i++)
			// {
			// 	num *= n - i + 1;
			// 	denom *= i;
			// }
			// return (int)(num/denom);
		}
	}
}