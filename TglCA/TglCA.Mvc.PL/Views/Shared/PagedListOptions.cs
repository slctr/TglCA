using X.PagedList.Web.Common;

namespace TglCA.Mvc.PL.Views.Shared;

public static class PagedListOptions
{
    public static readonly PagedListRenderOptions options = new()
    {
        LiElementClasses = new[] { "page-item" },
        PageClasses = new[] { "page-link" },
        DisplayLinkToLastPage = PagedListDisplayMode.IfNeeded,
        LinkToLastPageFormat = "Last",
        DisplayLinkToFirstPage = PagedListDisplayMode.IfNeeded,
        LinkToFirstPageFormat = "First",
        MaximumPageNumbersToDisplay = 7
    };
}