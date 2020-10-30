using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;

namespace WasteApp.Core
{
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

        public ICommand MaterialCommand { get; private set; }
        public ICommand WasteProcessingSelectedCommand { get; private set; }

        public HomePageViewModel(INavigationService navigationService, IMaterialsService materialsService, IItemsService itemsService) : base(navigationService)
        {
            Materials = materialsService.GetMaterials();
            PopularItems = itemsService.GetPopularItems();
            WasteProcessings = Enum.GetValues(typeof(WasteProcessingEnum)).Cast<WasteProcessingEnum>();

            MaterialCommand = new RelayCommand(() => 
            {
                navigationService.Push(PageEnum.MaterialDetailPage, materialsService.GetMaterial(MaterialEnum.Plastic));
            });
            WasteProcessingSelectedCommand = new RelayCommand(parameter => 
            {
                SelectedWasteProcessing = (WasteProcessingEnum)parameter;
            });
        }
    }
}
