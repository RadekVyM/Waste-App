using Microsoft.Maui.Controls.Shapes;
using SimpleToolkit.Core;
using WasteApp.Core.Interfaces.Services;
using WasteApp.Core.Models;
using WasteApp.Core.ViewModels;
using WasteApp.Maui.Views.Controls;

namespace WasteApp.Maui.Views.Pages;

public class HomePage : BaseRootContentPage
{
    const double SidePadding = 25;
    const double BottomPadding = 25;

    HomePageViewModel ViewModel => BindingContext as HomePageViewModel;

    ContentView footerView;


    public HomePage(HomePageViewModel viewModel, INavigationService navigationService) : base(navigationService)
    {
        BindingContext = viewModel;

        Content = new Grid
        {
            RowDefinitions = Rows.Define(Auto, Star),
        }
            .Children([
                RenderTopBar()
                    .Row(0)
                    .Margin(new Thickness(SidePadding, 10, SidePadding, 20)),
                new CollectionView
                {
                    Header = new VerticalStackLayout()
                        .Paddings(top: 20, bottom: 10)
                        .Children([
                            RenderWasteProcessingSelection()
                                .Padding(new Thickness(SidePadding, 0))
                                .Margins(bottom: 10),
                            RenderMaterials()
                                .Margins(bottom: 25),
                            new StyledLabel()
                                .Text("Popular items")
                                .Font("Medium", size: 17)
                                .Padding(new Thickness(SidePadding, 0, SidePadding, 0)),
                            new ContentView()
                                .Background(Themes.Primary)
                                .Size(50, 3)
                                .Start()
                                .Margins(left: SidePadding, top: 2.5)
                        ]),
                    Footer = new ContentView()
                        .Assign(out footerView)
                }
                    .Row(1)
                    .ItemsSource(viewModel.PopularItems)
                    .ItemTemplate(new DataTemplate(RenderPopularItemCard))
            ]);

        footerView.HeightRequest = Controls.TabBar.TabBarHeight + BottomPadding;
    }


    protected override void OnSafeAreaChanged(Thickness safeArea)
    {
        base.OnSafeAreaChanged(safeArea);
        footerView.HeightRequest = Controls.TabBar.TabBarHeight + BottomPadding + safeArea.Bottom;
    }

    Grid RenderTopBar()
    {
        const double AvatarSize = 50;

        var grid = new Grid
        {
            ColumnDefinitions = Columns.Define(Star, AvatarSize),
            ColumnSpacing = 30
        }
            .Children([
                new Border
                {
                    StrokeThickness = 0,
                    StrokeShape = Shapes.RoundedSmall,
                    Shadow = Shadows.Large
                }
                    .Background(Themes.SurfaceContainer)
                    .CenterVertical()
                    .Content(
                        new Grid
                        {
                            ColumnDefinitions = Columns.Define(20, Star),
                            ColumnSpacing = 10
                        }
                            .Padding(new Thickness(15, 2, 15, 0))
                            .Children([
                                new Icon()
                                    .Source("search_icon.png")
                                    .Center()
                                    .TintColor(Themes.OnSurfaceContainer)
                                    .Size(20),
                                new Entry()
                                    .Column(1)
                                    .TextCenterVertical()
                                    .TextColor(Themes.OnSurfaceContainer)
                                    .Placeholder("Search for an item")
                                    .Font("Regular")
                                    .Size(-1, 42)
                            ])),
                new Border
                {
                    StrokeThickness = 0,
                    StrokeShape = new RoundRectangle { CornerRadius = AvatarSize / 2 }
                }
                    .Column(1)
                    .Size(AvatarSize)
                    .Content(
                        new Image()
                            .Source("logo.jpg")
                            .Size(AvatarSize))
            ]);

        return grid;
    }

    WasteProcessingSelection RenderWasteProcessingSelection()
    {
        return new WasteProcessingSelection(ViewModel.WasteProcessings)
            .Bind(
                WasteProcessingSelection.SelectedWasteProcessingProperty,
                source: ViewModel,
                getter: (HomePageViewModel vm) => vm.SelectedWasteProcessing,
                setter: (HomePageViewModel vm, WasteProcessingEnum value) => vm.SelectedWasteProcessing = value,
                mode: BindingMode.TwoWay);
    }

    ScrollView RenderMaterials()
    {
        return new ScrollView
        {
            HorizontalScrollBarVisibility = ScrollBarVisibility.Never,
            Orientation = ScrollOrientation.Horizontal,
            Content = new HorizontalStackLayout
            {
                Spacing = 15
            }
                .Padding(new Thickness(SidePadding, 15, SidePadding, 25))
                .Bind(BindableLayout.ItemsSourceProperty, source: ViewModel, getter: (HomePageViewModel vm) => vm.Materials)
                .ItemTemplate(new DataTemplate(RenderMaterialCard))
        };
    }

    StyledContentButton RenderMaterialCard()
    {
        return new StyledContentButton
        {
            StrokeThickness = 0,
            StrokeShape = Shapes.RoundedLarge,
            Shadow = Shadows.Large,
            Command = ViewModel.MaterialCommand
        }
            .Bind(ContentButton.CommandParameterProperty)
            .Background(Themes.SurfaceContainer)
            .Padding(new Thickness(25, 20, 25, 16))
            .Content(new VerticalStackLayout
            {
                Spacing = 12
            }
                .Children([
                    new Image()
                        .Bind(Image.SourceProperty, getter: (Material material) => material.Image)
                        .Size(90),
                    new StyledLabel()
                        .Bind(Label.TextProperty, getter: (Material material) => material.Name)
                        .TextColor(Themes.OnSurfaceContainer)
                        .Center()
                ]));
    }

    static ContentView RenderPopularItemCard()
    {
        return new ContentView()
            .Padding(new Thickness(SidePadding, 8))
            .Content(new StyledContentButton
            {
                StrokeThickness = 0,
                StrokeShape = Shapes.RoundedLarge,
                Shadow = Shadows.Large
            }
                .Background(Themes.SurfaceContainer)
                .Content(new HorizontalStackLayout
                {
                    Spacing = 15
                }
                    .Padding(15, 20)
                    .Children([
                        new Image()
                            .Bind(Image.SourceProperty, getter: (Item item) => item.Image)
                            .Size(50),
                        new StyledLabel()
                            .Bind(Label.TextProperty, getter: (Item item) => item.Name)
                            .TextColor(Themes.OnSurfaceContainer)
                            .Center()
                    ])));
    }
}