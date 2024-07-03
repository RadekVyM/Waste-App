using Microsoft.Maui.Controls.Shapes;
using SimpleToolkit.Core;

namespace WasteApp.Maui.Extensions;

public static class MarkupExtensions
{
    public static T Children<T>(this T layout, IEnumerable<IView> children) where T : Microsoft.Maui.ILayout
    {
        layout.Clear();
        foreach (var child in children)
            layout.Add(child);
        return layout;
    }

    public static T Background<T>(this T view, ThemeColor themeColor) where T : VisualElement =>
        view.AppThemeBinding(View.BackgroundProperty, themeColor.Light, themeColor.Dark);
    
    public static T Fill<T>(this T view, ThemeColor themeColor) where T : Shape =>
        view.AppThemeBinding(Shape.FillProperty, themeColor.Light, themeColor.Dark);
    
    public static Label TextColor(this Label label, ThemeColor themeColor) =>
        label.AppThemeBinding(Label.TextColorProperty, themeColor.Light, themeColor.Dark);

    public static Button TextColor(this Button button, ThemeColor themeColor) =>
        button.AppThemeBinding(Button.TextColorProperty, themeColor.Light, themeColor.Dark);

    public static Icon TintColor(this Icon label, ThemeColor themeColor) =>
        label.AppThemeBinding(Icon.TintColorProperty, themeColor.Light, themeColor.Dark);

    public static T BindingContext<T>(this T bindable, object bindingContext) where T : BindableObject
    {
        bindable.BindingContext = bindingContext;
        return bindable;
    }
}