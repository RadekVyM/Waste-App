using System.Threading.Tasks;
using WasteApp.Core;

namespace WasteApp
{
    public class Browser : IBrowser
    {
        public async Task OpenAsync(string uri)
        {
            await Xamarin.Essentials.Browser.OpenAsync(uri, Xamarin.Essentials.BrowserLaunchMode.SystemPreferred);
        }
    }
}
