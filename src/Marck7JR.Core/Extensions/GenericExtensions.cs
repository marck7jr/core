using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;

namespace Marck7JR.Core.Extensions
{
    public static class GenericExtensions
    {
        [Obsolete("Use 'GetCustomAttribute`2' instead.")]
        public static string GetDescription<T>(this T t)
        {
            DescriptionAttribute? attribute = null;

            if (t is null)
            {
                throw new ArgumentNullException(nameof(t));
            }

            MemberInfo memberInfo = t switch
            {
                Enum _ => t.GetType().GetField($"{t}"),
                _ => t.GetType()
            };

            attribute = memberInfo.GetCustomAttribute<DescriptionAttribute>(false);

            if (attribute is not null)
            {
                return attribute.Description;
            }

            throw new NullReferenceException($"The field must have a {nameof(DescriptionAttribute)}.");
        }

        public static U GetCustomAttribute<T, U>(this T t, bool inherit = false) where U : Attribute => t.GetCustomAttributes<T, U>(inherit).FirstOrDefault();

        public static IEnumerable<U> GetCustomAttributes<T, U>(this T? t, bool inherit = false)
            where U : Attribute
        {
            if (t is null)
            {
                throw new ArgumentNullException(nameof(t));
            }

            MemberInfo memberInfo = t switch
            {
                Enum _ => t.GetType().GetField($"{t}"),
                _ => t.GetType()
            };

            var attributes = memberInfo.GetCustomAttributes<U>(inherit);

            if (attributes.Any())
            {
                return attributes;
            }

            throw new InvalidOperationException($"'{typeof(T).Name}' must have a {typeof(U).Name}.");
        }

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
