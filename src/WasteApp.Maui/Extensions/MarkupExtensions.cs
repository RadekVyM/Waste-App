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

    public static T Content<T>(this T contentView, View content) where T : IContentView
    {
        if (contentView is Border border)
            border.Content = content;
        if (contentView is ContentView realContentView)
            realContentView.Content = content;
        if (contentView is ScrollView scrollView)
            scrollView.Content = content;
        if (contentView is ContentPage page)
            page.Content = content;
        return contentView;
    }

    public static T Background<T>(this T view, ThemeColor themeColor) where T : VisualElement =>
        view.AppThemeBinding(View.BackgroundProperty, themeColor.Light, themeColor.Dark);
    
    public static T Fill<T>(this T view, ThemeColor themeColor) where T : Shape =>
        view.AppThemeBinding(Shape.FillProperty, themeColor.Light, themeColor.Dark);
    
    public static T TextColor<T>(this T text, ThemeColor themeColor) where T : BindableObject, ITextStyle =>
        text.AppThemeBinding(Label.TextColorProperty, themeColor.Light, themeColor.Dark);

    public static Icon TintColor(this Icon label, ThemeColor themeColor) =>
        label.AppThemeBinding(Icon.TintColorProperty, themeColor.Light, themeColor.Dark);

    public static Shadow Brush(this Shadow shadow, ThemeColor themeColor) =>
        shadow.AppThemeBinding(Shadow.BrushProperty, themeColor.Light, themeColor.Dark);

    public static T BindingContext<T>(this T bindable, object bindingContext) where T : BindableObject
    {
        bindable.BindingContext = bindingContext;
        return bindable;
    }
}