using WasteApp.Core.Interfaces.ViewModels;

namespace WasteApp.Core.Interfaces.Services;

public enum PageType
{
    HomePage, MaterialDetailPage, CalendarPage, CameraPage
}

public interface INavigationService
{
    Task GoTo(PageType pageType, IParameters? parameters = null);
    Task GoBack();
}
