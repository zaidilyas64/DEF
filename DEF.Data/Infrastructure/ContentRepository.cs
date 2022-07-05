using System;
using System.Collections.Generic;
using System.Linq;
using Umbraco.Core;
using Umbraco.Core.Models;
using Umbraco.Core.Models.PublishedContent;
using Umbraco.Web;

namespace DEF.Data.Infrastructure
{
    public interface IContentRepository : IDisposable
    {
        IPublishedContent Get();
        IPublishedContent Get(Guid id);
        IPublishedContent Get(int id);
        IPublishedContent Get(string id);
        IPublishedContent Get(Udi udi);
        IEnumerable<IPublishedContent> GetChildren();
        IPublishedContent GetHome();
        IPublishedContent GetForm();
        IPublishedContent GetDataSource();
        IPublishedContent GetSettingNode();
        IPublishedContent GetWebsite();
        IPublishedContent GetCurrentWebsite();
        IPublishedContent GetCurrentWebsite(int id);
        IContent CreateNode(Dictionary<string, string> data, string name, int parentId, string documentType, bool isCultureSupported, string culture = "en-US");
    }

    public class ContentRepository : IContentRepository
    {
        private bool disposed = false;
        private ICmsInfoRepository _cmsInfoRepository;

        public ContentRepository(ICmsInfoRepository cmsInfoRepository)
        {
            _cmsInfoRepository = cmsInfoRepository;
        }

        /// <summary>
        /// Return the Current Page IPublishedContent
        /// </summary>
        /// <returns>IPublishedContent</returns>
        public IPublishedContent Get()
        {
            try
            {
                return _cmsInfoRepository.GetUmbracoContext().PublishedRequest.PublishedContent;
            }
            catch (Exception ex)
            {
                throw new Exception("An error occured in ContentRepository -- Get()", ex);
            }

        }

        /// <summary>
        /// Return IPublishedContent by Guid Id
        /// </summary>
        /// <param name="id">Guid</param>
        /// <returns>IPublishedContent</returns>
        public IPublishedContent Get(Guid id)
        {
            try
            {
                return _cmsInfoRepository.GetUmbracoHelper().Content(id);
            }
            catch (Exception ex)
            {
                throw new Exception("An error occured in ContentRepository -- Get(Guid id)", ex);
            }

        }

        /// <summary>
        /// Return the IPublishedContent by string (string can be guid or integer value)
        /// </summary>
        /// <param name="id">string</param>
        /// <returns>IPublishedContent</returns>
        public IPublishedContent Get(string id)
        {
            try
            {
                return _cmsInfoRepository.GetUmbracoHelper().Content(id);
            }
            catch (Exception ex)
            {
                throw new Exception("An error occured in ContentRepository -- Get(string id)", ex);
            }

        }

        /// <summary>
        /// Return the IPublishedContent by int
        /// </summary>
        /// <param name="id"></param>
        /// <returns>IPublishedContent</returns>
        public IPublishedContent Get(int id)
        {
            try
            {
                return _cmsInfoRepository.GetUmbracoHelper().Content(id);
            }
            catch (Exception ex)
            {
                throw new Exception("An error occured in ContentRepository -- Get(int id)", ex);
            }

        }

        /// <summary>
        /// Return the IPublishedContent by Udi
        /// </summary>
        /// <param name="id">Udi</param>
        /// <returns>IPublishedContent</returns>
        public IPublishedContent Get(Udi udi)
        {
            try
            {
                return _cmsInfoRepository.GetUmbracoHelper().Content(udi);
            }
            catch (Exception ex)
            {
                throw new Exception("An error occured in ContentRepository -- Get(Udi udi)", ex);
            }

        }

        /// <summary>
        /// Returns all the childrens of a current item of all type.
        /// </summary>
        /// <returns>IEnumerable<IPublishedContent></returns>
        public IEnumerable<IPublishedContent> GetChildren()
        {
            try
            {
                IPublishedContent source = Get();
                return source?.Children;
            }
            catch (Exception ex)
            {
                throw new Exception("An error occured in ContentRepository -- GetChildren()", ex);
            }

        }

        /// <summary>
        /// Get the first site from the root content.
        /// </summary>
        /// <returns></returns>
        public IPublishedContent GetWebsite()
        {
            try
            {
                return _cmsInfoRepository.GetUmbracoHelper().ContentAtRoot()?.FirstOrDefault(x => x.ContentType.Alias.Equals("site"));
            }
            catch (Exception ex)
            {
                throw new Exception("An error occured in ContentRepository -- GetWebsite()", ex);
            }

        }

        /// <summary>
        /// Get the Current website from the root content.
        /// </summary>
        /// <returns></returns>
        public IPublishedContent GetCurrentWebsite()
        {
            try
            {
                return Get()?.Ancestor("site");
            }
            catch (Exception ex)
            {
                throw new Exception("An error occured in ContentRepository -- GetCurrentWebsite()", ex);
            }

        }
        /// <summary>
        /// Get current website by Id of an IPublished Content then returns the parent website of that Content.
        /// </summary>
        /// <returns></returns>
        public IPublishedContent GetCurrentWebsite(int id)
        {
            try
            {
                return Get(id).Ancestor("site");
            }
            catch (Exception ex)
            {
                throw new Exception("An error occured in ContentRepository -- GetCurrentWebsite(int id)", ex);
            }

        }

        /// <summary>
        /// Get the first Home Node from the root content.
        /// </summary>
        /// <returns>IPublishedContent</returns>
        public IPublishedContent GetHome()
        {
            try
            {
                IPublishedContent website = GetWebsite();
                return website?.Children?.FirstOrDefault(x => x.ContentType.Alias.Equals("home"));
            }
            catch (Exception ex)
            {
                throw new Exception("An error occured in ContentRepository -- GetHome()", ex);
            }

        }

        /// <summary>
        /// Get the first Form Node from the root content.
        /// </summary>
        /// <returns>IPublishedContent</returns>
        public IPublishedContent GetForm()
        {
            try
            {
                return _cmsInfoRepository.GetUmbracoHelper().ContentAtRoot()?.FirstOrDefault(x => x.ContentType.Alias.Equals("forms"));
            }
            catch (Exception ex)
            {
                throw new Exception("An error occured in ContentRepository -- GetForm()", ex);
            }

        }

        /// <summary>
        /// Get the first Datasource Node from the root content.
        /// </summary>
        /// <returns>IPublishedContent</returns>
        public IPublishedContent GetDataSource()
        {
            try
            {
                return _cmsInfoRepository.GetUmbracoHelper().ContentAtRoot()?.FirstOrDefault(x => x.ContentType.Alias.Equals("datasources"));
            }
            catch (Exception ex)
            {
                throw new Exception("An error occured in ContentRepository -- GetForm()", ex);
            }

        }

        /// <summary>
        /// Get the Setting node from the site
        /// </summary>
        /// <returns>IPublishedContent</returns>
        /// 
        public IPublishedContent GetSettingNode()
        {
            try
            {
                IPublishedContent website = GetWebsite();
                return website?.Children?.FirstOrDefault(x => x.ContentType.Alias.Equals("settings"));
            }
            catch (Exception ex)
            {
                throw new Exception("An error occured in ContentRepository -- GetSettingNode()", ex);
            }

        }
        public IContent CreateNode(Dictionary<string, string> data, string name, int parentId, string documentType, bool isCultureSupported, string culture = "en-US")
        {
            var contentService = _cmsInfoRepository.GetContentService();
            var content = contentService.CreateAndSave(name, parentId, documentType, 0);
            foreach (var item in data)
            {
                if (isCultureSupported)
                {
                    content.SetValue(item.Key, item.Value, culture);
                }
                else
                {
                    content.SetValue(item.Key, item.Value);
                }
            }

            if (isCultureSupported)
            {
                contentService.SaveAndPublish(content, culture);
            }
            else
            {
                contentService.SaveAndPublish(content);
            }
            return content;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    _cmsInfoRepository = null;
                }
                disposed = true;
            }
        }
    }
}
