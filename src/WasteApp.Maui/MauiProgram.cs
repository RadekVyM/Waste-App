using WasteApp.Core.Interfaces.Services;
using WasteApp.Core.Interfaces.ViewModels;
using WasteApp.Core.Services;
using WasteApp.Core.ViewModels;
using WasteApp.Maui.Services;
using WasteApp.Maui.Views.Pages;
using Microsoft.Extensions.Logging;
using SimpleToolkit.Core;
using SimpleToolkit.SimpleShell;
using WasteApp.Core;
using CommunityToolkit.Maui;
using Browser = WasteApp.Maui.Services.Browser;

namespace WasteApp.Maui;

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();
        builder
            .UseMauiApp<App>()
            .UseMauiCommunityToolkitCamera()
            .ConfigureFonts(fonts =>
            {
                fonts.AddFont("MADE Tommy Soft Regular.otf", "Regular");
                fonts.AddFont("MADE Tommy Soft Medium.otf", "Medium");
            });

        builder.UseSimpleShell();
        builder.UseSimpleToolkit();

#if DEBUG
        builder.Logging.AddDebug();
#endif

#if IOS || ANDROID
        builder.DisplayContentBehindBars();
#endif
#if ANDROID
        builder.SetDefaultStatusBarAppearance(Colors.Transparent, false);
#endif

        builder.Services.AddTransient<AppShell>();

        builder.Services.AddTransient<HomePage>();
        builder.Services.AddTransient<CalendarPage>();
        builder.Services.AddTransient<MaterialDetailPage>();
        builder.Services.AddTransient<CameraPage>();

        builder.Services.AddSingleton<INavigationService, NavigationService>();
        builder.Services.AddSingleton<IItemsService, ItemsService>();
        builder.Services.AddSingleton<IMaterialsService, MaterialsService>();
        builder.Services.AddSingleton<WasteApp.Core.Interfaces.Services.IBrowser, Browser>();

        builder.Services.AddTransient<IHomePageViewModel, HomePageViewModel>();
        builder.Services.AddTransient<ICameraPageViewModel, CameraPageViewModel>();
        builder.Services.AddTransient<IMaterialDetailPageViewModel, MaterialDetailPageViewModel>();

        return builder.Build();
    }
}