using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace Marck7JR.Core.Extensions
{
    public static class EnumerableExtensions
    {
        public static IEnumerable<T?> Reflect<T>(this IEnumerable<object> enumerable) => enumerable.Select(_ => _.Reflect<T>());
        public static ObservableCollection<T> ToObservableCollection<T>(this IEnumerable<T> enumerable) => new(enumerable);
    }
}
