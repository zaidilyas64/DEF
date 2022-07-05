using DEF.Model.Base;
using Umbraco.Core.Models.PublishedContent;

namespace DEF.Model.Components
{
    public class Spotlight : BaseItem
    {
        public Spotlight()
        {

        }
        public Spotlight(IPublishedContent content) : base(content)
        {

        }
        public string Title { get; set; }
    }
}
