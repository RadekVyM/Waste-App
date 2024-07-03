using WasteApp.Core.Interfaces.Services;

namespace WasteApp.Maui.Views.Pages;

public class BaseRootContentPage(INavigationService navigationService) : BaseContentPage(navigationService)
{
    protected override void OnSafeAreaChanged(Thickness safeArea)
    {
        Padding = new Thickness(0, safeArea.Top, 0, 0);
    }
}