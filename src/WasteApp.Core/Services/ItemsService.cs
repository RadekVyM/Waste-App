using WasteApp.Core.Models;

namespace WasteApp.Core.Services;

public class ItemsService : IItemsService
{
    public IEnumerable<Item> GetPopularItems() =>
        [
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
        ];
}