using System;
using System.Collections.Generic;
using System.Linq;

namespace GameCore.Utils
{
    public static class IEnumerableUtils
    {
        public static bool HasElement<T>(this IEnumerable<T> someEnumerable, Type elementType)
        {
            return someEnumerable.Any(e => e.GetType() == elementType);
        }

        public static bool TryGet<T>(this IEnumerable<T> someCollection, Type elementType, out T element)
        {
            element = default;
            var hasElement = someCollection.HasElement(elementType);
            if (!hasElement)
                return false;

            element = someCollection.First(someElement => someElement.GetType() == elementType);
            return true;
        }

        public static bool TryGet<T, TV>(this IEnumerable<T> someCollection, out TV element) 
            where TV : T
        {
            var tryingGet = TryGet(someCollection, typeof(TV), out var outElement);
            element = (TV)outElement;
            return tryingGet;
        }
    }
}