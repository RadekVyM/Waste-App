using Maui.BindableProperty.Generator.Core;
using Microsoft.Maui.Controls.Shapes;
using SimpleToolkit.Core;
using Path = Microsoft.Maui.Controls.Shapes.Path;

namespace WasteApp.Maui.Views.Controls;

public partial class TabBar : Grid
{
    public const double TabBarHeight = 80;
    const double CornerRadius = 50;

    [AutoBindable]
    readonly ShellContent currentItem;
    [AutoBindable]
    readonly Thickness innerPadding;

    Grid innerGrid;
    Path backgroundPath;


    public TabBar(ShellContent first, ShellContent second, EventHandler itemClicked, EventHandler cameraClicked) : base()
    {
        HeightRequest = CornerRadius + TabBarHeight;
        RowDefinitions = Rows.Define(CornerRadius, Auto);

        Add(new Path()
            .RowSpan(2)
            .Fill(Themes.Primary)
            .Assign(out backgroundPath));
        Add(new Grid
            {
                ColumnDefinitions = Columns.Define(Star, Star, Star)
            }
            .Row(1)
            .Children([
                RenderTabItem(first, itemClicked).Column(0),
                RenderTabItem("leaf.png").Column(1).Assign(out TabItem camera),
                RenderTabItem(second, itemClicked).Column(2)
            ])
            .Height(TabBarHeight)
            .Assign(out innerGrid));

        camera.Clicked += cameraClicked;
        SizeChanged += OnSizeChanged;
    }


    TabItem RenderTabItem(ShellContent shellContent, EventHandler itemClicked)
    {
        var tabItem = RenderTabItem(shellContent.Icon).BindingContext(shellContent);
        tabItem.Clicked += itemClicked;
        return tabItem;
    }

    TabItem RenderTabItem(ImageSource icon) => new (icon);

    partial void OnInnerPaddingChanged(Thickness value)
    {
        HeightRequest = CornerRadius + TabBarHeight + value.Bottom;
        innerGrid.Margin(new Thickness(20 + value.Left, 0, 20 + value.Right, value.Bottom));
    }

    partial void OnCurrentItemChanged(ShellContent value)
    {
        foreach (var child in innerGrid.Children)
        {
            if (child is TabItem tabItem)
                tabItem.IsSelected = tabItem.BindingContext == value;
        }
    }

    private void OnSizeChanged(object sender, EventArgs e)
    {
        backgroundPath.Data = CreatePath();
    }

    private PathGeometry CreatePath() =>
        new()
        {
            Figures =
                [
                    new PathFigure
                    {
                        StartPoint = new Point(-1, 0), IsClosed = true, IsFilled = true,
                        Segments =
                        [
                            new QuadraticBezierSegment(new Point(-1, CornerRadius), new Point(CornerRadius, CornerRadius)),
                            new LineSegment(new Point(Width - CornerRadius + 1, CornerRadius)),
                            new QuadraticBezierSegment(new Point(Width + 1, CornerRadius), new Point(Width + 1, 0)),
                            new LineSegment(new Point(Width + 1, Height)),
                            new LineSegment(new Point(-1, Height))
                        ]
                    }
                ]
        };
}

partial class TabItem : ContentButton
{
    [AutoBindable(DefaultValue = "false")]
    readonly bool isSelected;

    RoundRectangle roundRectangle;

    public TabItem(ImageSource icon) : base()
    {
        this.StrokeThickness = 0;

        Content = new Grid
        {
            RowDefinitions = Rows.Define(8, Star),
            RowSpacing = 8
        }
            .Children([
                new RoundRectangle()
                {
                    CornerRadius = 4,
                    IsVisible = false
                }
                    .Fill(Themes.OnPrimary)
                    .Center()
                    .Size(8)
                    .Assign(out roundRectangle),
                new Icon()
                    .Row(1)
                    .Source(icon)
                    .Size(25)
                    .TintColor(Themes.OnPrimary)
                    .Center()
            ])
            .Center();
    }

    partial void OnIsSelectedChanged(bool value)
    {
        roundRectangle.IsVisible = value;
    }
}