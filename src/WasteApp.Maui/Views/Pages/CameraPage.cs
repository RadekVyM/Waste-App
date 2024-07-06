using CommunityToolkit.Maui.Views;
using Microsoft.Maui.Controls.Shapes;
using SimpleToolkit.Core;
using WasteApp.Core.Extensions;
using WasteApp.Core.Interfaces.Services;
using WasteApp.Core.Models;
using WasteApp.Core.ViewModels;
using WasteApp.Maui.Views.Controls;

namespace WasteApp.Maui.Views.Pages;

public class CameraPage : BaseContentPage<CameraPageViewModel>
{
    const string ScanAnimationKey = "ScanAnimationKey";

    readonly CameraView cameraView;
    readonly AppBar appBar;
    readonly MaterialCard materialCard;
    readonly ScanDrawable scanDrawable;
    readonly GraphicsView scanGraphicsView;


    public CameraPage(CameraPageViewModel viewModel, INavigationService navigationService) : base(viewModel, navigationService)
    {
        Content = new Grid()
            .Children([
                new CameraView()
                    .Fill()
                    .Assign(out cameraView),
                new GraphicsView
                {
                    Drawable = scanDrawable = new ScanDrawable() 
                }
                    .Assign(out scanGraphicsView)
                    .InputTransparent(true)
                    .Background(Colors.Transparent),
                new MaterialCard()
                    .Assign(out materialCard)
                    .Padding(SidePadding, 20)
                    .Bottom(),
                new AppBar(navigationService, false)
                    .Assign(out appBar)
                    .Top()
            ]);

        this.Background(Colors.Black);

        appBar.SizeChanged += OnSizeChanged;
        materialCard.SizeChanged += OnSizeChanged;
        SizeChanged += OnSizeChanged;
    }

    private void OnSizeChanged(object sender, EventArgs e)
    {
        scanGraphicsView.Margin = new Thickness(
            scanGraphicsView.Margin.Left,
            appBar.Margin.VerticalThickness + appBar.Height,
            scanGraphicsView.Margin.Right,
            materialCard.Margin.VerticalThickness + materialCard.Height);
        scanGraphicsView.Invalidate();
    }

    protected override void OnSafeAreaChanged(Thickness safeArea)
    {
        appBar.Margin = safeArea;
        materialCard.Margin = safeArea;
        scanGraphicsView.Margin = safeArea;
    }

    protected async override void OnAppearing()
    {
        StartScannerAnimation();

        if (cameraView.IsAvailable)
        {
            try
            {
                await cameraView.StartCameraPreview(CancellationToken.None);
            }
            catch (Exception exception)
            {
                // TODO: IDK why this exception is thrown...
                System.Diagnostics.Debug.WriteLine(exception.Message);
            }
        }

        base.OnAppearing();
    }

    protected override void OnDisappearing()
    {
        cameraView.StopCameraPreview();
        base.OnDisappearing();
    }

    protected override void SetTabBarVisibility()
    {
        (AppShell.Current as AppShell).IsTabBarHidden = true;
    }

    void StartScannerAnimation()
    {
        scanGraphicsView.AbortAnimation(ScanAnimationKey);

        var animation = new Animation
        {
            { 0, 0.3, new Animation(UpdateEndPosition, 0, 1, Easing.SinInOut) },
            { 0.3, 0.5, new Animation(UpdateStartPosition, 0, 1, Easing.SinInOut) },
            { 0.5, 0.8, new Animation(UpdateEndPosition, 1, 0, Easing.SinInOut) },
            { 0.8, 1, new Animation(UpdateStartPosition, 1, 0, Easing.SinInOut) }
        };

        animation.Commit(scanGraphicsView, ScanAnimationKey, length: 4000, repeat: () => true);

        void UpdateStartPosition(double position)
        {
            scanDrawable.StartPosition = (float)position;
            scanGraphicsView.Invalidate();
        }
        void UpdateEndPosition(double position)
        {
            scanDrawable.EndPosition = (float)position;
            scanGraphicsView.Invalidate();
        }
    }
}

partial class MaterialCard : Grid
{
    public MaterialCard() : base()
    {
        const double materialCardWidth = 40;

        Add(new StyledContentButton
        {
            StrokeShape = Shapes.RoundedLarge,
            Shadow = Shadows.Large
        }
            .Bind(StyledContentButton.CommandProperty, getter: (CameraPageViewModel vm) => vm.MaterialCommand)
            .Background(Themes.SurfaceContainer)
            .Margins(top: 30)
            .Padding(16)
            .Content(new Grid
            {
                RowDefinitions = Rows.Define(Auto, Star),
                ColumnDefinitions = Columns.Define(materialCardWidth, Star),
                RowSpacing = 12,
                ColumnSpacing = 10
            }
                .Bind(MaterialCard.BindingContextProperty, getter: (CameraPageViewModel vm) => vm.FoundMaterial)
                .Children([
                    new StyledLabel()
                        .Bind(Label.TextProperty, getter: (Material material) => material.Name)
                        .Font("Medium", size: 17)
                        .ColumnSpan(2),
                    new Border
                    {
                        StrokeThickness = 0,
                        StrokeShape = new RoundRectangle { CornerRadius = 8 }
                    }
                        .Row(1)
                        .Size(materialCardWidth, 30)
                        .CenterVertical()
                        .Bind(Border.BackgroundProperty, getter: (Material material) => material.WasteProcessingEnum, convert: (WasteProcessingEnum wp) => Color.FromArgb(wp.ToColor()))
                        .Content(new Icon()
                            .Bind(Icon.SourceProperty, getter: (Material material) => material.WasteProcessingEnum, convert: (WasteProcessingEnum wp) => wp.ToIcon())
                            .Center()
                            .Size(15)
                            .TintColor(Themes.Primary)),
                    new StyledLabel()
                        .Bind(Label.TextProperty, getter: (Material material) => material.ShortDescription)
                        .Row(1)
                        .Column(1)
                        .CenterVertical(),
                ])));

        Add(new Image()
            .Bind(Image.SourceProperty, getter: (CameraPageViewModel vm) => vm.FoundMaterial, convert: (Material material) => material.Image)
            .InputTransparent(true)
            .End()
            .Top()
            .Size(75)
            .Margins(right: 6));
    }
}

class ScanDrawable : IDrawable
{
    const float SidePadding = 25f;
    const float CornerRadius = 25f;
    const float StrokeSize = 8f;

    public float StartPosition { get; set; } = 0f;
    public float EndPosition { get; set; } = 0f;

    public void Draw(ICanvas canvas, RectF dirtyRect)
    {
        var size = Math.Min(dirtyRect.Width, dirtyRect.Height) - (SidePadding * 2);
        var left = (dirtyRect.Width - size) / 2;
        var top = (dirtyRect.Height - size) / 2;
        var rect = new RectF(left, top, size, size);

        canvas.ClipPath(CreateClipPath(rect));

        FillRect(canvas, rect);
        DrawCorners(canvas, rect);
    }

    private void DrawCorners(ICanvas canvas, RectF rect)
    {
        var straightSectionLength = CornerRadius * 0.6f;
        var totalLength = straightSectionLength + CornerRadius;

        var topLeft = new PathF();
        topLeft.MoveTo(rect.Left, rect.Top + totalLength);
        topLeft.LineTo(rect.Left, rect.Top + CornerRadius);
        topLeft.QuadTo(rect.Left, rect.Top, rect.Left + CornerRadius, rect.Top);
        topLeft.LineTo(rect.Left + totalLength, rect.Top);

        var topRight = new PathF();
        topLeft.MoveTo(rect.Right - totalLength, rect.Top);
        topLeft.LineTo(rect.Right - CornerRadius, rect.Top);
        topLeft.QuadTo(rect.Right, rect.Top, rect.Right, rect.Top + CornerRadius);
        topLeft.LineTo(rect.Right, rect.Top + totalLength);

        var bottomRight = new PathF();
        topLeft.MoveTo(rect.Right, rect.Bottom - totalLength);
        topLeft.LineTo(rect.Right, rect.Bottom - CornerRadius);
        topLeft.QuadTo(rect.Right, rect.Bottom, rect.Right - CornerRadius, rect.Bottom);
        topLeft.LineTo(rect.Right - totalLength, rect.Bottom);

        var bottomLeft = new PathF();
        topLeft.MoveTo(rect.Left + totalLength, rect.Bottom);
        topLeft.LineTo(rect.Left + CornerRadius, rect.Bottom);
        topLeft.QuadTo(rect.Left, rect.Bottom, rect.Left, rect.Bottom - CornerRadius);
        topLeft.LineTo(rect.Left, rect.Bottom - totalLength);

        canvas.StrokeColor = Themes.Primary.Light;
        canvas.StrokeSize = StrokeSize;
        canvas.DrawPath(topLeft);
        canvas.DrawPath(topRight);
        canvas.DrawPath(bottomRight);
        canvas.DrawPath(bottomLeft);
    }

    private void FillRect(ICanvas canvas, RectF rect)
    {
        var directionUp = StartPosition > EndPosition;
        var diff = Math.Abs(StartPosition - EndPosition);
        var fillRect = new RectF(
            rect.Left,
            rect.Top + (rect.Height * Math.Min(StartPosition, EndPosition)),
            rect.Width,
            rect.Height * diff);

        var gradient = new LinearGradientPaint([
            new PaintGradientStop(0, Color.FromArgb("#00ffffff")),
            new PaintGradientStop(1, Color.FromArgb("#aaffffff"))
        ])
        {
            StartPoint = new Point(0, directionUp ? 1 : 0),
            EndPoint = new Point(0, directionUp ? 0 : 1)
        };

        canvas.SetFillPaint(gradient, fillRect);
        canvas.FillPath(CreateFillPath(fillRect, directionUp));
    }
    
    private PathF CreateClipPath(RectF rect)
    {
        var path = new PathF();

        path.MoveTo(rect.Left + CornerRadius, rect.Top);
        path.LineTo(rect.Right - CornerRadius, rect.Top);
        path.QuadTo(rect.Right, rect.Top, rect.Right, rect.Top + CornerRadius);
        path.LineTo(rect.Right, rect.Bottom - CornerRadius);
        path.QuadTo(rect.Right, rect.Bottom, rect.Right - CornerRadius, rect.Bottom);
        path.LineTo(rect.Left + CornerRadius, rect.Bottom);
        path.QuadTo(rect.Left, rect.Bottom, rect.Left, rect.Bottom - CornerRadius);
        path.LineTo(rect.Left, rect.Top + CornerRadius);
        path.QuadTo(rect.Left, rect.Top, rect.Left + CornerRadius, rect.Top);
        path.Close();

        return path;
    }

    private PathF CreateFillPath(RectF rect, bool directionUp)
    {
        if (directionUp)
        {
            var path = new PathF();

            path.MoveTo(rect.Left + CornerRadius, rect.Top);
            path.LineTo(rect.Right - CornerRadius, rect.Top);
            path.QuadTo(rect.Right, rect.Top, rect.Right, rect.Top + CornerRadius);
            path.LineTo(rect.Right, rect.Bottom);
            path.LineTo(rect.Left, rect.Bottom);
            path.LineTo(rect.Left, rect.Top + CornerRadius);
            path.QuadTo(rect.Left, rect.Top, rect.Left + CornerRadius, rect.Top);
            path.Close();

            return path;
        }
        else
        {
            var path = new PathF();

            path.MoveTo(rect.Left, rect.Top);
            path.LineTo(rect.Right, rect.Top);
            path.LineTo(rect.Right, rect.Bottom - CornerRadius);
            path.QuadTo(rect.Right, rect.Bottom, rect.Right - CornerRadius, rect.Bottom);
            path.LineTo(rect.Left + CornerRadius, rect.Bottom);
            path.QuadTo(rect.Left, rect.Bottom, rect.Left, rect.Bottom - CornerRadius);
            path.LineTo(rect.Left, rect.Top);
            path.Close();

            return path;
        }
    }
}