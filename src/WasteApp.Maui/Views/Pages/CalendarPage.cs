using WasteApp.Core.Interfaces.Services;
using WasteApp.Maui.Views.Controls;

namespace WasteApp.Maui.Views.Pages;

public class CalendarPage : BaseRootContentPage<object>
{
    public CalendarPage(INavigationService navigationService) : base(null, navigationService)
    {
        Content = new Grid
        {
            new StyledLabel()
                .Text("Calendar Page")
                .Center()
        };
    }
}