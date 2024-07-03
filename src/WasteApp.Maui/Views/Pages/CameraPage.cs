using CommunityToolkit.Maui.Views;
using WasteApp.Core.Interfaces.Services;
using WasteApp.Core.Interfaces.ViewModels;
using WasteApp.Core.ViewModels;

namespace WasteApp.Maui.Views.Pages;

public class CameraPage : BaseContentPage
{
    ICameraPageViewModel ViewModel => BindingContext as ICameraPageViewModel;

    CameraView cameraView;


    public CameraPage(ICameraPageViewModel viewModel, INavigationService navigationService) : base(navigationService)
    {
        BindingContext = viewModel;

        Content = new Grid()
            .Children([
                new CameraView()
                    .Fill()
                    .Assign(out cameraView),
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
            ]);

        backButton.Clicked += (s, e) => navigationService.GoBack();
        detailButton.Clicked += async (s, e) => await navigationService.GoTo(PageType.MaterialDetailPage, new MaterialDetailPageParameters(null));
    }


    protected override void OnSafeAreaChanged(Thickness safeArea)
    {
    }

    protected async override void OnAppearing()
    {
        try
        {
            await cameraView.StartCameraPreview(CancellationToken.None);
        }
        catch (Exception exception)
        { }
        base.OnAppearing();
    }

    protected override void OnDisappearing()
    {
        cameraView.StopCameraPreview();
        base.OnDisappearing();
    }

    protected override void SetTabBarVisibility()
    {
        (AppShell.Current as AppShell).IsTabBarHidden = true;
    }
}