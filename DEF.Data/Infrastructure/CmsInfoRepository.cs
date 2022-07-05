using System;
using Umbraco.Core.Services;
using Umbraco.Web;
using Umbraco.Web.Composing;

namespace DEF.Data.Infrastructure
{
    public interface ICmsInfoRepository : IDisposable
    {
        /// <summary>
        /// This method returns the current application context for Content Service.
        /// </summary>
        /// <returns>IContentService</returns>
        IContentService GetContentService();
        /// <summary>
        /// This method returns the current application context for Media Service.
        /// </summary>
        /// <returns>IMediaService</returns>
        IMediaService GetMediaService();
        /// <summary>
        /// This method returns the current Umbraco context.
        /// </summary>
        /// <returns>UmbracoContext</returns>
        UmbracoContext GetUmbracoContext();
        /// <summary>
        /// This method returns the Umbraco helper.
        /// </summary>
        /// <returns>UmbracoHelper</returns>
        UmbracoHelper GetUmbracoHelper();

        /// <summary>
        /// This method returns the current application context for IContentType Service.
        /// </summary>
        /// <returns>IContentTypeService</returns>
        IContentTypeService GetContentTypeService();

        /// <summary>
        /// This method returns the current application Service
        /// </summary>
        /// <returns>ServiceContext</returns>
        ServiceContext Service();
    }

    public class CmsInfoRepository : ICmsInfoRepository
    {
        private bool disposed = false;

        /// <summary>
        /// This method returns the Umbraco helper.
        /// </summary>
        /// <returns>UmbracoHelper</returns>
        public UmbracoHelper GetUmbracoHelper() => Current.UmbracoHelper;

        /// <summary>
        /// This method returns the current Umbraco context.
        /// </summary>
        /// <returns>UmbracoContext</returns>
        public UmbracoContext GetUmbracoContext() => Current.UmbracoContext;

        /// <summary>
        /// This method returns the current application context for Media Service.
        /// </summary>
        /// <returns>IMediaService</returns>
        public IMediaService GetMediaService() => Current.Services.MediaService;

        /// <summary>
        /// This method returns the current application context for Content Service.
        /// </summary>
        /// <returns>IContentService</returns>
        public IContentService GetContentService() => Current.Services.ContentService;

        /// <summary>
        /// This method returns the current application context for IContentType Service.
        /// </summary>
        /// <returns>IContentTypeService</returns>
        public IContentTypeService GetContentTypeService() => Current.Services.ContentTypeService;

        /// <summary>
        /// This method returns the current application Service
        /// </summary>
        /// <returns>ServiceContext</returns>
        public ServiceContext Service() => Umbraco.Core.Composing.Current.Services;

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        protected virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                disposed = true;
            }
        }
    }

}
