using System;
using System.Diagnostics;
using System.IO;
using YamlDotNet.Serialization;

namespace Marck7JR.Core.Extensions
{
    public static class YamlExtensions
    {
        public static object? FromYaml(this string? yaml, Type type)
        {
            try
            {
                if (yaml is null)
                {
                    throw new ArgumentNullException(nameof(yaml));
                }

                var deserializer = new DeserializerBuilder()
                    .IgnoreUnmatchedProperties()
                    .Build();

                var @object = deserializer.Deserialize(yaml, type);
                return @object;
            }
            catch (Exception exception)
            {
                Debug.WriteLine(exception);
            }

            return default;
        }

        public static T? FromYaml<T>(this string? yaml) => (T?)yaml.FromYaml(typeof(T));

        public static string? ToYaml(this object? @object, Type type)
        {
            try
            {
                if (@object is null)
                {
                    throw new ArgumentNullException(nameof(@object));
                }

                var serializer = new SerializerBuilder()
                    .Build();

                using MemoryStream memoryStream = new();
                using StreamReader streamReader = new(memoryStream);
                using StreamWriter streamWriter = new(memoryStream);

                serializer.Serialize(streamWriter, @object, type);
                streamWriter.Flush();
                memoryStream.Flush();
                memoryStream.Seek(0, SeekOrigin.Begin);

                var yaml = streamReader.ReadToEnd();

                return yaml;
            }
            catch (Exception exception)
            {
                Debug.Write(exception);
            }

            return default;
        }

        public static string? ToYaml<T>(this T? @object) => @object.ToYaml(typeof(T));
    }
}
