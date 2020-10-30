using System.Threading.Tasks;

namespace WasteApp.Core
{
    public interface IBrowser
    {
        Task OpenAsync(string uri);
    }
}
