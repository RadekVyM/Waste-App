#if IOS || MACCATALYST
global using NativePlatformCameraPreviewView = global::UIKit.UIView;
global using NativePlatformView = global::UIKit.UIView;
#elif ANDROID
global using NativePlatformCameraPreviewView = global::AndroidX.Camera.View.PreviewView;
global using NativePlatformView = global::Android.Views.View;
#elif WINDOWS
global using NativePlatformCameraPreviewView = global::Microsoft.UI.Xaml.FrameworkElement;
global using NativePlatformView = global::Microsoft.UI.Xaml.FrameworkElement;
#elif TIZEN
global using NativePlatformCameraPreviewView = global::Tizen.NUI.BaseComponents.View;
global using NativePlatformView = global::Tizen.NUI.BaseComponents.View;
#else
global using NativePlatformCameraPreviewView = global::Microsoft.Maui.IView;
global using NativePlatformView = global::Microsoft.Maui.IView;
#endif
using CommunityToolkit.Maui.Core.Handlers;

namespace WasteApp.Maui;

public class CustomCameraViewHandler : CameraViewHandler
{
    protected override NativePlatformCameraPreviewView CreatePlatformView()
    {
        var platformView = base.CreatePlatformView();

        // Make camera preview to fill the entire available area
#if ANDROID
        if (platformView is NativePlatformCameraPreviewView cameraPreview)
            cameraPreview.SetScaleType(NativePlatformCameraPreviewView.ScaleType.FillCenter);
#endif

        return platformView;
    }
}