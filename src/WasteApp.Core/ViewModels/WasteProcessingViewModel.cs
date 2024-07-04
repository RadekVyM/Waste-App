using WasteApp.Core.Models;

namespace WasteApp.Core.ViewModels;

public record WasteProcessingViewModel(WasteProcessingEnum Enum, string Title, string Icon, string Color);