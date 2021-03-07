using Godot.Collections;

namespace Utility
{
    public static class ArrayUtil
    {
        public static object Min(ref Array array, CompareFunction<object> function)
        {
            object min = null;
            foreach (object item in array)
            {
                min = function(min, item);
            }
            return min;
        }

        public delegate T CompareFunction<T>(T x, T y);
    }
}