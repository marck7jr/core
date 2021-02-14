using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;

namespace System.ComponentModel
{
    public abstract class ObservableObject : INotifyPropertyChanged, ISerializable
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        public ObservableObject()
        {
            PropertyChanged += (sender, args) =>
            {
                if (KeyValuePairs is not null && KeyValuePairs.TryGetValue(args.PropertyName, out Action action))
                {
                    action?.Invoke();
                }
            };
        }

        protected Dictionary<string?, Action>? KeyValuePairs { get; private set; }

        protected virtual bool AreEquals<T>(ref T field, T value, [CallerMemberName] string? propertyName = null) => EqualityComparer<T>.Default.Equals(field, value);
        
        protected virtual T GetValue<T>(ref T field, [CallerMemberName] string? propertyName = null) => field;

        protected virtual bool SetValue<T>(ref T field, T value, [CallerMemberName] string? propertyName = null)
        {
            if (AreEquals(ref field, value) is bool boolean && !boolean)
            {
                field = value;
                OnPropertyChanged(propertyName);
            }

            return !boolean;
        }

        protected virtual bool SetValue<T>(ref T field, T value, Action action, [CallerMemberName] string? propertyName = null)
        {
            if (KeyValuePairs is null)
            {
                KeyValuePairs = new();
            }

            KeyValuePairs[propertyName] = action;

            return SetValue(ref field, value, propertyName);
        }

        protected virtual void OnPropertyChanged(string? propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        protected virtual void GetObjectData(SerializationInfo info, StreamingContext context)
        {

        }

        void ISerializable.GetObjectData(SerializationInfo info, StreamingContext context)
        {
            if (info is null)
            {
                throw new ArgumentNullException(nameof(info));
            }

            GetObjectData(info, context);
        }
    }
}
