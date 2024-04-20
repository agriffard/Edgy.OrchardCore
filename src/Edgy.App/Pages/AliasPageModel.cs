namespace Edgy.App.Pages;

public class AliasPageModel : BasePageModel
{
    private readonly ContentService _contentService;
    private readonly string _alias;

    public PageContent PageContent { get; set; }

    public AliasPageModel(ContentService contentService, string alias)
    {
        _contentService = contentService;
        _alias = alias;
    }

    public async Task OnGetAsync()
    {
        PageContent = await _contentService.GetPageByAliasAsync(_alias);

        ViewData["Title"] = PageContent.Title;
        ViewData["Description"] = PageContent.MetaDescription;
        //ViewData["Keywords"] = PageContent.MetaKeywords;
    }
}
