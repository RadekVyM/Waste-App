using System;
using System.Threading.Tasks;
using WasteApp.Core;
using Xamarin.Forms;
using Xamarin.Forms.Shapes;
using Xamarin.Forms.Xaml;

namespace WasteApp
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TabBarView : ContentView
    {
        bool isTabBarShown = true;
        double backgroundCornerRadius => 60;

        public PathGeometry BackgroundPathGeometry { get; set; }

        public TabBarView()
        {
            InitializeComponent();

            SizeChanged += TabBarViewSizeChanged;
        }

        private void TabBarViewSizeChanged(object sender, EventArgs e)
        {
            backgroundPath.Fill = new SolidColorBrush(App.Current.Resources.GetValue<Color>("DarkDetailColour"));
            BackgroundPathGeometry = CreatePath();
            OnPropertyChanged(nameof(BackgroundPathGeometry));

            tabsGrid.HeightRequest = Height - backgroundCornerRadius;
        }

        private PathGeometry CreatePath()
        {
            return new PathGeometry
            {
                Figures = new PathFigureCollection
                {
                    new PathFigure
                    {
                        StartPoint = new Point(-1,0), IsClosed = true, IsFilled = true, 
                        Segments = new PathSegmentCollection
                        {
                            new QuadraticBezierSegment(new Point(-1, backgroundCornerRadius), new Point(backgroundCornerRadius, backgroundCornerRadius)),
                            new LineSegment(new Point(Width - backgroundCornerRadius + 1, backgroundCornerRadius)),
                            new QuadraticBezierSegment(new Point(Width + 1, backgroundCornerRadius), new Point(Width + 1, 0)),
                            new LineSegment(new Point(Width + 1, Height)),
                            new LineSegment(new Point(-1, Height))
                        }
                    }
                }
            };
        }

        private void HomeTapped(object sender, EventArgs e) => Shell.Current.GoToAsync("///" + PageEnum.HomePage.ToString());

        private void ScanTapped(object sender, EventArgs e)
        {
            if (Shell.Current.CurrentState.Location.OriginalString.Contains(PageEnum.CameraPage.ToString()))
                Shell.Current.Navigation.PopAsync();
            else
                Shell.Current.GoToAsync(PageEnum.CameraPage.ToString());
        }

        private void CalendarTapped(object sender, EventArgs e) => Shell.Current.GoToAsync("///" + PageEnum.CalendarPage.ToString());

        public async Task HideTabBarAsync()
        {
            if(isTabBarShown)
                await mainGrid.TranslateTo(0, Height);
            isTabBarShown = false;
        }

        public async Task ShowTabBarAsync()
        {
            if (!isTabBarShown)
                await mainGrid.TranslateTo(0, 0, 400);
            isTabBarShown = true;
        }

        public async Task UpdateIndicators(PageEnum page)
        {
            if(page == PageEnum.HomePage)
            {
                await calendarEllipse.FadeTo(0, 180);
                await homeEllipse.FadeTo(1, 180);
            }
            else if (page == PageEnum.CalendarPage)
            {
                await homeEllipse.FadeTo(0, 180);
                await calendarEllipse.FadeTo(1, 180);
            }
            else
            {
                await Task.WhenAll(
                    calendarEllipse.FadeTo(0, 180),
                    homeEllipse.FadeTo(0, 180));
            }
        }
    }
}