using Marck7JR.Core.Extensions;
using System;
using System.Linq.Expressions;

namespace Microsoft.EntityFrameworkCore.Storage.ValueConversion
{
    public class TypeValueConverter : ValueConverter<Type?, string?>
    {
        public TypeValueConverter(ConverterMappingHints? mappingHints = null) : base(ConvertToStringExpression, ConvertoFromStringExpression, mappingHints)
        {

        }

        private static Expression<Func<Type?, string?>> ConvertToStringExpression => (type) => type == null ? default : type.FullName;
        private static Expression<Func<string?, Type?>> ConvertoFromStringExpression => (@string) => @string.IsNotNullOrEmpty() ? Type.GetType(@string) : default;
    }
}
