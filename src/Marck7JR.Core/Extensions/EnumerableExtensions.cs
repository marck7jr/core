using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace Marck7JR.Core.Extensions
{
    public static class EnumerableExtensions
    {
        public static IEnumerable<T?> Reflect<T>(this IEnumerable<object> enumerable) => enumerable.Select(_ => _.Reflect<T>());
        public static ObservableCollection<T> ToObservableCollection<T>(this IEnumerable<T> enumerable) => new(enumerable);
        public static async ValueTask<ObservableCollection<T>> ToObservableCollectionAsync<T>(this IAsyncEnumerable<T> asyncEnumerable) => new(await asyncEnumerable.ToListAsync());
    }
}
