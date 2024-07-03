namespace WasteApp.Maui.Views.Controls;

public class StyledLabel : Label
{
    public StyledLabel() : base()
    {
        this.TextColor(Themes.OnSurface);
        FontFamily = "Regular";
    }
}

public enum LabelType
{
    Body
}