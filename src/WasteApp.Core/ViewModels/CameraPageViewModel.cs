using System.Windows.Input;
using WasteApp.Core.Interfaces.Services;
using WasteApp.Core.Models;

namespace WasteApp.Core.ViewModels;

public class CameraPageViewModel : BasePageViewModel
{
    Material? foundMaterial;

    public Material? FoundMaterial
    {
        get => foundMaterial;
        set
        {
            foundMaterial = value;
            OnPropertyChanged(nameof(FoundMaterial));
        }
    }

    public ICommand MaterialCommand { get; private init; }

    public CameraPageViewModel(INavigationService navigationService, IMaterialsService materialsService)
    {
        FoundMaterial = materialsService.GetMaterial(MaterialEnum.Plastic);
        MaterialCommand = new RelayCommand(async () => 
        {
            var material = materialsService.GetMaterial(MaterialEnum.Plastic) ?? throw new Exception("Material cannot be null here.");

            await navigationService.GoTo(PageType.MaterialDetailPage, new MaterialDetailPageParameters(material));
        });
    }
}