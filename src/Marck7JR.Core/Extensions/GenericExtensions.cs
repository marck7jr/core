using System;
using System.Linq;

namespace Marck7JR.Core.Extensions
{
    public static class GenericExtensions
    {
        public static T? IfNotNull<T>(this T? value, Action<T>? action, Action? fallback = null) where T : class
        {
            if (value is not null)
            {
                action?.Invoke(value);
            }
            else if (fallback is not null)
            {
                fallback?.Invoke();
            }

            return value;
        }

        public static object? Reflect(this object value, Type type)
        {
            var @object = Activator.CreateInstance(type);

            value.GetType().GetProperties().Where(_ => _.CanWrite).AsParallel().ForAll(_ =>
            {
                type.GetProperty(_.Name)?.SetValue(@object, _.GetValue(value));
            });

            return @object;
        }

        public static T? Reflect<T>(this object value) => (T?)Convert.ChangeType(value.Reflect(typeof(T)), typeof(T));
    }
}
