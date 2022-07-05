using DEF.Model.Base;
using System.Collections.Generic;
using Umbraco.Core.Models.PublishedContent;

namespace DEF.Model.Pages
{
    public class Home : SEOTag
    {
        public Home()
        {

        }
        public Home(IPublishedContent content) : base(content)
        {

        }
        public IEnumerable<IPublishedContent> Components { get; set; }
    }
}
