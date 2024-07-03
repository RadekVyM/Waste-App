using WasteApp.Core.Interfaces.ViewModels;
using WasteApp.Core.Models;

namespace WasteApp.Core.ViewModels;

public record MaterialDetailPageParameters(Material Material) : IParameters;