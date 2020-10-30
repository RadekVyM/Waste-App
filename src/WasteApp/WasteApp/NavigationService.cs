using System.Linq;
using WasteApp.Core;
using Xamarin.Forms;

namespace WasteApp
{
    public class NavigationService : INavigationService
    {
        public void Pop()
        {
            Shell.Current.Navigation.PopAsync();
        }

        public void Push(PageEnum page, params object[] parameters)
        {
            Shell.Current.GoToAsync(page.ToString());
            Page lastPage = Shell.Current.Navigation.NavigationStack.LastOrDefault();

            if (lastPage != null)
                ((IBasePageViewModel)lastPage.BindingContext).OnPagePushing(parameters);
        }
    }
}
