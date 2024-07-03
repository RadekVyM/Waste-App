using WasteApp.Core.Interfaces.Services;
using WasteApp.Core.Interfaces.ViewModels;
using WasteApp.Core.ViewModels;
using WasteApp.Maui.Views.Controls;

namespace WasteApp.Maui.Views.Pages;

public class HomePage : BaseRootContentPage
{
    IHomePageViewModel ViewModel => BindingContext as IHomePageViewModel;


    public HomePage(IHomePageViewModel viewModel, INavigationService navigationService) : base(navigationService)
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
                    new StyledLabel()
                        .Text("Home Page")
                        .Center(),
                    new Button()
                        .Text("Detail")
                        .Center()
                        .Assign(out Button button)
                ])
        };

        button.Clicked += async (s, e) => await navigationService.GoTo(PageType.MaterialDetailPage, new MaterialDetailPageParameters(null));
    }
}