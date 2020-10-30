using System.Windows.Input;

namespace WasteApp.Core
{
    public class CameraPageViewModel : BasePageViewModel, ICameraPageViewModel
    {
        Material foundMaterial;

        public Material FoundMaterial
        {
            get => foundMaterial;
            set
            {
                foundMaterial = value;
                OnPropertyChanged(nameof(FoundMaterial));
            }
        }

        public ICommand MaterialCommand { get; private set; }

        public CameraPageViewModel(INavigationService navigationService, IMaterialsService materialsService) : base(navigationService)
        {
            FoundMaterial = materialsService.GetMaterial(MaterialEnum.Plastic);
            MaterialCommand = new RelayCommand(() => navigationService.Push(PageEnum.MaterialDetailPage, materialsService.GetMaterial(MaterialEnum.Plastic)) );
        }
    }
}
