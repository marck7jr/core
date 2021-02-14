using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace Windows.Storage
{
    public abstract class LocalApplicationDataContainer : ObservableObject
    {
        public LocalApplicationDataContainer() : base()
        {
            try
            {
                ApplicationDataContainer = ApplicationData.Current.LocalSettings.CreateContainer(GetType().Name, ApplicationDataCreateDisposition.Always);
            }
            catch (InvalidOperationException invalidOperationException)
            {
                Debug.WriteLine(invalidOperationException.Message);
            }
        }

        protected ApplicationDataContainer? ApplicationDataContainer { get; private set; }

        protected override bool SetValue<T>(ref T field, T value, [CallerMemberName] string? propertyName = null)
        {
            if (ApplicationDataContainer is not null && !AreEquals(ref field, value))
            {
                ApplicationDataContainer.Values[propertyName] = value;
            }

            return base.SetValue(ref field, value, propertyName);
        }

        protected override bool SetValue<T>(ref T field, T value, Action action, [CallerMemberName] string? propertyName = null)
        {
            if (ApplicationDataContainer is not null && !AreEquals(ref field, value))
            {
                ApplicationDataContainer.Values[propertyName] = value;
            }

            return base.SetValue(ref field, value, action, propertyName);
        }

        protected override T GetValue<T>(ref T field, [CallerMemberName] string? propertyName = null)
        {
            if (ApplicationDataContainer is not null)
            {
                if (ApplicationDataContainer.Values.TryGetValue(propertyName, out var value))
                {
                    field = (T)value;
                }
            }

            return base.GetValue(ref field, propertyName);
        }
    }
}
