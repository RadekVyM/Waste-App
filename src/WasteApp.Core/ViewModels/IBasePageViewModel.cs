using System.ComponentModel;
using System.Windows.Input;

namespace WasteApp.Core
{
    public interface IBasePageViewModel : INotifyPropertyChanged
    {
        ICommand GoBackCommand { get; }

        void OnPagePushing(params object[] parameters);
        void OnPropertyChanged(string property);
    }
}