using System;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace Marck7JR.Core.Extensions
{
    public static class JsonExtensions
    {
        public static object? FromJson(this string json, Type type, JsonSerializerOptions? options = null)
        {
            try
            {
                var @object = JsonSerializer.Deserialize(json, type, options);
                return @object;
            }
            catch (JsonException jsonException)
            {
                Debug.WriteLine(jsonException);
            }

            return default;
        }

        public static T? FromJson<T>(this string json, JsonSerializerOptions? options = null) => (T?)json.FromJson(typeof(T), options);

        public static async Task<object?> FromJsonAsync(this string @string, Type type, JsonSerializerOptions? options = null, CancellationToken cancellationToken = default)
        {
            try
            {
                var bytes = Encoding.UTF8.GetBytes(@string);

                using MemoryStream memoryStream = new(bytes);

                var @object = await JsonSerializer.DeserializeAsync(memoryStream, type, options, cancellationToken);
                return @object;
            }
            catch (JsonException jsonException)
            {
                Debug.WriteLine(jsonException);
            }

            return default;
        }

        public static async Task<T?> FromJsonAsync<T>(this string @string, JsonSerializerOptions? options = null, CancellationToken cancellationToken = default) => (T?)await @string.FromJsonAsync(typeof(T), options, cancellationToken);

        public static string? ToJson(this object @object, Type type, JsonSerializerOptions? options = null)
        {
            try
            {
                var json = JsonSerializer.Serialize(@object, type, options);
                return json;
            }
            catch (JsonException jsonException)
            {
                Debug.Write(jsonException);
            }

            return default;
        }

        public static string? ToJson<T>(this T t, JsonSerializerOptions? options = null) => t!.ToJson(typeof(T), options);

        public static async Task<string?> ToJsonAsync(this object @object, Type type, JsonSerializerOptions? options = null, CancellationToken cancellationToken = default)
        {
            try
            {
                using MemoryStream memoryStream = new();
                using StreamReader streamReader = new(memoryStream);

                await JsonSerializer.SerializeAsync(memoryStream, @object, type, options, cancellationToken);
                await memoryStream.FlushAsync(cancellationToken);

                memoryStream.Seek(0, SeekOrigin.Begin);

                var json = await streamReader.ReadToEndAsync();
                return json;
            }
            catch (JsonException jsonException)
            {
                Debug.Write(jsonException);
            }

            return default;
        }

        public static async Task<string?> ToJsonAsync<T>(this T t, JsonSerializerOptions? options = null, CancellationToken cancellationToken = default) => await t!.ToJsonAsync(typeof(T), options, cancellationToken);
    }
}
