using Maui.BindableProperty.Generator.Core;
using SimpleToolkit.Core;
using WasteApp.Core.Models;
using WasteApp.Core.ViewModels;

namespace WasteApp.Maui.Views.Controls;

partial class WasteProcessingSelection : ScrollView
{
    [AutoBindable(DefaultBindingMode = "TwoWay")]
    WasteProcessingEnum selectedWasteProcessing;

    HorizontalStackLayout stackLayout;


    public WasteProcessingSelection(IEnumerable<WasteProcessingViewModel> wasteProcessings) : base()
    {
        HorizontalScrollBarVisibility = ScrollBarVisibility.Never;
        Orientation = ScrollOrientation.Horizontal;
        Content = new HorizontalStackLayout
        {
            Spacing = 15
        }
            .Children(wasteProcessings.Select(RenderCard))
            .Assign(out stackLayout);

        OnSelectedWasteProcessingChanged(selectedWasteProcessing);
    }


    WasteProcessingCard RenderCard(WasteProcessingViewModel wasteProcessing)
    {
        var card = new WasteProcessingCard(wasteProcessing);

        card.Clicked += (s, e) =>
            SelectedWasteProcessing = wasteProcessing.Enum;

        return card;
    }

    partial void OnSelectedWasteProcessingChanged(WasteProcessingEnum value)
    {
        foreach (var card in stackLayout.Cast<WasteProcessingCard>())
        {
            var isSelected = (card.BindingContext as WasteProcessingViewModel).Enum == value;
            card.IsSelected = isSelected;

            if (card.IsSelected)
                ScrollToCard(card);
        }
    }

    private async void ScrollToCard(WasteProcessingCard card)
    {
        await ScrollToAsync(card, ScrollToPosition.Start, false);
    }
}

partial class WasteProcessingCard : StyledContentButton
{
    [AutoBindable]
    bool isSelected;

    Border border;
    Icon icon;
    Label label;


    public WasteProcessingCard(WasteProcessingViewModel wasteProcessing) : base()
    {
        StrokeThickness = 0;

        this
            .BindingContext(wasteProcessing)
            .Content(
                new VerticalStackLayout
                {
                    Spacing = 10
                }
                    .Children([
                        new Border
                        {
                            StrokeThickness = 0,
                            StrokeShape = Shapes.RoundedSmall,
                        }
                            .Assign(out border)
                            .Background(Color.FromArgb(wasteProcessing.Color))
                            .Content(
                                new Icon()
                                    .Source(wasteProcessing.Icon)
                                    .TintColor(Themes.Primary)
                                    .Assign(out icon)),
                        new StyledLabel()
                            .Text(wasteProcessing.Title)
                            .Center()
                            .Assign(out label)
                    ]));

        AnimatableContent = icon;

        OnIsSelectedChanged(false);
    }


    partial void OnIsSelectedChanged(bool value)
    {
        if (value)
        {
            border.Size(110, 80);
            icon.Size(32);
            label.FontSize(17);
        }
        else
        {
            border.Size(80, 55);
            icon.Size(25);
            label.FontSize(14);
        }
    }
}