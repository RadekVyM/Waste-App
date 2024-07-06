using WasteApp.Core.Interfaces.Services;

namespace WasteApp.Maui.Views.Pages;

public class BaseRootContentPage<TViewModel>(TViewModel viewModel, INavigationService navigationService) :
    BaseContentPage<TViewModel>(viewModel, navigationService) where TViewModel : class
{
    protected override void OnSafeAreaChanged(Thickness safeArea)
    {
        Padding = new Thickness(0, safeArea.Top, 0, 0);
    }
}