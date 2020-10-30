using System.Collections.Generic;

namespace WasteApp.Core
{
    public interface IMaterialsService
    {
        IEnumerable<Material> GetMaterials();
        Material GetMaterial(MaterialEnum materialEnum);
    }
}