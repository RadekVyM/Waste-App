using WasteApp.Core.Models;

namespace WasteApp.Core;

public interface IItemsService
{
    IEnumerable<Item> GetPopularItems();
}