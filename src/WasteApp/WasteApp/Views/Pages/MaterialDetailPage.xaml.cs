using Microsoft.Extensions.DependencyInjection;
using WasteApp.Core;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace WasteApp
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MaterialDetailPage : ContentPage
    {
        public MaterialDetailPage()
        {
            InitializeComponent();

            BindingContext = ((App)App.Current).ServiceProvider.GetRequiredService<IMaterialDetailPageViewModel>();
        }
    }
}