using SimpleToolkit.Core;

namespace WasteApp.Maui.Views.Controls;

public class StyledContentButton : ContentButton
{
    const string PressedAnimationKey = "PressedAnimationKey";
    const string ReleasedAnimationKey = "PressedAnimationKey";
    const double PressedOpacity = 0.6;
    const uint AnimationLength = 150;

    public IView AnimatableContent { get; set; }


    public StyledContentButton() : base()
    {
        StrokeThickness = 0;
    }


    public override async void OnReleased(Point releasePosition)
    {
        base.OnReleased(releasePosition);
        
        if ((AnimatableContent ?? Content) is not View viewContent)
            return;

        var animation = new Animation((v) => viewContent.Opacity = v, PressedOpacity, 1);
        
        await Task.Delay((int)AnimationLength);
        animation.Commit(this, ReleasedAnimationKey, length: AnimationLength, finished: (d, cancelled) => viewContent.Opacity = 1);
    }

    public override void OnPressed(Point pressPosition)
    {
        base.OnPressed(pressPosition);

        if ((AnimatableContent ?? Content) is not View viewContent)
            return;

        var animation = new Animation((v) => viewContent.Opacity = v, 1, PressedOpacity);
        
        animation.Commit(this, PressedAnimationKey, length: AnimationLength);
    }
}