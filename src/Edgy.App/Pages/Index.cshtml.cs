namespace Edgy.App.Pages;

public class IndexModel : AliasPageModel
{
    public IndexModel(ContentService contentService) :
        base(contentService, "home")
    {
    }
}
