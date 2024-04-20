using Edgy.OrchardCore.Models;

namespace Edgy.App.Pages;

public class BasePageModel : PageModel
{
    public PageContent PageContent { get; set; } = new();

    public BasePageModel()
    {
    }
}
