using WasteApp.Core.Models;

namespace WasteApp.Core.Services;

public class ItemsService : IItemsService
{
    public IEnumerable<Item> GetPopularItems() =>
        [
            new Item
            {
                Name = "Glass Bottle",
                Image = "glass_bottle.png"
            },
            new Item
            {
                Name = "Coffee Cup",
                Image = "coffee_cup.png"
            },
            new Item
            {
                Name = "Plastic Bottle",
                Image = "pet_bottle.png"
            }
        ];
}