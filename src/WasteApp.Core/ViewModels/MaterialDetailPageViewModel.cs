using System.Windows.Input;
using WasteApp.Core.Interfaces.Services;
using WasteApp.Core.Interfaces.ViewModels;
using WasteApp.Core.Models;

namespace WasteApp.Core.ViewModels;

public class MaterialDetailPageViewModel : BasePageViewModel, IMaterialDetailPageViewModel
{
    Material? material;

    public Material? Material
    {
        get => material;
        set
        {
            material = value;
            OnPropertyChanged(nameof(Material));
        }
    }

    public ICommand LinkCommand { get; private init; }

    public MaterialDetailPageViewModel(IBrowser browser)
    {
        LinkCommand = new RelayCommand(async parameter => 
        {
            if (parameter?.ToString() is string uri)
                await browser.OpenAsync(uri);
        });
    }

    public override void OnApplyFirstParameters(IParameters parameters)
    {
        if (parameters is not MaterialDetailPageParameters materialDetailPageParameters)
            throw new Exception("Wrong parameters have been sent to the ViewModel.");

        Material = materialDetailPageParameters.Material;
    }
}