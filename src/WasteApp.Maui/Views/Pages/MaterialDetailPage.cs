using Microsoft.Maui.Controls.Shapes;
using SimpleToolkit.Core;
using WasteApp.Core.Extensions;
using WasteApp.Core.Interfaces.Services;
using WasteApp.Core.Models;
using WasteApp.Core.ViewModels;
using WasteApp.Maui.Views.Controls;
using Path = Microsoft.Maui.Controls.Shapes.Path;

namespace WasteApp.Maui.Views.Pages;

public class MaterialDetailPage : BaseContentPage
{
    readonly PathGeometryConverter pathGeometryConverter = new();

    Border header;
    VerticalStackLayout contentStack;

    MaterialDetailPageViewModel ViewModel => BindingContext as MaterialDetailPageViewModel;


    public MaterialDetailPage(MaterialDetailPageViewModel viewModel, INavigationService navigationService) : base(navigationService)
    {
        Shell.SetPresentationMode(this, PresentationMode.Animated);

        BindingContext = viewModel;

        Content = new Grid
        {
            RowDefinitions = Rows.Define(Auto, Star),
            Children =
            {
                RenderHeader()
                    .Assign(out header),
                RenderMaterialCard()
                    .Bottom()
                    .CenterHorizontal()
                    .TranslationY(25),
                new VerticalStackLayout()
                    .Assign(out contentStack)
                    .Row(1)
                    .Children([
                        new StyledLabel
                        {
                            CharacterSpacing = 1
                        }
                            .CenterHorizontal()
                            .Margins(top: 35)
                            .Font("Medium", 20)
                            .TextColor(Themes.OnSurface)
                            .Bind(
                                StyledLabel.TextProperty,
                                source: ViewModel,
                                getter: (MaterialDetailPageViewModel vm) => vm.Material,
                                convert: (Material material) => material.WasteProcessingEnum.ToAdjective()),
                            new StyledLabel
                            {
                                LineHeight = 1.4
                            }
                                .Margins(25, 35, 25)
                                .FontSize(18)
                                .TextColor(Themes.OnSurface)
                                .Bind(
                                    StyledLabel.TextProperty,
                                    source: ViewModel,
                                    getter: (MaterialDetailPageViewModel vm) => vm.Material,
                                    convert: (Material material) => material.Description)
                    ])
            }
        };
    }


    protected override void OnSafeAreaChanged(Thickness safeArea)
    {
        header.Padding(new Thickness(safeArea.Left, safeArea.Top, safeArea.Right, 0));
        contentStack.Padding(new Thickness(safeArea.Left, 0, safeArea.Right, safeArea.Bottom + Controls.TabBar.TabBarHeight));
    }

    Border RenderMaterialCard()
    {
        return new Border
        {
            StrokeThickness = 0,
            StrokeShape = Shapes.RoundedSmall
        }
            .Background(Colors.Red)
            .Bind(
                Border.BackgroundProperty,
                source: ViewModel,
                getter: (MaterialDetailPageViewModel vm) => vm.Material,
                convert: (Material material) => Color.FromArgb(material.WasteProcessingEnum.ToColor()))
            .Size(80, 55)
            .Content(new Icon()
                .Bind(
                    Icon.SourceProperty,
                    source: ViewModel,
                    getter: (MaterialDetailPageViewModel vm) => vm.Material,
                    convert: (Material material) => material.WasteProcessingEnum.ToIcon())
                .Size(25)
                .TintColor(Themes.Primary));
    }

    Border RenderHeader()
    {
        var border = new Border
        {
            StrokeThickness = 0,
            StrokeShape = new RoundRectangle { CornerRadius = new CornerRadius(0, 0, 50, 50) }
        }
            .Margin(-1)
            .Background(Themes.Primary)
            .Content(new Grid()
                .Size(-1, 200)
                .Children([
                    new Path
                    {
                        Data = CreateBottleGeometry(),
                        Aspect = Stretch.Uniform,
#if ANDROID
                        AnchorX = 0,
                        AnchorY = 0,
#endif
                    }
                        .Start()
                        .Top()
                        .Size(80)
                        .Rotation(80)
                        .Margins(45, 42, 0, 0)
                        .Fill(Themes.OnPrimary),
                    new Path
                    {
                        Data = CreateBottleGeometry(),
                        Aspect = Stretch.Uniform
                    }
                        .End()
                        .Top()
                        .Size(95)
                        .Margins(0, 50, 35, 0)
                        .Fill(Themes.OnPrimary),
                    new StyledLabel
                    {
                        CharacterSpacing = 1
                    }
                        .Bind(Label.TextProperty, source: ViewModel, getter: (MaterialDetailPageViewModel vm) => vm.Material, convert: (Material material) => material.Name)
                        .CenterHorizontal()
                        .Bottom()
                        .Margins(bottom: 70)
                        .Font("Medium", size: 30)
                        .TextColor(Themes.OnPrimary),
                    TopButton("left_arrow_icon.png", 20)
                        .Assign(out ContentButton backButton)
                        .Start()
                        .Top(),
                    TopButton("ellipsis.png", 30)
                        .End()
                        .Top()
                ]));

        backButton.Clicked += (s, e) => navigationService.GoBack();

        return border;

        StyledContentButton TopButton(string icon, double iconWidth) =>
            new StyledContentButton()
                .Padding(10)
                .Margin(15, 2)
                .Content(new Icon()
                    .Source(icon)
                    .TintColor(Themes.OnPrimary)
                    .Size(iconWidth, 20));
    }

    Geometry CreateBottleGeometry()
        => pathGeometryConverter.ConvertFromInvariantString("m -219.30098,164.54726 c -0.97014,-0.38054 -2.50267,-1.23585 -3.40561,-1.90067 -1.03249,-0.76021 -8.1621,-9.16971 -19.2123,-22.66125 -25.55495,-31.20086 -32.25312,-39.31577 -33.43132,-40.502474 -1.79703,-1.81001 -4.30288,-5.97987 -5.00763,-8.33296 -0.48488,-1.61897 -0.90094,-3.28968 -0.88925,-5.95454 0.0185,-4.2279 0.76225,-5.98291 3.16414,-10.048567 0.86308,-1.460934 1.45558,-2.909203 1.45558,-3.557954 0,-1.180789 -1.03576,-2.880134 -10.62854,-17.43792 -3.00455,-4.559652 -7.06028,-10.261173 -9.01272,-12.670046 -2.83693,-3.500125 -3.50602,-4.53724 -3.3314,-5.163814 0.32601,-1.169862 11.93426,-10.38962 13.0735,-10.383527 0.73342,0.0039 1.60569,0.88312 4.63748,4.674305 2.60933,3.262904 6.78756,7.638669 13.87071,14.526489 4.38432,4.083261 7.37342,8.028424 11.59137,10.071453 1.32887,0.09612 3.66815,-0.46055 5.72473,-0.943378 7.10966,-1.669148 11.75194,-0.344966 16.82277,4.118559 1.13292,0.997242 7.7417,8.74813 14.68616,17.224201 6.94447,8.476069 18.87348,23.031029 26.50892,32.344363 9.5506,11.64935 14.19623,17.59383 14.8876,19.05 0.82887,1.74576 1.00442,2.5803 1.00181,4.7625 -0.005,4.39746 -2.04829,7.6748 -6.09349,9.77493 -1.54785,0.80359 -2.29601,0.95775 -4.70816,0.97011 l -2.86786,0.0147 -0.21367,2.0298 c -0.38588,3.66578 -2.71035,6.94551 -6.22143,8.77818 -1.32677,0.69254 -2.63132,1.15645 -5.32493,1.15857 l -2.66169,-0.5125 -0.23518,2.01269 c -0.43257,3.70197 -2.61722,7.017 -5.71036,8.4574 -2.28149,1.06244 -5.90769,1.10561 -8.46923,0.10084 z m 7.40345,-3.47319 c 3.03526,-1.7129 4.09157,-5.80555 2.69763,-10.45191 -0.81397,-2.71317 -0.58999,-3.49806 0.75564,-2.64797 3.81696,2.4113 4.26016,2.61367 6.04903,2.76202 2.33159,0.19335 4.11352,-0.367 5.75766,-1.81058 2.80752,-2.46503 3.21789,-5.40419 1.3103,-9.38466 -1.02914,-2.14745 -1.16844,-2.68957 -0.78871,-3.06929 0.37972,-0.37973 0.89011,-0.20431 2.88965,0.9932 2.02489,1.21269 2.81551,1.4809 4.75746,1.61394 2.05951,0.14109 2.55288,0.0416 4.26383,-0.86019 3.85191,-2.03013 5.07495,-6.00863 3.07481,-10.00222 -1.0762,-2.1488 -1.35722,-2.4989 -27.96096,-34.834494 -10.48892,-12.74878 -20.06816,-24.449604 -21.28721,-26.001826 -1.21905,-1.552222 -3.38719,-3.977382 -4.81809,-5.38924 -4.70673,-4.644083 -8.2992,-5.570431 -15.20927,-3.921833 -3.46498,0.82667 -6.14041,1.921842 -8.99406,-0.0676 -0.48039,-0.334905 -5.62136,-5.063163 -11.34239,-10.641461 -5.72102,-5.578298 -10.48612,-10.14236 -10.5891,-10.14236 -0.40074,0 -8.16795,6.759164 -8.09458,7.04404 0.0428,0.166278 3.5978,5.674079 7.89995,12.239557 4.30215,6.565479 7.97494,12.487333 8.16176,13.159681 0.55776,2.007281 0.0418,3.997004 -2.10761,8.127822 -2.44526,4.030042 -2.23666,4.536848 -2.44597,7.788909 0.35813,4.521094 1.16139,6.630711 4.23587,10.232481 1.22743,1.43795 12.11511,14.679444 24.19484,29.425554 12.07974,14.74611 23.2991,28.43718 24.93192,30.4246 1.63283,1.98742 3.6105,4.03817 4.39484,4.55722 2.78182,1.84092 5.93866,2.1682 8.2628,0.85661 z") as Geometry;
}