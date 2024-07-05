using IBrowser = WasteApp.Core.Interfaces.Services.IBrowser;

namespace WasteApp.Maui.Services;

public class Browser : IBrowser
{
    public async Task OpenAsync(string uri)
    {
        await Microsoft.Maui.ApplicationModel.Browser.Default.OpenAsync(uri);
    }
}