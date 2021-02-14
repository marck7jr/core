using System.Threading;
using System.Threading.Tasks;

namespace System.ComponentModel
{
    public interface IObservableObject : INotifyPropertyChanged
    {
        void InitializeComponent();
        ValueTask<bool> InitializeComponentAsync(CancellationToken cancellationToken = default);
    }
}