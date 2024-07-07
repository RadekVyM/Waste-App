namespace WasteApp.Core.Interfaces.Services;

public interface IBrowser
{
    Task OpenAsync(string uri);
}