using System.Windows.Input;
using WasteApp.Core.Interfaces.Services;
using WasteApp.Core.Interfaces.ViewModels;
using WasteApp.Core.Models;

namespace WasteApp.Core.ViewModels;

public class HomePageViewModel : BasePageViewModel, IHomePageViewModel
{
    WasteProcessingEnum selectedWasteProcessing;

    public IEnumerable<Material> Materials { get; set; }
    public IEnumerable<Item> PopularItems { get; set; }
    public IEnumerable<WasteProcessingEnum> WasteProcessings { get; set; }
    public WasteProcessingEnum SelectedWasteProcessing
    {
        get => selectedWasteProcessing;
        set
        {
            selectedWasteProcessing = value;
            OnPropertyChanged(nameof(SelectedWasteProcessing));
        }
    }

    public ICommand MaterialCommand { get; private init; }
    public ICommand WasteProcessingSelectedCommand { get; private init; }

    public HomePageViewModel(INavigationService navigationService, IMaterialsService materialsService, IItemsService itemsService)
    {
        Materials = materialsService.GetMaterials();
        PopularItems = itemsService.GetPopularItems();
        WasteProcessings = Enum.GetValues(typeof(WasteProcessingEnum)).Cast<WasteProcessingEnum>();

        MaterialCommand = new RelayCommand(async () => 
        {
            var material = materialsService.GetMaterial(MaterialEnum.Plastic) ?? throw new Exception("Material cannot be null here.");

            await navigationService.GoTo(PageType.MaterialDetailPage, new MaterialDetailPageParameters(material));
        });
        WasteProcessingSelectedCommand = new RelayCommand(parameter => 
        {
            if (parameter is WasteProcessingEnum wasteProcessing)
                SelectedWasteProcessing = wasteProcessing;
        });
    }
}