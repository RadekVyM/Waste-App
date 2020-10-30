using System.Linq;
using System.Windows.Input;

namespace WasteApp.Core
{
    public class MaterialDetailPageViewModel : BasePageViewModel, IMaterialDetailPageViewModel
    {
        Material material;

        public Material Material
        {
            get => material;
            set
            {
                material = value;
                OnPropertyChanged(nameof(Material));
            }
        }

        public ICommand LinkCommand { get; private set; }

        public MaterialDetailPageViewModel(INavigationService navigationService, IBrowser browser) : base(navigationService)
        {
            LinkCommand = new RelayCommand(async parameter => 
            {
                await browser.OpenAsync(parameter.ToString());
            });
        }

        public override void OnPagePushing(params object[] parameters)
        {
            Material = parameters.FirstOrDefault() as Material;
        }
    }
}
