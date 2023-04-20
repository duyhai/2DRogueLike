using Godot;
using Godot.Collections;

namespace Utility
{
    public static class ArrayUtil
    {
        public static object Min<[MustBeVariant] T>(ref Array<T> array, CompareFunction<T> function)
        {
            T min = default;
            foreach (T item in array)
            {
                min = function(min, item);
            }
            return min;
        }

        public delegate T CompareFunction<T>(T x, T y);
    }
}