using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AGTIV.Framework.MVC.Framework.Helper
{
    public static class ExtensionHelper
    {
        public static bool MatchAny(this string str, params string[] candidates)
        {
            if (candidates != null)
            {
                for (int i = 0; i < candidates.Length; i++)
                {
                    if (str == candidates[i])
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        public static bool MatchAny(this int integer, params int[] candidates)
        {
            if (candidates != null)
            {
                for (int i = 0; i < candidates.Length; i++)
                {
                    if (integer == candidates[i])
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        public static bool MatchAny(this object obj, params Type[] candidates)
        {
            for (int i = 0; i < candidates.Length; i++)
            {
                if (obj.GetType() == candidates[i])
                {
                    return true;
                }
            }

            return false;
        }

        public static bool IsMatch<T>(this IEnumerable<T> firstList, IEnumerable<T> secondList) where T : struct
        {
            var firstNotSecond = firstList.Except(secondList).ToList();
            var secondNotFirst = secondList.Except(firstList).ToList();

            return !firstNotSecond.Any() && !secondNotFirst.Any();
        }

        public static IEnumerable<TSource> DistinctBy<TSource, TKey>(this IEnumerable<TSource> source, Func<TSource, TKey> keySelector)
        {
            HashSet<TKey> seenKeys = new HashSet<TKey>();
            foreach (TSource element in source)
            {
                if (seenKeys.Add(keySelector(element)))
                {
                    yield return element;
                }
            }
        }
    }
}
