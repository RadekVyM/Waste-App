using System.Collections.Generic;

namespace WasteApp.Core
{
    public interface IItemsService
    {
        IEnumerable<Item> GetPopularItems();
    }
}