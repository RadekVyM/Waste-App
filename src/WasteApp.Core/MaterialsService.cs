using System.Collections.Generic;
using System.Linq;

namespace WasteApp.Core
{
    public class MaterialsService : IMaterialsService
    {
        // These images are from https://icons8.com/photos
        string plasticImage1 = "girl1.jpg";
        string plasticImage2 = "girl2.jpg";

        IEnumerable<Material> materials;

        public MaterialsService()
        {
            materials = new List<Material>
            {
                new Material
                {
                    Name = "Paper",
                    MaterialEnum = MaterialEnum.Paper,
                    WasteProcessingEnum = WasteProcessingEnum.Recycle,
                    Image = "paper.png",
                    Description = "It can take up to 1000 years for plastic to decompose in landfills. Plastic bags we use in our everyday life take 10-20 years to decompose, while plastic bottles take 450 years.",
                    ShortDescription = "Put this item in your recycle bin.",
                    Links = new Link[]
                    {
                        new Link
                        {
                            Description = "Effects of plastic on human body",
                            Image = plasticImage1,
                            URL = "https://repurpose.global/letstalktrash/harmful-effects-of-plastic-pollution-on-human-health/"
                        },
                        new Link
                        {
                            Description = "What happens to plastic when heated",
                            Image = plasticImage2,
                            URL = "https://www.nationalgeographic.com/environment/2019/07/exposed-to-extreme-heat-plastic-bottles-may-become-unsafe-over-time/"
                        }
                    }
                },
                new Material
                {
                    Name = "Plastic",
                    MaterialEnum = MaterialEnum.Plastic,
                    WasteProcessingEnum = WasteProcessingEnum.Recycle,
                    Image = "plastic.png",
                    Description = "It can take up to 1000 years for plastic to decompose in landfills. Plastic bags we use in our everyday life take 10-20 years to decompose, while plastic bottles take 450 years.",
                    ShortDescription = "Put this item in your recycle bin.",
                    Links = new Link[]
                    {
                        new Link
                        {
                            Description = "Effects of plastic on human body",
                            Image = plasticImage1,
                            URL = "https://repurpose.global/letstalktrash/harmful-effects-of-plastic-pollution-on-human-health/"
                        },
                        new Link
                        {
                            Description = "What happens to plastic when heated",
                            Image = plasticImage2,
                            URL = "https://www.nationalgeographic.com/environment/2019/07/exposed-to-extreme-heat-plastic-bottles-may-become-unsafe-over-time/"
                        }
                    }
                },
                new Material
                {
                    Name = "Glass",
                    MaterialEnum = MaterialEnum.Glass,
                    WasteProcessingEnum = WasteProcessingEnum.Recycle,
                    Image = "glass.png",
                    Description = "It can take up to 1000 years for plastic to decompose in landfills. Plastic bags we use in our everyday life take 10-20 years to decompose, while plastic bottles take 450 years.",
                    ShortDescription = "Put this item in your recycle bin.",
                    Links = new Link[]
                    {
                        new Link
                        {
                            Description = "Effects of plastic on human body",
                            Image = plasticImage1,
                            URL = "https://repurpose.global/letstalktrash/harmful-effects-of-plastic-pollution-on-human-health/"
                        },
                        new Link
                        {
                            Description = "What happens to plastic when heated",
                            Image = plasticImage2,
                            URL = "https://www.nationalgeographic.com/environment/2019/07/exposed-to-extreme-heat-plastic-bottles-may-become-unsafe-over-time/"
                        }
                    }
                },
                new Material
                {
                    Name = "Aluminium",
                    MaterialEnum = MaterialEnum.Aluminium,
                    WasteProcessingEnum = WasteProcessingEnum.Recycle,
                    Image = "aluminium.png",
                    Description = "It can take up to 1000 years for plastic to decompose in landfills. Plastic bags we use in our everyday life take 10-20 years to decompose, while plastic bottles take 450 years.",
                    ShortDescription = "Put this item in your recycle bin.",
                    Links = new Link[]
                    {
                        new Link
                        {
                            Description = "Effects of plastic on human body",
                            Image = plasticImage1,
                            URL = "https://repurpose.global/letstalktrash/harmful-effects-of-plastic-pollution-on-human-health/"
                        },
                        new Link
                        {
                            Description = "What happens to plastic when heated",
                            Image = plasticImage2,
                            URL = "https://www.nationalgeographic.com/environment/2019/07/exposed-to-extreme-heat-plastic-bottles-may-become-unsafe-over-time/"
                        }
                    }
                }
            };
        }

        public IEnumerable<Material> GetMaterials()
        {
            return materials;
        }

        public Material GetMaterial(MaterialEnum materialEnum)
        {
            return materials.FirstOrDefault(m => m.MaterialEnum == materialEnum);
        }
    }
}
