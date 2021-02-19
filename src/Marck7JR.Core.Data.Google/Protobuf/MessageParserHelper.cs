using System;
using System.Reflection;

namespace Google.Protobuf
{
    public static class MessageParserHelper
    {
        public static MessageParser GetMessageParser(Type type)
        {
            if (type.GetProperty("Parser", BindingFlags.Public | BindingFlags.Static)?.GetValue(null, null) is MessageParser messageParser)
            {
                return messageParser;
            }

            throw new InvalidOperationException($"'{type.Name}' must have a static property \"Parser\" with an instance of '{nameof(MessageParser)}'");
        }
        public static MessageParser GetMessageParser<T>() where T : IMessage => GetMessageParser(typeof(T));
        public static MessageParser GetMessageParser<T>(this T _) where T : IMessage => GetMessageParser(typeof(T));
    }
}
