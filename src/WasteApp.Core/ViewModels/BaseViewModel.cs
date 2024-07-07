using System.ComponentModel;
using WasteApp.Core.Interfaces.ViewModels;

namespace WasteApp.Core.ViewModels;

public abstract class BaseViewModel : IBaseViewModel
{
    public event PropertyChangedEventHandler? PropertyChanged;

    public void OnPropertyChanged(string propertyName)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}