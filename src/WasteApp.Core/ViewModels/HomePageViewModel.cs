using System.Windows.Input;
using WasteApp.Core.Extensions;
using WasteApp.Core.Interfaces.Services;
using WasteApp.Core.Models;

namespace WasteApp.Core.ViewModels;

public class HomePageViewModel : BasePageViewModel
{
    WasteProcessingEnum selectedWasteProcessing;

    public IEnumerable<Material> Materials { get; private init; }
    public IEnumerable<Item> PopularItems { get; private init; }
    public IEnumerable<WasteProcessingViewModel> WasteProcessings { get; private init; }
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
        WasteProcessings = Enum
            .GetValues(typeof(WasteProcessingEnum))
            .Cast<WasteProcessingEnum>()
            .Select((e) => new WasteProcessingViewModel(e, e.ToString(), e.ToIcon(), e.ToColor()));

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