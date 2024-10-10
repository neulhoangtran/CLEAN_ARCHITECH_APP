using WebApp.Models;

namespace WebApp.Services
{
    public class LayoutService
    {
        public string Title { get; private set; } = "Dashboard";
        public string BodyClass { get; private set; }

        public List<BreadcrumbItem> Breadcrumb { get; private set; } = new List<BreadcrumbItem>
        {
            new BreadcrumbItem { Name = "Dashboard", Url = "/dashboard", IsActive = true }
        };

        public event Action OnChange;

        public void SetTitleAndBreadcrumb(string title, List<BreadcrumbItem> breadcrumb)
        {
            Title = title;
            Breadcrumb = breadcrumb;
            NotifyStateChanged();
        }

        public void SetCssClass(string bodyClass)
        {
            BodyClass = bodyClass;
            NotifyStateChanged();
        }

        private void NotifyStateChanged() => OnChange?.Invoke();
    }

}
