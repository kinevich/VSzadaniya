using System;

namespace Callback
{
    public static class Extension
    {
        public static bool MyAny(this int[] array, Func<int, bool> predicate)
        {
            foreach (int i in array)
            {
                if (predicate(i))
                    return true;
            }
            return false;
        }

        public static bool MyAll(this int[] array, Func<int, bool> predicate)
        {
            foreach (int i in array)
            {
                if (!predicate(i))
                    return false;
            }
            return true;
        }
    }
}
