using System.Linq;
using System.Threading.Tasks;
using WasteApp.Core;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace WasteApp
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();

            Routing.RegisterRoute(PageEnum.MaterialDetailPage.ToString(), typeof(MaterialDetailPage));
            Routing.RegisterRoute(PageEnum.CameraPage.ToString(), typeof(CameraPage));

            tabBar.Items.Add(new ShellContent { Route = PageEnum.HomePage.ToString(), ContentTemplate = new DataTemplate(typeof(HomePage)) });
            tabBar.Items.Add(new ShellContent { Route = PageEnum.CalendarPage.ToString(), ContentTemplate = new DataTemplate(typeof(CalendarPage)) });
        }

        protected override async void OnNavigated(ShellNavigatedEventArgs args)
        {
            base.OnNavigated(args);

            UpdateStatusBarColour();
            await UpdateTabBarVisibility();
        }

        public bool UpdateStatusBarColour()
        {
            var page = Shell.Current.GetCurrentPage();

            bool darkText = page == PageEnum.HomePage || page == PageEnum.CalendarPage;

            DependencyService.Get<IStatusBarService>().SetLightStatusBar(darkText);

            return darkText;
        }

        public async Task<bool> UpdateTabBarVisibility()
        {
            var page = Shell.Current.GetCurrentPage();

            bool isHidden = page == PageEnum.CameraPage;

            CustomTabBar tabBar = Items.FirstOrDefault() as CustomTabBar;

            if (isHidden)
                await tabBar?.TabBarView.HideTabBarAsync();
            else
                await tabBar?.TabBarView.ShowTabBarAsync();

            await tabBar.TabBarView.UpdateIndicators(page);

            return isHidden;
        }
    }
}