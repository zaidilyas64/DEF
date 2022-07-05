using DEF.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using Umbraco.Core.Models.PublishedContent;

namespace DEF.Model.Base
{
    public class SEOTag : BaseItem
    {
        public SEOTag() { }
        public SEOTag(IPublishedContent content) : base(content)
        {
            if (content != null)
            {
                #region Meta Tags
                this.PageTitle = Convert.ToString(content.GetFieldValue("pageTitle"));
                this.MetaTitle = Convert.ToString(content.GetFieldValue("metaTitle"));
                this.MetaDescription = Convert.ToString(content.GetFieldValue("metaDescription"));
                this.MetaKeywords = content.GetTagsFieldValue("metaKeywords");
                this.HrefLangs = content.Cultures.Select(x => x.Value.Culture);
                this.RobotsToIndex = content.GetFieldValue<bool>("robotsToIndex", false, true);

                #endregion

                #region Twitter Card
                this.TwitterTitle = Convert.ToString(content.GetFieldValue("twitterTitle"));
                this.TwitterCard = Convert.ToString(content.GetFieldValue("twitterCard"));
                this.TwitterSite = Convert.ToString(content.GetFieldValue("twitterSite"));
                this.TwitterCreator = Convert.ToString(content.GetFieldValue("twitterCreator"));
                this.TwitterDescription = Convert.ToString(content.GetFieldValue("twitterDescription"));
                this.TwitterImage = content.GetMediaItemFieldValue("twitterImage");
                #endregion

                #region Open Graph Tags
                this.OGTitle = Convert.ToString(content.GetFieldValue("ogTitle"));
                this.OGType = Convert.ToString(content.GetFieldValue("ogType"));
                this.OGDescription = Convert.ToString(content.GetFieldValue("ogDescription"));
                this.OGImage = content.GetMediaItemFieldValue("ogImage");
                #endregion
            }
        }

        #region Meta Tags
        public string PageTitle { get; set; }
        public string MetaTitle { get; set; }
        public string MetaDescription { get; set; }
        public string[] MetaKeywords { get; set; }
        public IEnumerable<string> HrefLangs { get; set; }
        public bool RobotsToIndex { get; set; }
        #endregion

        #region Twitter Card
        public string TwitterTitle { get; set; }
        public string TwitterCard { get; set; }
        public string TwitterSite { get; set; }
        public string TwitterCreator { get; set; }
        public string TwitterDescription { get; set; }
        public string TwitterImage { get; set; }
        #endregion

        #region Open Graph Tags
        public string OGTitle { get; set; }
        public string OGType { get; set; }
        public string OGDescription { get; set; }
        public string OGImage { get; set; }
        #endregion
    }
}
