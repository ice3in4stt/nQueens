using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Queens2
{
    class Helpers
    {
        /// <summary>
        /// Returns a random integer from 1 to max (inclusive) that is not in the given list 
        /// </summary>
        /// <param name="max">max value (inclusive)</param>
        /// <param name="list">list of unallowed integers</param>
        /// <returns></returns>
        public static int randomNumber(int max, List<int> list)
        {
            Random random = new Random();
            int x;

            do
            {
                x = random.Next(max) + 1;
            } while (list.Contains(x));

            return x;
        }

        /// <summary>
        /// Compares if lists have the same content regardless of order.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list1"></param>
        /// <param name="list2"></param>
        /// <returns></returns>
        /// Method was copied as is from Stackoverflow.
        public static bool ScrambledEquals<T>(IEnumerable<T> list1, IEnumerable<T> list2)
        {
            var cnt = new Dictionary<T, int>();
            foreach (T s in list1)
            {
                if (cnt.ContainsKey(s))
                {
                    cnt[s]++;
                }
                else
                {
                    cnt.Add(s, 1);
                }
            }
            foreach (T s in list2)
            {
                if (cnt.ContainsKey(s))
                {
                    cnt[s]--;
                }
                else
                {
                    return false;
                }
            }
            return cnt.Values.All(c => c == 0);
        }
    }
}
