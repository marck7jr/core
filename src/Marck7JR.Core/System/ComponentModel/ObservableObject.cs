using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;

namespace System.ComponentModel
{
    public abstract class ObservableObject : INotifyPropertyChanged, ISerializable
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        protected virtual bool AreEquals<T>(ref T field, T value, [CallerMemberName] string? propertyName = null) => EqualityComparer<T>.Default.Equals(field, value);
        protected virtual T GetValue<T>(ref T field, [CallerMemberName] string? propertyName = null) => field;
        protected virtual bool SetValue<T>(ref T field, T value, [CallerMemberName] string? propertyName = null)
        {
            if (AreEquals(ref field, value) is bool boolean && !boolean)
            {
                field = value;
                OnPropertyChanged(propertyName!);
            }

            return !boolean;
        }

        protected virtual void OnPropertyChanged(string propertyName)
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
