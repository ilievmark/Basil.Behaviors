using System.Collections.Generic;

namespace Basil.Behaviors.Extensions
{
    public static class CollectionsExtensions
    {
        #region List extensions

        public static T GetNextFrom<T>(this IList<T> collection, T current)
        {
            var index = collection.IndexOf(current);
            if (index + 1 < collection.Count)
                return collection[index + 1];
            return default;
        }

        #endregion
    }
}