using Microsoft.Extensions.DependencyInjection;
using System;
using WasteApp.Core;
using Xamarin.Forms;

namespace WasteApp
{
    public partial class App : Application
    {
        public IServiceProvider ServiceProvider { get; private set; }

        public App()
        {
            InitializeComponent();

            Device.SetFlags(new string[] { "Shapes_Experimental" });

            var services = new ServiceCollection();

            services.AddSingleton<INavigationService, NavigationService>();
            services.AddSingleton<IBrowser, Browser>();
            services.AddSingleton<IMaterialsService, MaterialsService>();
            services.AddSingleton<IItemsService, ItemsService>();
            services.AddTransient<IHomePageViewModel, HomePageViewModel>();
            services.AddTransient<IMaterialDetailPageViewModel, MaterialDetailPageViewModel>();
            services.AddTransient<ICameraPageViewModel, CameraPageViewModel>();

            ServiceProvider = services.BuildServiceProvider();

            MainPage = new AppShell();
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
