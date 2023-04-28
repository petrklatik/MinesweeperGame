using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace MinesweeperGame.Services
{
    public abstract class NotifyPropertyChangedBase : INotifyPropertyChanged
    {
        // Event and method for property changed notifications
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
