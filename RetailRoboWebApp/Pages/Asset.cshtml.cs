using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace RetailRoboWebApp.Pages;

public class AssetModel : PageModel
{
    private readonly ILogger<AssetModel> _logger;

    public AssetModel(ILogger<AssetModel> logger)
    {
        _logger = logger;
    }

    public void OnGet()
    {

    }
}
