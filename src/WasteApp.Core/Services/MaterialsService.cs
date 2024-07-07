using WasteApp.Core.Models;

namespace WasteApp.Core.Services;

public class MaterialsService : IMaterialsService
{
    // These images are from https://icons8.com/photos
    const string PlasticImage1 = "girl1.jpg";
    const string PlasticImage2 = "girl2.jpg";

    readonly IEnumerable<Material> materials;

    public MaterialsService()
    {
        materials =
        [
            new Material
            {
                Name = "Paper",
                MaterialEnum = MaterialEnum.Paper,
                WasteProcessingEnum = WasteProcessingEnum.Recycle,
                Image = "paper.png",
                Description = "It can take up to 1000 years for plastic to decompose in landfills. Plastic bags we use in our everyday life take 10-20 years to decompose, while plastic bottles take 450 years.",
                ShortDescription = "Put this item in your recycle bin.",
                Links =
                [
                    new Link
                    {
                        Description = "Effects of plastic on human body",
                        Image = PlasticImage1,
                        URL = "https://www.earthday.org/what-you-need-to-know-about-the-impact-of-plastics-on-human-health/"
                    },
                    new Link
                    {
                        Description = "What happens to plastic when heated",
                        Image = PlasticImage2,
                        URL = "https://www.nationalgeographic.com/environment/2019/07/exposed-to-extreme-heat-plastic-bottles-may-become-unsafe-over-time/"
                    }
                ],
                Facts =
                [
                    "Only 9% of all plastic produced is recycled.",
                    "A whopping 2 million plastic bags are used every minute worldwide.",
                    "Single-use plastics are illegal in some parts of the world.",
                    "Every minute of every day a truckload of plastic is dumped into the ocean.",
                    "73% of beach litter worldwide is plastic.",
                ],
            },
            new Material
            {
                Name = "Plastic",
                MaterialEnum = MaterialEnum.Plastic,
                WasteProcessingEnum = WasteProcessingEnum.Recycle,
                Image = "plastic.png",
                Description = "It can take up to 1000 years for plastic to decompose in landfills. Plastic bags we use in our everyday life take 10-20 years to decompose, while plastic bottles take 450 years.",
                ShortDescription = "Put this item in your recycle bin.",
                Links =
                [
                    new Link
                    {
                        Description = "Effects of plastic on human body",
                        Image = PlasticImage1,
                        URL = "https://www.earthday.org/what-you-need-to-know-about-the-impact-of-plastics-on-human-health/"
                    },
                    new Link
                    {
                        Description = "What happens to plastic when heated",
                        Image = PlasticImage2,
                        URL = "https://www.nationalgeographic.com/environment/2019/07/exposed-to-extreme-heat-plastic-bottles-may-become-unsafe-over-time/"
                    }
                ],
                Facts =
                [
                    "Only 9% of all plastic produced is recycled.",
                    "A whopping 2 million plastic bags are used every minute worldwide.",
                    "Single-use plastics are illegal in some parts of the world.",
                    "Every minute of every day a truckload of plastic is dumped into the ocean.",
                    "73% of beach litter worldwide is plastic.",
                ],
            },
            new Material
            {
                Name = "Glass",
                MaterialEnum = MaterialEnum.Glass,
                WasteProcessingEnum = WasteProcessingEnum.Recycle,
                Image = "glass.png",
                Description = "It can take up to 1000 years for plastic to decompose in landfills. Plastic bags we use in our everyday life take 10-20 years to decompose, while plastic bottles take 450 years.",
                ShortDescription = "Put this item in your recycle bin.",
                Links =
                [
                    new Link
                    {
                        Description = "Effects of plastic on human body",
                        Image = PlasticImage1,
                        URL = "https://www.earthday.org/what-you-need-to-know-about-the-impact-of-plastics-on-human-health/"
                    },
                    new Link
                    {
                        Description = "What happens to plastic when heated",
                        Image = PlasticImage2,
                        URL = "https://www.nationalgeographic.com/environment/2019/07/exposed-to-extreme-heat-plastic-bottles-may-become-unsafe-over-time/"
                    }
                ],
                Facts =
                [
                    "Only 9% of all plastic produced is recycled.",
                    "A whopping 2 million plastic bags are used every minute worldwide.",
                    "Single-use plastics are illegal in some parts of the world.",
                    "Every minute of every day a truckload of plastic is dumped into the ocean.",
                    "73% of beach litter worldwide is plastic.",
                ],
            },
            new Material
            {
                Name = "Aluminium",
                MaterialEnum = MaterialEnum.Aluminium,
                WasteProcessingEnum = WasteProcessingEnum.Recycle,
                Image = "aluminium.png",
                Description = "It can take up to 1000 years for plastic to decompose in landfills. Plastic bags we use in our everyday life take 10-20 years to decompose, while plastic bottles take 450 years.",
                ShortDescription = "Put this item in your recycle bin.",
                Links =
                [
                    new Link
                    {
                        Description = "Effects of plastic on human body",
                        Image = PlasticImage1,
                        URL = "https://www.earthday.org/what-you-need-to-know-about-the-impact-of-plastics-on-human-health/"
                    },
                    new Link
                    {
                        Description = "What happens to plastic when heated",
                        Image = PlasticImage2,
                        URL = "https://www.nationalgeographic.com/environment/2019/07/exposed-to-extreme-heat-plastic-bottles-may-become-unsafe-over-time/"
                    }
                ],
                Facts =
                [
                    "Only 9% of all plastic produced is recycled.",
                    "A whopping 2 million plastic bags are used every minute worldwide.",
                    "Single-use plastics are illegal in some parts of the world.",
                    "Every minute of every day a truckload of plastic is dumped into the ocean.",
                    "73% of beach litter worldwide is plastic.",
                ],
            }
        ];
    }

    public IEnumerable<Material> GetMaterials() => materials.ToList();

    public Material? GetMaterial(MaterialEnum materialEnum) =>
        materials.FirstOrDefault(m => m.MaterialEnum == materialEnum);
}