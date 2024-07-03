using WasteApp.Core.Interfaces.Services;
using WasteApp.Maui.Views.Pages;
using SimpleToolkit.Core;
using SimpleToolkit.SimpleShell;
using TabBar = WasteApp.Maui.Views.Controls.TabBar;
using Maui.BindableProperty.Generator.Core;
using ShellItem = Microsoft.Maui.Controls.ShellItem;

namespace WasteApp.Maui;

public partial class AppShell : SimpleShell
{
    const string TabBarAnimation = "TabBarAnimation";

    private readonly INavigationService navigationService;
    [AutoBindable]
    readonly bool isTabBarHidden = false;

    TabBar tabBar;


    public AppShell(INavigationService navigationService)
	{
        this.navigationService = navigationService;

        Loaded += AppShellLoaded;

        Routing.RegisterRoute(PageType.MaterialDetailPage.ToString(), typeof(MaterialDetailPage));
        Routing.RegisterRoute(PageType.CameraPage.ToString(), typeof(CameraPage));

        var firstTab = new ShellContent
        {
            Title = "Home",
            Icon = "house.png",
            Route = PageType.HomePage.ToString(),
            ContentTemplate = new DataTemplate(typeof(HomePage))
        };
        var secondTab = new ShellContent
        {
            Title = "Calendar",
            Icon = "calendar.png",
            Route = PageType.CalendarPage.ToString(),
            ContentTemplate = new DataTemplate(typeof(CalendarPage))
        };

        var shellItem = new ShellItem();
        shellItem.Items.Add(firstTab);
        shellItem.Items.Add(secondTab);

        Items.Add(shellItem);

        Background = Color.FromArgb("#f8f5fb");
        Content = RenderContent(firstTab, secondTab);
    }


    Grid RenderContent(ShellContent first, ShellContent second)
    {
        const double cornerRadius = 35;

        return new Grid()
        {
            RowDefinitions = Rows.Define(Star, Auto),
            Children =
            {
                new SimpleNavigationHost()
                    .RowSpan(2),
                new TabBar(first, second, ItemClicked, async (s, e) => await navigationService.GoTo(PageType.CameraPage))
                    .Row(1)
                    .Bind(TabBar.CurrentItemProperty, source: this, getter: (AppShell shell) => shell.CurrentShellContent)
                    .Assign(out tabBar)
            }
        }
            .Background(Themes.Primary);
    }

    partial void OnIsTabBarHiddenChanged(bool value)
    {
        tabBar.AbortAnimation(TabBarAnimation);

        var height = tabBar.Height;
        var animation = new Animation(
            (value) => tabBar.TranslationY = value,
            tabBar.TranslationY,
            value ? height : 0);

        tabBar.IsVisible(true);
        animation.Commit(tabBar, TabBarAnimation, easing: Easing.CubicInOut, finished: (d, cancelled) => {
            if (!cancelled)
                tabBar.IsVisible(!value);
        });
    }

    private static void AppShellLoaded(object sender, EventArgs e)
    {
        var shell = sender as AppShell;

        shell.Window.SubscribeToSafeAreaChanges(safeArea =>
        {
            shell.tabBar.InnerPadding = new Thickness(safeArea.Left, 0, safeArea.Right, safeArea.Bottom);
        });
    }

    private async void ItemClicked(object sender, EventArgs e)
    {
        var button = sender as View;
        var shellItem = button.BindingContext as BaseShellItem;

        await navigationService.GoTo((PageType)Enum.Parse(typeof(PageType), shellItem.Route));
    }
}