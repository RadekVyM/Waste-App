using Microsoft.Extensions.DependencyInjection;
using SkiaSharp;
using SkiaSharp.Views.Forms;
using WasteApp.Core;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace WasteApp
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class HomePage : ContentPage
    {
        public string AvatarImage { get; set; }

        public HomePage()
        {
            AvatarImage = "logo.jpg";

            InitializeComponent();

            BindingContext = ((App)App.Current).ServiceProvider.GetRequiredService<IHomePageViewModel>();
        }

        private void GradientPaintSurface(object sender, SKPaintSurfaceEventArgs e)
        {
            var canvas = e.Surface.Canvas;
            var info = e.Info;

            canvas.Clear();

            using (SKPaint paint = new SKPaint())
            {
                paint.IsAntialias = true;

                paint.Shader = SKShader.CreateLinearGradient(
                                new SKPoint(info.Rect.Left, info.Rect.Top),
                                new SKPoint(info.Rect.Left, info.Height),
                                new SKColor[] 
                                { 
                                    App.Current.Resources.GetValue<Color>("BackgroundColour").ToSKColor().WithAlpha((byte)(1 * byte.MaxValue)),
                                    App.Current.Resources.GetValue<Color>("BackgroundColour").ToSKColor().WithAlpha((byte)(0 * byte.MaxValue)) 
                                },
                                new float[] { 0.2f, 1f },
                                SKShaderTileMode.Clamp);

                canvas.DrawRect(0, 0, info.Width, info.Height * 2, paint);
            }
        }
    }
}