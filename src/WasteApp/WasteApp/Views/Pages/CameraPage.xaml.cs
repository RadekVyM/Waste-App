using Microsoft.Extensions.DependencyInjection;
using System;
using System.Diagnostics;
using WasteApp.Core;
using Xamarin.Forms;
using Xamarin.Forms.Shapes;
using Xamarin.Forms.Xaml;

namespace WasteApp
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CameraPage : ContentPage
    {
        float whitePosition = 0f;
        float transparentPosition = 0f;
        double cornerRadius => 30;
        double cornersStrokeThickness => 4;
        Stopwatch stopwatch;
        bool pageIsActive;
        Color transparentColour;
        Color whiteColour;

        public CameraPage()
        {
            stopwatch = new Stopwatch();

            transparentColour = Color.FromHex("#00000000");
            whiteColour = Color.FromHex("#80ffffff");

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

                bool down = transparentPosition - whitePosition < 0;

                boxView.TranslateTo(0, down ? scanFrame.Height * transparentPosition : scanFrame.Height * whitePosition, 0);
                if (down && whitePosition != 1)
                    boxView.HeightRequest = scanFrame.Height * whitePosition;
                else if (down && whitePosition == 1)
                    boxView.HeightRequest = scanFrame.Height - (scanFrame.Height * transparentPosition);

                if (!down && whitePosition != 0)
                    boxView.HeightRequest = scanFrame.Height * (1 - whitePosition);
                else if (!down && whitePosition == 0)
                    boxView.HeightRequest = scanFrame.Height - (scanFrame.Height * (1 - transparentPosition));

                boxView.Background = new LinearGradientBrush(
                    new GradientStopCollection
                    {
                        new GradientStop(down ? transparentColour : whiteColour, down ? transparentPosition : whitePosition),
                        new GradientStop(down ? whiteColour : transparentColour, down ? whitePosition : transparentPosition)
                    },
                    new Point(0,0), new Point(0, 1));

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
            scanFrame.WidthRequest = Width - scanFrame.Margin.HorizontalThickness;
            scanFrame.HeightRequest = Height * 0.48d;
            boxView.WidthRequest = scanFrame.WidthRequest;

            cornersPath.WidthRequest = scanFrame.WidthRequest + cornersStrokeThickness;
            cornersPath.HeightRequest = scanFrame.HeightRequest + cornersStrokeThickness;
            cornersPath.StrokeThickness = cornersStrokeThickness;

            double strokeMargin = cornersStrokeThickness / 2;

            cornersPath.Data = new PathGeometry
            {
                Figures = new PathFigureCollection
                {
                    new PathFigure
                    {
                        IsClosed = false, IsFilled = false, StartPoint = new Point(strokeMargin, (cornerRadius * 1.5) + strokeMargin),
                        Segments = new PathSegmentCollection
                        {
                            new LineSegment(new Point(strokeMargin, cornerRadius + strokeMargin + 1)),
                            new QuadraticBezierSegment(new Point(strokeMargin, strokeMargin), new Point(cornerRadius + strokeMargin + 1, strokeMargin)),
                            new LineSegment(new Point((cornerRadius * 1.5) + strokeMargin, strokeMargin))
                        }
                    },
                    new PathFigure
                    {
                        IsClosed = false, IsFilled = false, StartPoint = new Point(cornersPath.WidthRequest - (cornerRadius * 1.5) - strokeMargin, strokeMargin),
                        Segments = new PathSegmentCollection
                        {
                            new LineSegment(new Point(cornersPath.WidthRequest - cornerRadius - strokeMargin - 1, strokeMargin)),
                            new QuadraticBezierSegment(new Point(cornersPath.WidthRequest - strokeMargin, strokeMargin), new Point(cornersPath.WidthRequest - strokeMargin, strokeMargin + cornerRadius + 1)),
                            new LineSegment(new Point(cornersPath.WidthRequest - strokeMargin, (cornerRadius * 1.5) + strokeMargin))
                        }
                    },
                    new PathFigure
                    {
                        IsClosed = false, IsFilled = false, StartPoint = new Point(cornersPath.WidthRequest - strokeMargin, cornersPath.HeightRequest - (cornerRadius * 1.5) - strokeMargin),
                        Segments = new PathSegmentCollection
                        {
                            new LineSegment(new Point(cornersPath.WidthRequest - strokeMargin, cornersPath.HeightRequest - cornerRadius - strokeMargin - 1)),
                            new QuadraticBezierSegment(new Point(cornersPath.WidthRequest - strokeMargin, cornersPath.HeightRequest - strokeMargin), new Point(cornersPath.WidthRequest - strokeMargin - cornerRadius - 1, cornersPath.HeightRequest - strokeMargin)),
                            new LineSegment(new Point(cornersPath.WidthRequest - strokeMargin - (cornerRadius * 1.5), cornersPath.HeightRequest - strokeMargin))
                        }
                    },
                    new PathFigure
                    {
                        IsClosed = false, IsFilled = false, StartPoint = new Point(strokeMargin + (cornerRadius * 1.5), cornersPath.HeightRequest - strokeMargin),
                        Segments = new PathSegmentCollection
                        {
                            new LineSegment(new Point(cornerRadius + strokeMargin + 1, cornersPath.HeightRequest - strokeMargin)),
                            new QuadraticBezierSegment(new Point(strokeMargin, cornersPath.HeightRequest - strokeMargin), new Point(strokeMargin, cornersPath.HeightRequest - cornerRadius - strokeMargin - 1)),
                            new LineSegment(new Point(strokeMargin, cornersPath.HeightRequest - (cornerRadius * 1.5) - strokeMargin))
                        }
                    }
                }
            };
        }
    }
}