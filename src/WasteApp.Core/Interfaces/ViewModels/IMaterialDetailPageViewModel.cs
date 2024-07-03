using WasteApp.Core.Models;

namespace WasteApp.Core.Interfaces.ViewModels;

public interface IMaterialDetailPageViewModel : IBasePageViewModel
{
    Material? Material { get; set; }
}