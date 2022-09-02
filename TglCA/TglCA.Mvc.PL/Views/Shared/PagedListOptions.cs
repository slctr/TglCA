using X.PagedList.Web.Common;

namespace TglCA.Mvc.PL.Views.Shared
{
    public static class PagedListOptions
    {
        public static readonly PagedListRenderOptions options = new PagedListRenderOptions
        {
            LiElementClasses = new string[] { "page-item" },
            PageClasses = new string[] { "page-link" },
            DisplayLinkToLastPage = PagedListDisplayMode.IfNeeded,
            LinkToLastPageFormat = "Last",
            DisplayLinkToFirstPage = PagedListDisplayMode.IfNeeded,
            LinkToFirstPageFormat = "First",
            MaximumPageNumbersToDisplay = 7
        };
    }
}
