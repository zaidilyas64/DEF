using DEF.Shared;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using Umbraco.Core.Models.PublishedContent;
using Umbraco.Core.Services;
using Umbraco.Web;

namespace DEF.Model.Base
{
    public class BaseItem
    {
        IUserService userService = Umbraco.Core.Composing.Current.Services.UserService;
        public BaseItem() { }
        public BaseItem(IPublishedContent content)
        {
            if (content != null)
            {
                this.Id = content.Id;
                this.Name = content.Name;
                this.Level = content.Level;
                this.TemplateId = content.TemplateId;
                this.DocumentTypeAlias = content.ContentType.Alias;
                this.WriterName = content.WriterName(userService);
                this.WriterId = content.WriterId;
                this.CreatorName = content.CreatorName(userService);
                this.CreatorId = content.CreatorId;
                this.Path = content.Path;
                this.Url = content.GetCulturedBaseUrl();
                this.SortOrder = content.SortOrder;
                this.IsDraft = content.IsDraft(LanguageInfo.CurrentCulture);
                this.IsPublished = content.IsPublished(LanguageInfo.CurrentCulture);
                this.Parent = content.Parent;
                this.Children = content.Children;
                this.CreateDate = content.CreateDate;
                this.UpdateDate = content.UpdateDate;
            }
        }

        /// <summary>
        /// Returns the unique Id for the current content item
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Returns the Name of the current content item
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Returns the Level this content item is on
        /// </summary>
        public int Level { get; set; }
        /// <summary>
        /// Returns the Template id used by this content item.
        /// </summary>
        public int? TemplateId { get; set; }
        /// <summary>
        /// Returns the Alias of the Document type used by this content item.
        /// </summary>
        public string DocumentTypeAlias { get; set; }
        /// <summary>
        /// Returns the name of the Umbraco backoffice user that performed the last update operation on the content item.
        /// </summary>
        public string WriterName { get; }
        /// <summary>
        /// Returns the Id of the Umbraco backoffice user that performed the last update operation to the content item.
        /// </summary>
        public int WriterId { get; }
        /// <summary>
        /// Returns the name of the Umbraco backoffice user that initially created the content item.
        /// </summary>
        public string CreatorName { get; }
        /// <summary>
        /// Returns the Id of the Umbraco backoffice user that initially created the content item.
        /// </summary>
        public int CreatorId { get; }
        /// <summary>
        /// Returns a comma delimited string of Node Ids that represent the path of content items back to root.
        /// </summary>
        public string Path { get; set; }
        /// <summary>
        /// Returns the complete Url to the page
        /// </summary>
        public string Url { get; set; }
        /// <summary>
        /// Returns the index the page is on, compared to its siblings
        /// </summary>
        public int SortOrder { get; set; }
        /// <summary>
        /// Returns true if content item is in draft state.
        /// </summary>
        public bool IsDraft { get; set; }
        /// <summary>
        /// Return true if content item is in published state.
        /// </summary>
        public bool IsPublished { get; set; }
        [JsonIgnore]
        /// <summary>
        /// Returns the parent content item
        /// </summary>
        public IPublishedContent Parent { get; set; }
        [JsonIgnore]
        /// <summary>
        /// Returns the children content item
        /// </summary>
        public IEnumerable<IPublishedContent> Children { get; set; }
        /// <summary>
        /// Returns the DateTime the page was created
        /// </summary>
        public DateTime CreateDate { get; set; }
        /// <summary>
        /// Returns the DateTime the page was modified
        /// </summary>
        public DateTime UpdateDate { get; set; }
    }
}
