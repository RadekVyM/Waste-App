using System.ComponentModel;

namespace WasteApp.Core.Interfaces.ViewModels;

public interface IBaseViewModel : INotifyPropertyChanged
{
    void OnPropertyChanged(string propertyName);
}
