using WasteApp.Core.Interfaces.Services;
using WasteApp.Core.Interfaces.ViewModels;
using WasteApp.Core.ViewModels;

namespace WasteApp.Maui.Views.Pages;

public class CameraPage : BaseContentPage
{
    ICameraPageViewModel ViewModel => BindingContext as ICameraPageViewModel;


    public CameraPage(ICameraPageViewModel viewModel, INavigationService navigationService) : base(navigationService)
    {
        BindingContext = viewModel;

        Content = new Grid
        {
            new VerticalStackLayout()
                {
                    Spacing = 10
                }
                .Center()
                .Children([
                    new Button()
                    .Text("Back")
                    .Center()
                    .Assign(out Button backButton),
                new Button()
                    .Text("Detail")
                    .Center()
                    .Assign(out Button detailButton)
                ])
        };

        backButton.Clicked += (s, e) => navigationService.GoBack();
        detailButton.Clicked += async (s, e) => await navigationService.GoTo(PageType.MaterialDetailPage, new MaterialDetailPageParameters(null));
    }


    protected override void SetTabBarVisibility()
    {
        (AppShell.Current as AppShell).IsTabBarHidden = true;
    }
}