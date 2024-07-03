using WasteApp.Core.Interfaces.Services;
using WasteApp.Core.Interfaces.ViewModels;

namespace WasteApp.Maui.Services;

public class NavigationService : INavigationService
{
    private const string ParametersKey = "Parameters";

    private readonly IList<PageType> rootPages = [
        PageType.HomePage,
        PageType.CalendarPage,
    ];


    public NavigationService()
    {
    }


    public async Task GoBack()
    {
        await Shell.Current.GoToAsync("..");
    }

    public async Task GoTo(PageType pageType, IParameters parameters = null)
    {
        if (rootPages.Contains(pageType) && Shell.Current.Navigation.NavigationStack.Count > 1)
        {
            await Shell.Current.Navigation.PopToRootAsync(false);
            await Task.Delay(50);
        }

        await Shell.Current.GoToAsync(GetPageRoute(pageType), true, new Dictionary<string, object>
        {
            [ParametersKey] = parameters
        });
    }

    private string GetPageRoute(PageType pageType)
    {
        if (pageType == PageType.CameraPage)
            return $"///{Shell.Current.CurrentItem.Route}/{pageType}";

        if (rootPages.Contains(pageType))
            return $"///{pageType}";
        return pageType.ToString();
    }
}