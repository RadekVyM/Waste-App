using WasteApp.Core.Interfaces.Services;
using WasteApp.Core.Interfaces.ViewModels;

namespace WasteApp.Maui.Views.Pages;

public class MaterialDetailPage : BaseContentPage
{
    IMaterialDetailPageViewModel ViewModel => BindingContext as IMaterialDetailPageViewModel;


    public MaterialDetailPage(IMaterialDetailPageViewModel viewModel, INavigationService navigationService) : base(navigationService)
    {
        Shell.SetPresentationMode(this, PresentationMode.Animated);

        BindingContext = viewModel;

        Content = new Grid
        {
            Children =
            {
                new Button()
                    .Text("Back")
                    .Center()
                    .Assign(out Button backButton)
            }
        };

        backButton.Clicked += (s, e) => navigationService.GoBack();
    }
}