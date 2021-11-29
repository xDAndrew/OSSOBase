using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Client.BindingContexts
{
    public class BaseBindingContext : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
