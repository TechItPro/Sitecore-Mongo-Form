
using Sitecore.Data.Items;
using Sitecore.Mvc.Presentation;

namespace TechITPro.Samples.Models.Utility
{
    public static class SitecoreUtility
    {
        public static Item GetDataSourceItem()
        {
            Item dataSource = null;
            if (((RenderingContext.Current != null) && (RenderingContext.Current.Rendering != null))
                && !string.IsNullOrEmpty(RenderingContext.Current.Rendering.DataSource))
            {
                dataSource = Sitecore.Context.Database.GetItem(RenderingContext.Current.Rendering.DataSource);
            }
            return dataSource;
        }
    }
}
