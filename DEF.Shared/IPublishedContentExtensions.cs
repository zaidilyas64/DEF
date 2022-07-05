using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Umbraco.Core;
using Umbraco.Core.Models.PublishedContent;
using Umbraco.Core.PropertyEditors.ValueConverters;
using Umbraco.Web;
using Umbraco.Web.Models;

namespace DEF.Shared
{
    public static class IPublishedContentExtensions
    {
        private static UrlHelper _urlHelper = new UrlHelper(HttpContext.Current.Request.RequestContext);

        /// <summary>
        /// Return the UmbracoHelper Object
        /// </summary>
        public static UmbracoHelper UmbracoHelper => Umbraco.Web.Composing.Current.UmbracoHelper;

        /// <summary>
        /// Returns the Current Page IPublished Content
        /// </summary>
        public static IPublishedContent CurrentPage => Umbraco.Web.Composing.Current.UmbracoContext.PublishedRequest.PublishedContent;

        /// <summary>
        /// Returns the culture base url for IPublished Content
        /// </summary>
        /// <param name="item"></param>
        /// <returns>string</returns>
        public static string GetCulturedBaseUrl(this IPublishedContent item)
        {
            if (item.Url().Contains("/en/") || item.Url().Contains("/ar/"))
                return string.Concat("/", LanguageInfo.CurrentCulture, item.Url()
                    .Replace("/en/", "/")
                    .Replace("/ar/", "/")).TrimEnd('/');
            else
                return string.Concat("/", LanguageInfo.CurrentCulture, item.Url()).TrimEnd('/');
        }

        /// <summary>
        /// Returns the culture base url for IPublished Content
        /// </summary>
        /// <param name="item"></param>
        /// <returns>string</returns>
        public static string GetCulturedBaseUrl(this string url)
        {
            if (url.Contains("/en/") || url.Contains("/ar/"))
                return string.Concat("/", LanguageInfo.CurrentCulture, url
                    .Replace("/en/", "/")
                    .Replace("/ar/", "/")).TrimEnd('/');
            else
                return string.Concat("/", LanguageInfo.CurrentCulture, url).TrimEnd('/');
        }

        /// <summary>
        /// Get the string UDIs
        /// </summary>
        /// <param name="guids">string</param>
        /// <param name="udiEntityType">string</param>
        /// <returns>string</returns>
        public static string GetUdi(string[] guids, string udiEntityType)
        {
            List<string> udi = new List<string>();
            if (guids != null && guids.Any())
            {
                foreach (var item in guids)
                {
                    udi.Add(Udi.Create(udiEntityType, new Guid(item)).ToString());
                }
            }
            return string.Join(",", udi);
        }
        public static string GetUdi(string guid, string udiEntityType)
        {

            if (guid != null)
            {

                return Udi.Create(udiEntityType, new Guid(guid)).ToString();


            }
            return string.Empty;
        }

        #region Fields Methods

        /// <summary>
        /// Return Object property value
        /// </summary>
        /// <param name="item">IPublishedContent</param>
        /// <param name="propertyName">string</param>
        /// <returns>string</returns>
        public static object GetFieldValue(this IPublishedContent source, string propertyName, bool isFallBack = false)
        {
            if (!source.HasItemProperty(propertyName))
                return null;
            if (isFallBack)
            {
                return source.GetProperty(propertyName).GetValue();
            }
            else
            {
                return source.GetProperty(propertyName).GetValue(LanguageInfo.CultureInfo.Name);
            }
        }

        /// <summary>
        /// Return object property value
        /// </summary>
        /// <param name="item">IPublishedElement</param>
        /// <param name="propertyName">string</param>
        /// <returns>object</returns>
        public static object GetFieldValue(this IPublishedElement source, string propertyName, bool isFallBack = false)
        {
            if (!source.HasItemProperty(propertyName))
                return null;

            if (isFallBack)
            {
                return source.GetProperty(propertyName).GetValue();
            }
            else
            {
                return source.GetProperty(propertyName).GetValue(LanguageInfo.CultureInfo.Name);
            }
        }

        /// <summary>
        /// Return Generic Value (Note: don't use this method for complex properties instead use thier on Field methods)
        /// </summary>
        /// <typeparam name="T">T</typeparam>
        /// <param name="item">IPublishedContent</param>
        /// <param name="propertyName">string</param>
        /// <param name="defaultValue">T</param>
        /// <returns>T</returns>
        public static T GetFieldValue<T>(this IPublishedElement source, string propertyName, T defaultValue, bool fallBack = false)
        {
            var value = GetFieldValue(source, propertyName, fallBack);
            return value != null ? (T)Convert.ChangeType(value, typeof(T)) : defaultValue;
        }

        /// <summary>
        /// Return Generic Value (Note: don't use this method for complex properties instead use thier on Field methods)
        /// </summary>
        /// <typeparam name="T">T</typeparam>
        /// <param name="item">IPublishedContent</param>
        /// <param name="propertyName">string</param>
        /// <param name="defaultValue">T</param>
        /// <returns>T</returns>
        public static T GetFieldValue<T>(this IPublishedContent source, string propertyName, T defaultValue, bool fallBack = false)
        {
            var value = GetFieldValue(source, propertyName, fallBack);
            return value != null ? (T)Convert.ChangeType(value, typeof(T)) : defaultValue;
        }

        /// <summary>
        /// Returns the IEnumerable string value
        /// </summary>
        /// <param name="source">IPublishedContent</param>
        /// <param name="propertyName">string</param>
        /// <returns>IEnumerable<string></returns>
        public static IEnumerable<string> GetFieldValues(this IPublishedContent source, string propertyName)
        {
            var value = GetFieldValue(source, propertyName);
            return value != null ? (IEnumerable<string>)value : null;
        }

        /// <summary>
        /// Returns the Single link as Link Object
        /// </summary>
        /// <param name="item">IPublishedContent</param>
        /// <param name="propertyName">string</param>
        /// <returns>Link</returns>
        public static Link GetLinkFieldValue(this IPublishedContent source, string propertyName, bool fallBack = false)
        {
            var value = GetFieldValue(source, propertyName, fallBack);
            var link = value != null ? (Link)value : null;
            if (link != null)
            {
                string queryString = string.Empty;
                string url = string.Empty;

                string fullUrl = link.Url;
                int queryStringIndex = fullUrl.IndexOf("?");
                if (queryStringIndex > 0)
                {
                    url = fullUrl.Substring(0, queryStringIndex - 1);
                    queryString = fullUrl.Substring(queryStringIndex, fullUrl.Length - queryStringIndex);
                }
                else
                {
                    url = fullUrl;
                }
                link.Url = $"{url.TrimEnd('/')}{queryString}";
            }
            return link;
        }

        /// <summary>
        /// Returns the Single link as Link Object without removing trailing slash
        /// </summary>
        /// <param name="item">IPublishedContent</param>
        /// <param name="propertyName">string</param>
        /// <returns>Link</returns>
        public static Link GetLinkFieldValueWithoutTrimEnd(this IPublishedContent source, string propertyName, bool fallBack = false)
        {
            var value = GetFieldValue(source, propertyName, fallBack);
            var link = value != null ? (Link)value : null;
            if (link != null)
            {
                string queryString = string.Empty;
                string url = string.Empty;

                string fullUrl = link.Url;
                int queryStringIndex = fullUrl.IndexOf("?");
                if (queryStringIndex > 0)
                {
                    url = fullUrl.Substring(0, queryStringIndex - 1);
                    queryString = fullUrl.Substring(queryStringIndex, fullUrl.Length - queryStringIndex);
                }
                else
                {
                    url = fullUrl;
                }
                link.Url = $"{url}{queryString}";
            }
            return link;
        }

        /// <summary>
        /// Returns the Single link as Link Object
        /// </summary>
        /// <param name="item">IPublishedContent</param>
        /// <param name="propertyName">string</param>
        /// <returns>Link</returns>
        public static Link GetLinkFieldValue(this IPublishedElement source, string propertyName, bool fallBack = false)
        {
            var value = GetFieldValue(source, propertyName, fallBack);
            var link = value != null ? (Link)value : null;
            if (link != null)
            {
                link.Url = link.Url.TrimEnd('/');
            }
            return link;
        }

        /// <summary>
        /// Returns the Collection of links as IEnumerable items
        /// </summary>
        /// <param name="source">IPublishedContent</param>
        /// <param name="propertyName">string</param>
        /// <returns>IEnumerable<Link></returns>
        public static IEnumerable<Link> GetLinksFieldValue(this IPublishedContent source, string propertyName, bool fallBack = false)
        {
            var value = GetFieldValue(source, propertyName, fallBack);
            return value != null ? (IEnumerable<Link>)value : null;
        }

        /// <summary>
        /// Returns the Collection of links as IEnumerable items
        /// </summary>
        /// <param name="source">IPublishedContent</param>
        /// <param name="propertyName">string</param>
        /// <returns>IEnumerable<Link></returns>
        public static IEnumerable<Link> GetLinksFieldValue(this IPublishedElement source, string propertyName, bool fallBack = false)
        {
            var value = GetFieldValue(source, propertyName, fallBack);
            return value != null ? (IEnumerable<Link>)value : null;
        }

        /// <summary>
        /// Returns the tags as string array
        /// </summary>
        /// <param name="source">IPublishedContent</param>
        /// <param name="propertyName">string</param>
        /// <returns>string[]</returns>
        public static string[] GetTagsFieldValue(this IPublishedContent source, string propertyName)
        {
            var value = GetFieldValue(source, propertyName);
            string[] tags = value != null ? (string[])value : null;
            return tags;
        }

        /// <summary>
        /// Returns the Single Multinode IPublished Content
        /// </summary>
        /// <param name="source">IPublishedContent</param>
        /// <param name="propertyName">string</param>
        /// <returns>IPublishedContent</returns>
        public static IPublishedContent GetMultiNodeFieldValue(this IPublishedContent source, string propertyName, bool fallback = false)
        {
            var value = GetFieldValue(source, propertyName, fallback);
            return value != null ? (IPublishedContent)value : null;
        }
        public static IPublishedContent GetMultiNodeFieldValue(this IPublishedElement source, string propertyName, bool fallback = false)
        {
            var value = GetFieldValue(source, propertyName, fallback);
            return value != null ? (IPublishedContent)value : null;
        }

        /// <summary>
        /// Returns the Collection of IPublished Content
        /// </summary>
        /// <param name="source">IPublishedContent</param>
        /// <param name="propertyName">string</param>
        /// <returns>IEnumerable<IPublishedContent></returns>
        public static IEnumerable<IPublishedContent> GetMultiNodesFieldValue(this IPublishedContent source, string propertyName, bool fallback = false)
        {
            var value = GetFieldValue(source, propertyName, fallback);

            if (value is IEnumerable<IPublishedContent>)
            {
                var castValue = value as IEnumerable<IPublishedContent>;

                if (castValue != null && castValue.Any())
                    return castValue;
                else
                    return null;
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// Returns the Collection of IPublished Content
        /// </summary>
        /// <param name="source">IPublishedElement</param>
        /// <param name="propertyName">string</param>
        /// <returns>IEnumerable<IPublishedContent></returns>
        public static IEnumerable<IPublishedContent> GetMultiNodesFieldValue(this IPublishedElement source, string propertyName, bool fallback = false)
        {
            var value = GetFieldValue(source, propertyName, fallback);

            if (value is IEnumerable<IPublishedContent>)
            {
                var castValue = value as IEnumerable<IPublishedContent>;

                if (castValue != null && castValue.Any())
                    return castValue;
                else
                    return null;
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// Returns field values as string array
        /// </summary>
        /// <param name="source">IPublishedContent</param>
        /// <param name="propertyName">string</param>
        /// <returns>string[]</returns>
        public static string[] GetRepeatableStringFieldValue(this IPublishedContent source, string propertyName)
        {
            var value = GetFieldValue(source, propertyName);
            string[] values = value != null ? (string[])value : null;
            return values;
        }

        /// <summary>
        /// Returns field values as string array
        /// </summary>
        /// <param name="source">IPublishedElement</param>
        /// <param name="propertyName">string</param>
        /// <returns>string[]</returns>
        public static string[] GetRepeatableStringFieldValue(this IPublishedElement source, string propertyName)
        {
            var value = GetFieldValue(source, propertyName);
            string[] values = value != null ? (string[])value : null;
            return values;
        }

        /// <summary>
        /// Returns IPublished Element Items
        /// </summary>
        /// <param name="source">IPublishedElement</param>
        /// <param name="propertyName">string</param>
        /// <returns>IEnumerable<IPublishedElement></returns>
        public static IEnumerable<IPublishedElement> GetNestedContentFieldValue(this IPublishedElement source, string propertyName)
        {
            var value = GetFieldValue(source, propertyName);
            return value != null ? (IEnumerable<IPublishedElement>)value : null;
        }

        /// <summary>
        /// Returns IPublished Element Items
        /// </summary>
        /// <param name="source">IPublishedContent</param>
        /// <param name="propertyName">string</param>
        /// <returns>IEnumerable<IPublishedElement></returns>
        public static IEnumerable<IPublishedElement> GetNestedContentFieldValue(this IPublishedContent source, string propertyName)
        {
            var value = GetFieldValue(source, propertyName);
            return value != null ? (IEnumerable<IPublishedElement>)value : null;
        }

        /// <summary>
        /// Get Image Url 
        /// </summary>
        /// <param name="source">IPublishedContent</param>
        /// <param name="propertyName">string</param>
        /// <returns>string</returns>
        public static string GetMediaItemFieldValue(this IPublishedContent source, string propertyName)
        {
            var value = GetFieldValue(source, propertyName);
            return value != null ? ((IPublishedContent)value).MediaUrl() : null;
        }

        /// <summary>
        /// Get Video Url 
        /// </summary>
        /// <param name="source">IPublishedContent</param>
        /// <param name="propertyName">string</param>
        /// <returns>string</returns>
        public static string GetVideoFieldValue(this IPublishedContent source, string propertyName)
        {
            var value = GetFieldValue(source, propertyName);
            return value != null ? ((IPublishedContent)value).MediaUrl() : null;
        }

        /// <summary>
        /// Get Image Url 
        /// </summary>
        /// <param name="source">IPublishedElement</param>
        /// <param name="propertyName">string</param>
        /// <returns>string</returns>
        public static string GetMediaItemFieldValue(this IPublishedElement source, string propertyName)
        {
            var value = GetFieldValue(source, propertyName);
            return value != null && value is IPublishedContent ? ((IPublishedContent)value).MediaUrl() : null;
        }

        /// <summary>
        /// Get List of Image Urls
        /// </summary>
        /// <param name="source">IPublishedContent</param>
        /// <param name="propertyName">string</param>
        /// <returns>IList<string></returns>
        public static IList<string> GetMediaItemsFieldValue(this IPublishedContent source, string propertyName)
        {
            List<string> ImageUrls = new List<string>();

            var value = source.Value<IEnumerable<IPublishedContent>>(propertyName); ;// GetFieldValue<IEnumerable<IPublishedContent>>(source, propertyName);
            if (value == null)
                return null;

            //var fieldValue = (List<IPublishedContent>)value;

            if (value == null && !value.Any())
                return null;

            foreach (IPublishedContent item in value)
            {
                ImageUrls.Add(item.MediaUrl());
            }
            return ImageUrls;
        }

        /// <summary>
        /// Get List of Image Urls
        /// </summary>
        /// <param name="source">IPublishedElement</param>
        /// <param name="propertyName">string</param>
        /// <returns>IList<string></returns>
        public static IList<string> GetMediaItemsFieldValue(this IPublishedElement source, string propertyName)
        {
            List<string> ImageUrls = new List<string>();

            var value = GetFieldValue(source, propertyName);
            if (value == null)
                return null;

            IList<IPublishedContent> fieldValue = (List<IPublishedContent>)value;

            if (fieldValue == null && !fieldValue.Any())
                return null;

            foreach (IPublishedContent item in fieldValue)
            {
                ImageUrls.Add(item.MediaUrl());
            }
            return ImageUrls;
        }

        public static ImageCropperValue GetCropImage(this IPublishedContent source, string propertyName)
        {
            if (source == null)
                return null;

            return GetUmbracoImageCropper(source, propertyName);
        }

        private static ImageCropperValue GetUmbracoImageCropper(IPublishedContent source, string propertyName)
        {
            if (source == null && string.IsNullOrWhiteSpace(propertyName) && source.GetProperty(propertyName) == null)
                return null;

            return (ImageCropperValue)source.GetProperty(propertyName).GetValue(LanguageInfo.CultureInfo.Name);
        }

        /// <summary>
        /// Validate the source has childrens
        /// </summary>
        /// <param name="source">Source to validate has childrens</param>
        /// <returns>returns true if source is not null and source has childres else false</returns>
        public static bool HasChildren(this IPublishedContent source)
        {
            return source != null && source.Children != null && source.Children.Any();
        }
        /// <summary>
        /// Validate the source has any item
        /// </summary>
        /// <param name="source">Source to validate has any item</param>
        /// <returns>returns true if source is not null and source has any item else false</returns>
        public static bool HasChildrens(this IEnumerable<IPublishedContent> source)
        {
            return source != null && source.Any();
        }
        private static bool HasItemProperty(this IPublishedContent source, string propertyName)
        {
            return source != null && !string.IsNullOrWhiteSpace(propertyName) && source.GetProperty(propertyName) != null;
        }
        private static bool HasItemProperty(this IPublishedElement source, string propertyName)
        {
            return source != null && !string.IsNullOrWhiteSpace(propertyName) && source.GetProperty(propertyName) != null;
        }

        public static IPublishedContent GetPublishedContent(this Udi udi)
        {
            var umbracoHelper = Umbraco.Web.Composing.Current.UmbracoHelper;
            return umbracoHelper.Content(udi);
        }
        #endregion
    }

}
