using WasteApp.Core.Models;

namespace WasteApp.Core;

public interface IMaterialsService
{
    IEnumerable<Material> GetMaterials();
    Material? GetMaterial(MaterialEnum materialEnum);
}