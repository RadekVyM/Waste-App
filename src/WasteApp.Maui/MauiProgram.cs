using WasteApp.Core.Interfaces.Services;
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
using Microsoft.Maui.Handlers;
using CommunityToolkit.Maui.Views;

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
            })
            .ConfigureMauiHandlers((IMauiHandlersCollection h) => h.AddHandler<CameraView, CustomCameraViewHandler>());

        builder.UseSimpleShell();
        builder.UseSimpleToolkit();

#if DEBUG
        builder.Logging.AddDebug();
#endif

        RemoveEntryBorder();

#if IOS || ANDROID
        builder.DisplayContentBehindBars();
#endif
#if ANDROID
        builder.SetDefaultStatusBarAppearance(Colors.Transparent, false);
        builder.SetDefaultNavigationBarAppearance(Colors.Transparent);
#endif

        RegisterServices(builder);

        return builder.Build();
    }

    private static void RegisterServices(MauiAppBuilder builder)
    {
        builder.Services.AddTransient<AppShell>();

        builder.Services.AddTransient<HomePage>();
        builder.Services.AddTransient<CalendarPage>();
        builder.Services.AddTransient<MaterialDetailPage>();
        builder.Services.AddTransient<CameraPage>();

        builder.Services.AddSingleton<INavigationService, NavigationService>();
        builder.Services.AddSingleton<IItemsService, ItemsService>();
        builder.Services.AddSingleton<IMaterialsService, MaterialsService>();
        builder.Services.AddSingleton<WasteApp.Core.Interfaces.Services.IBrowser, Browser>();

        builder.Services.AddTransient<HomePageViewModel>();
        builder.Services.AddTransient<CameraPageViewModel>();
        builder.Services.AddTransient<MaterialDetailPageViewModel>();
    }

    private static void RemoveEntryBorder()
    {
        EntryHandler.Mapper.AppendToMapping("RemoveEntryBorder", (handler, entry) =>
        {
#if ANDROID
            handler.PlatformView.Background = null;
            handler.PlatformView.SetBackgroundColor(Android.Graphics.Color.Transparent);
#elif IOS || MACCATALYST
            handler.PlatformView.BackgroundColor = UIKit.UIColor.Clear;
            handler.PlatformView.Layer.BorderWidth = 0;
            handler.PlatformView.BorderStyle = UIKit.UITextBorderStyle.None;
#elif WINDOWS
            handler.PlatformView.BorderThickness = new Microsoft.UI.Xaml.Thickness(0);
            handler.PlatformView.Background = null;
            handler.PlatformView.FocusVisualMargin = new Microsoft.UI.Xaml.Thickness(0);
#endif
        });
    }
}