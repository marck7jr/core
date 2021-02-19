using Google.Protobuf;
using System;
using System.Linq.Expressions;

namespace Microsoft.EntityFrameworkCore.Storage.ValueConversion
{
    public class MessageValueConverter<T> : ValueConverter<T?, byte[]?>
            where T : IMessage
    {
        public MessageValueConverter(ConverterMappingHints? mappingHints = null) : base(ConvertToByteArrayExpression, ConvertFromByteArrayExpression, mappingHints)
        {

        }

        private static Expression<Func<T?, byte[]?>> ConvertToByteArrayExpression => (message) => message != null ? message.ToByteArray() : default;
        private static Expression<Func<byte[]?, T?>> ConvertFromByteArrayExpression => (bytes) => bytes != null ? (T?)MessageParserHelper.GetMessageParser<T>().ParseFrom(bytes) : default;
    }
}
