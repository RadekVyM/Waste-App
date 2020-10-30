using Microsoft.Extensions.DependencyInjection;
using SkiaSharp;
using SkiaSharp.Views.Forms;
using System;
using System.Diagnostics;
using WasteApp.Core;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace WasteApp
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CameraPage : ContentPage
    {
        bool isCameraOpen = false;
        float whitePosition = 0f;
        float transparentPosition = 0f;
        float cornerRadius => (float)(30 * DeviceDisplay.MainDisplayInfo.Density);
        Stopwatch stopwatch;
        bool pageIsActive;
        SKPaint overlayPaint;
        SKColor transparentColour;
        SKColor whiteColour;

        public CameraPage()
        {
            stopwatch = new Stopwatch();

            transparentColour = Color.White.ToSKColor().WithAlpha((byte)(0 * byte.MaxValue));
            whiteColour = Color.White.ToSKColor().WithAlpha((byte)(0.5 * byte.MaxValue));

            InitializeComponent();

            SizeChanged += CameraPageSizeChanged;

            BindingContext = ((App)App.Current).ServiceProvider.GetRequiredService<ICameraPageViewModel>();
        }


        protected override void OnAppearing()
        {
            base.OnAppearing();

            stopwatch.Start();
            pageIsActive = true;

            Device.StartTimer(TimeSpan.FromMilliseconds(16), () =>
            {
                double milliseconds = stopwatch.Elapsed.TotalMilliseconds;
                float f = (float)(stopwatch.Elapsed.TotalMilliseconds % 4000f / 1000f);

                if(f <= 2f)
                {
                    whitePosition = f < 1f ? f : 1f;
                    transparentPosition = f - 1f < 0f ? 0f : f - 1f;
                }
                else
                {
                    f -= 2f;

                    whitePosition = f < 1f ? Math.Abs(f - 1) : 0f;
                    transparentPosition = f - 1f < 0f ? 1f : Math.Abs(f - 2f);
                }

                scanCanvasView.InvalidateSurface();

                if (!pageIsActive)
                {
                    stopwatch.Stop();
                }
                return pageIsActive;
            });
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            pageIsActive = false;
        }

        private void CameraPageSizeChanged(object sender, EventArgs e)
        {
            scanCanvasView.WidthRequest = Width - scanCanvasView.Margin.HorizontalThickness;
            scanCanvasView.HeightRequest = Height * 0.48d;

            cornersCanvasView.WidthRequest = Width - scanCanvasView.Margin.HorizontalThickness;
            cornersCanvasView.HeightRequest = Height * 0.48d;
        }

        private async void CameraPreviewSizeChanged(object sender, EventArgs e)
        {
            if (isCameraOpen)
                return;

            var result = await Permissions.CheckStatusAsync<Permissions.Camera>();

            if (result == PermissionStatus.Granted)
            {
                cameraPreview.Open();
                isCameraOpen = true;
            }
            else
                await Shell.Current.Navigation.PopAsync();
        }

        private void ScanCanvasViewPaintSurface(object sender, SKPaintSurfaceEventArgs e)
        {
            var canvas = e.Surface.Canvas;
            var info = e.Info;

            canvas.Clear();

            SKRoundRect roundRect = new SKRoundRect(new SKRect(4, 4, info.Width - 4, info.Height - 4), cornerRadius - 4, cornerRadius - 4);

            if (overlayPaint == null)
                overlayPaint = new SKPaint();
            
            bool down = transparentPosition - whitePosition < 0;

            overlayPaint.Shader = SKShader.CreateLinearGradient(
                            new SKPoint(info.Rect.Left, info.Rect.Top),
                            new SKPoint(info.Rect.Left, info.Height),
                            new SKColor[]
                            {
                                down ? transparentColour : whiteColour,
                                down ? whiteColour : transparentColour
                            },
                            new float[] { down ? transparentPosition : whitePosition, down ? whitePosition : transparentPosition },
                            SKShaderTileMode.Clamp);

            canvas.ClipRoundRect(roundRect, antialias: true);

            canvas.DrawRoundRect(0, down ? info.Height * transparentPosition : info.Height * whitePosition, info.Width, down ? info.Height * whitePosition : info.Height * transparentPosition, cornerRadius, cornerRadius, overlayPaint);
        }

        private void CornersCanvasViewPaintSurface(object sender, SKPaintSurfaceEventArgs e)
        {
            var canvas = e.Surface.Canvas;
            var info = e.Info;

            canvas.Clear();

            SKRoundRect roundRect = new SKRoundRect(new SKRect(0, 0, info.Width, info.Height), cornerRadius, cornerRadius);

            using(SKPaint cornerPaint = new SKPaint())
            {
                cornerPaint.Color = App.Current.Resources.GetValue<Color>("DarkDetailColour").ToSKColor();
                cornerPaint.Style = SKPaintStyle.Stroke;
                cornerPaint.StrokeWidth = 20;
                cornerPaint.IsAntialias = true;
                cornerPaint.StrokeCap = SKStrokeCap.Butt;
                cornerPaint.StrokeJoin = SKStrokeJoin.Round;

                canvas.ClipRoundRect(roundRect, antialias: true);
                canvas.ClipRect(new SKRect(0, cornerRadius * 1.5f, info.Width, info.Height - (cornerRadius * 1.5f)), SKClipOperation.Difference);
                canvas.ClipRect(new SKRect(cornerRadius * 1.5f, 0, info.Width - (cornerRadius * 1.5f), info.Height), SKClipOperation.Difference);

                canvas.DrawRoundRect(roundRect, cornerPaint);
            }
        }
    }
}