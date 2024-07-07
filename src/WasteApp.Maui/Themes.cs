using Microsoft.Maui.Controls.Shapes;

namespace WasteApp.Maui;

public static class Themes
{
    public static ThemeColor Primary => new(Color.FromArgb("#573753"), Color.FromArgb("#573753"));
    public static ThemeColor OnPrimary => new(Colors.White, Colors.White);
    public static ThemeColor Surface => new(Color.FromArgb("#f8f5fb"), Color.FromArgb("#f8f5fb"));
    public static ThemeColor OnSurface => Primary;
    public static ThemeColor SurfaceContainer => new(Colors.White, Colors.White);
    public static ThemeColor OnSurfaceContainer => Primary;
    public static ThemeColor Shadow => Primary;
}

public record ThemeColor(Color Light, Color Dark);

public static class Shapes
{
    public static RoundRectangle RoundedSmall => new() { CornerRadius = 10 };

    public static RoundRectangle RoundedLarge => new() { CornerRadius = 16 };
}

public static class Shadows
{
    public static Shadow Small => new Shadow()
    {
        Opacity = 0.15f,
#if ANDROID
        Radius = 40,
#endif
    }
        .Brush(Themes.Shadow);

    public static Shadow Large => new Shadow()
    {
        Opacity = 0.25f,
#if ANDROID
        Radius = 50,
#endif
    }
        .Brush(Themes.Shadow);
}