using Marck7JR.Core.Extensions;
using System;
using System.Linq.Expressions;

namespace Microsoft.EntityFrameworkCore.Storage.ValueConversion
{
    public class ObjectValueConverter : ValueConverter<object?, string?>
    {
        private struct ObjectValue
        {
            public ObjectValue(object? @object)
            {
                Json = @object?.ToJson();
                TypeName = @object?.GetType().FullName;
            }

            public string? TypeName { get; }
            public string? Json { get; }

            public object? GetObject() => Json?.FromJson(Type.GetType(TypeName!));
            public override string? ToString() => this.ToYaml();
        }

        public ObjectValueConverter(ConverterMappingHints? mappingHints = null) : base(ConvertToStringExpression, ConvertFromStringExpression, mappingHints)
        {

        }

        private static Expression<Func<object?, string?>> ConvertToStringExpression => (@object) => new ObjectValue(@object).ToString();
        private static Expression<Func<string?, object?>> ConvertFromStringExpression => (@string) => @string.IsNotNullOrEmpty() ? null : @string.FromYaml<ObjectValue>().GetObject();
    }
}
