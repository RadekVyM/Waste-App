using SimpleToolkit.Core;
using WasteApp.Core.Interfaces.Services;

namespace WasteApp.Maui.Views.Controls;

public class AppBar : Grid
{
    public AppBar(INavigationService navigationService, bool onDarkSurface, bool hideMoreButton = true) : base()
    {
        Add(TopButton("left_arrow_icon.png", 20, onDarkSurface)
            .Assign(out ContentButton backButton)
            .Start()
            .Top());
        
        if (!hideMoreButton)
        {
            Add(TopButton("ellipsis.png", 30, onDarkSurface)
                .End()
                .Top());
        }

        backButton.Clicked += (s, e) => navigationService.GoBack();
    }


    static StyledContentButton TopButton(string icon, double iconWidth, bool onDarkSurface) =>
        new StyledContentButton()
            .InputTransparent(false)
            .Padding(10)
            .Margin(15, 2)
            .Content(new Icon()
                .Source(icon)
                .TintColor(onDarkSurface ? Themes.OnPrimary : Themes.Primary)
                .Size(iconWidth, 20));
}