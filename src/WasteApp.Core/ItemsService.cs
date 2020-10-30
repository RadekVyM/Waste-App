using System.Collections.Generic;

namespace WasteApp.Core
{
    public class ItemsService : IItemsService
    {
        public IEnumerable<Item> GetPopularItems()
        {
            return new List<Item>
            {
                new Item
                {
                    Name = "Glass Bottle",
                    Image = "glassBottle.png"
                },
                new Item
                {
                    Name = "Coffee Cup",
                    Image = "coffeeCup.png"
                },
                new Item
                {
                    Name = "Plastic Bottle",
                    Image = "petBottle.png"
                }
            };
        }
    }
}
