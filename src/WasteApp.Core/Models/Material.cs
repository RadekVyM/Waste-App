namespace WasteApp.Core
{
    public class Material
    {
        public MaterialEnum MaterialEnum { get; set; }
        public WasteProcessingEnum WasteProcessingEnum { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string ShortDescription { get; set; }
        public string Image { get; set; }
        public Link[] Links { get; set; }
    }
}
