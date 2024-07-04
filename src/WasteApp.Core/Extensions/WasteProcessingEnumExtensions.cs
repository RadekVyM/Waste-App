using WasteApp.Core.Models;

namespace WasteApp.Core.Extensions;

public static class WasteProcessingEnumExtensions
{
    public static string ToIcon(this WasteProcessingEnum wasteProcessing)
    {
        return wasteProcessing switch
        {
            WasteProcessingEnum.Recycle => "recycle.png",
            WasteProcessingEnum.Garbage => "trash_can.png",
            WasteProcessingEnum.Green => "leaf.png",
            WasteProcessingEnum.Yard => "tree.png",
            _ => "",
        };
    }

    public static string ToAdjective(this WasteProcessingEnum wasteProcessing)
    {
        return wasteProcessing switch
        {
            WasteProcessingEnum.Recycle => "Recyclable",
            WasteProcessingEnum.Garbage => "Garbage",
            WasteProcessingEnum.Green => "Compostable",
            WasteProcessingEnum.Yard => "Yard",
            _ => "",
        };
    }

    public static string ToColor(this WasteProcessingEnum wasteProcessing)
    {
        return wasteProcessing switch
        {
            WasteProcessingEnum.Recycle => "#81d3e1",
            WasteProcessingEnum.Garbage => "#fe5c19",
            WasteProcessingEnum.Green => "#acbf60",
            WasteProcessingEnum.Yard => "#ffc431",
            _ => "",
        };
    }
}