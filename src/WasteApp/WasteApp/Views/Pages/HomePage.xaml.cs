using Microsoft.Extensions.DependencyInjection;
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
    }
}