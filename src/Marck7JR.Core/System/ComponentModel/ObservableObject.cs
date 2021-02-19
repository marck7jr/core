using Marck7JR.Core.Extensions;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using System.Threading;
using System.Threading.Tasks;

namespace System.ComponentModel
{
    public abstract class ObservableObject : IObservableObject, ISerializable
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        public ObservableObject()
        {
            PropertyChanged += (sender, args) =>
            {
                if (args.PropertyName.IsNotNullOrEmpty() && KeyValuePairs is not null && KeyValuePairs.TryGetValue(args.PropertyName, out Action? action))
                {
                    action?.Invoke();
                }
            };
        }

        protected Dictionary<string, Action>? KeyValuePairs { get; private set; }

        protected virtual bool AreEquals<T>(ref T field, T value, [CallerMemberName] string? propertyName = null) => EqualityComparer<T>.Default.Equals(field, value);

        public virtual void InitializeComponent()
        {
            var actions = KeyValuePairs?.Select(keyValuePair => keyValuePair.Value);

            if (actions is not null)
            {
                foreach (var action in actions)
                {
                    action.Invoke();
                }
            }
        }

        public virtual async ValueTask<bool> InitializeComponentAsync(CancellationToken cancellationToken = default)
        {
            try
            {
                var task = Task.Run(() => InitializeComponent(), cancellationToken).ContinueWith(task =>
                {
                    task.ConfigureAwait(true).GetAwaiter().OnCompleted(() =>
                    {

                    });

                    task.Wait(cancellationToken);
                });

                await task;

                return true;
            }
            catch (OperationCanceledException operationCanceledException)
            {
                Debug.WriteLine(operationCanceledException);
            }

            return false;
        }

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

            KeyValuePairs[propertyName!] = action;

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
