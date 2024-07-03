namespace WasteApp.Core.Models;

public class Material
{
    public MaterialEnum MaterialEnum { get; set; }
    public WasteProcessingEnum WasteProcessingEnum { get; set; }
    public required string Name { get; set; }
    public required string Description { get; set; }
    public required string ShortDescription { get; set; }
    public required string Image { get; set; }
    public required Link[] Links { get; set; }
}