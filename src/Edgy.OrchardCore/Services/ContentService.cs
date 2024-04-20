namespace Edgy.OrchardCore.Services;

public class ContentService
{
    //private readonly IAuthorizationService _authorizationService;
    //private readonly IContentHandleManager _contentHandleManager;
    //private readonly IContentManager _contentManager;
    private readonly IOrchardHelper _orchardHelper;
    //private readonly ISession _session;
    private readonly ILogger<ContentService> _logger;

    public ContentService(IOrchardHelper orchardHelper, ILogger<ContentService> logger) //IAuthorizationService authorizationService, IContentHandleManager contentHandleManager, IContentManager contentManager, ISession session,
    {
        //_authorizationService = authorizationService;
        //_contentHandleManager = contentHandleManager;
        //_contentManager = contentManager;
        _orchardHelper = orchardHelper;
        //_session = session;
        _logger = logger;
    }

    public async Task<PageContent> GetPageByAliasAsync(string alias)
    {
        //var id = await _contentHandleManager.GetContentItemIdAsync($"alias:{alias}");
        //var contentItem = await _contentManager.GetAsync(id, VersionOptions.Latest);
        var contentItem = await _orchardHelper.GetContentItemByAliasAsync(alias);

        if (contentItem != null)
        {
            var result = ConvertContentItemToPage(contentItem);

            return result;
        }

        return null;
    }

    private static PageContent ConvertContentItemToPage(ContentItem contentItem)
    {
        var result = new PageContent();
        result.Id = contentItem.ContentItemId;
        result.Title = contentItem.DisplayText; //contentItem.As<TitlePart>().Title;
        result.Permalink = contentItem.As<AliasPart>().Alias; //contentItem.As<AutoroutePart>().Path; // Use Alias?

        result.Html = contentItem.As<HtmlBodyPart>().Html;
        result.CreatedUtc = contentItem.CreatedUtc;

        var seo = contentItem.As<SeoMetaPart>();
        if (seo != null)
        {
            result.MetaDescription = seo.MetaDescription;
        }

        return result;
    }

    private List<MenuContentItem> GetMenuItems(ContentItem contentItem)
    {
        var result = new List<MenuContentItem>();
        if (contentItem.As<MenuItemsListPart>() != null)
        {
            foreach (var menuItem in contentItem.As<MenuItemsListPart>().MenuItems)
            {
                var item = new MenuContentItem();
                item.Title = menuItem.DisplayText;
                //item.Name = menuItem.As<LinkMenuItemPart>().Name;
                item.Url = menuItem.As<LinkMenuItemPart>().Url; // Remove "~" from url?
                //item.CreatedUtc = menuItem.CreatedUtc;

                // Get items recursively
                item.Items = GetMenuItems(menuItem);

                result.Add(item);
            }
        }
        return result;
    }

    //public async Task<List<PageContent>> GetPages()
    //{
    //    var result = new List<PageContent>();
    //    var query = _session.Query<ContentItem, ContentItemIndex>();
    //    query = query.With<ContentItemIndex>(x => x.ContentType == "Page");
    //    query = query.With<ContentItemIndex>(x => x.Published);
    //    query = query.OrderByDescending(x => x.PublishedUtc);
    //    var contentItems = await query.ListAsync(); //.Skip(pager.GetStartIndex()).Take(pager.PageSize).ListAsync();

    //    foreach (var contentItem in contentItems)
    //    {
    //        result.Add(ConvertContentItemToPage(contentItem));
    //    }

    //    return result;
    //}

    //public async Task<MenuContent> GetMenuByAlias(string alias)
    //{
    //    var result = new MenuContent();
    //    var menu = await _orchardHelper.GetContentItemByAliasAsync(alias);

    //    if (menu == null)
    //    {
    //        return NotFound();
    //    }

    //    //result.Id = contentItem.ContentItemId;
    //    result.Title = menu.DisplayText;
    //    result.Alias = menu.As<AliasPart>().Alias;
    //    //result.CreatedUtc = menu.CreatedUtc;

    //    var items = GetMenuItems(menu);
    //    result.Items = items;

    //    return result);
    //}

    //public async Task<ContentItem> AddContent(ContentItem contentItem)
    //{
    //    //if (!await _authorizationService.AuthorizeAsync(User, Permissions.DemoAPIAccess))
    //    //{
    //    //    return this.ChallengeOrForbid("Api");
    //    //}

    //    await _contentManager.CreateAsync(contentItem);

    //    return contentItem;
    //}
}
