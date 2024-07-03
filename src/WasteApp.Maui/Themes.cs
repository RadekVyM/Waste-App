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